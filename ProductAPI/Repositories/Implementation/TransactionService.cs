using SeminarAPI.Data.Model;
using SeminarAPI.Data;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using SeminarAPI.Repositories.Interface;
using System.Linq;
using SeminarAPI.Data.Dto;
using System.Collections.Generic;
using RazorEngineCore;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using System.Transactions;

namespace SeminarAPI.Repositories.Implementation
{
    /// <summary>
    /// OrderService Implementation
    /// </summary>
    public class TransactionService : Interface.ITransaction
    {
        /// <summary>
        /// _context
        /// </summary>
        protected SeminarDbContext _context;

        /// <summary>
        /// DietService
        /// </summary>
        /// <param name="context"></param>
        public TransactionService(SeminarDbContext context)
        {
            _context = context;
        }

        //public async Task<string> CreatedTransaction(RequestTransactionDto data)
        //{
        //    try
        //    {
        //        int rewardAmount = 0;
        //        if (!string.IsNullOrEmpty(data.challenge_tasks_id))
        //        {
        //            rewardAmount = Int32.Parse(_context.ChallengeTasks.Where(x => x.challenge_tasks_id == data.challenge_tasks_id).FirstOrDefault().reward_amount);
        //        } 
        //        else if (!string.IsNullOrEmpty(data.daily_tasks_id))
        //        {
        //            rewardAmount = Int32.Parse(_context.DailyTasks.Where(x => x.daily_tasks_id == data.daily_tasks_id).FirstOrDefault().reward_amount);
        //        }
        //        TransactionHistory transaction = new TransactionHistory();
        //        transaction.transaction_history_id = Guid.NewGuid().ToString();
        //        transaction.daily_tasks_id = data.daily_tasks_id;
        //        transaction.user_id = data.user_id;
        //        transaction.challenge_tasks_id = data.challenge_tasks_id;
        //        transaction.reward_amount = rewardAmount.ToString();
        //        transaction.type = data.type;
        //        transaction.status = data.status;
        //        transaction.Created_At = DateTime.Now;

        //        await _context.TransactionHistory.AddAsync(transaction);

        //        var walletUser = _context.Wallet.Where(x => x.user_id == data.user_id).FirstOrDefault();
        //        var amount = Int32.Parse(walletUser.male_usd) + rewardAmount;
        //        walletUser.male_usd = amount.ToString();
        //        _context.Wallet.Update(walletUser);

        //        await _context.SaveChangesAsync();
        //        return "Success";
        //    } catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        public async Task<List<ResRankingDto>> GetRanking()
        {
            try
            {
                var result = (from data in _context.Ranking
                              join user in _context.Users on
                              data.user_id equals user.User_Id into user_temp
                              from user in user_temp.DefaultIfEmpty()
                              select new ResRankingDto
                              {
                                  ranking_id = data.ranking_id,
                                  user_id = data.user_id,
                                  full_name = user.Full_Name,
                                  phone = user.Phone,
                                  country = user.Country,
                                  email = user.Email,
                                  reward_amount = data.reward_amount,
                                  type = data.type,
                                  status = data.status,
                                  Created_At = data.Created_At
                              }
                    ).OrderByDescending(x => x.reward_amount).ToList();
                if (result != null)
                    return result;

                return new List<ResRankingDto>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ResTransactionHistoryDto>> GetRewardDailyByUser(string userId)
        {
            try
            {
                var result = (from data in _context.TransactionHistory
                              join chall in _context.ChallengeTasks on 
                              data.challenge_tasks_id equals chall.challenge_tasks_id into chall_temp
                              from chall in chall_temp.DefaultIfEmpty()
                              join task in _context.DailyTasks on
                              data.daily_tasks_id equals task.daily_tasks_id into task_temp
                              from task in task_temp.DefaultIfEmpty()
                              where data.user_id == userId
                              select new ResTransactionHistoryDto {
                                    transaction_history_id = data.transaction_history_id,
                                    user_id = data.user_id,
                                    title_challenge_tasks = chall.title,
                                    title_daily_tasks = task.title,
                                    reward_amount = data.reward_amount,
                                    type = data.type,
                                    status = data.status,
                                    created_at = data.Created_At
                              }
                    ).OrderByDescending(x => x.created_at).ToList();
                if (result != null)
                    return result;

                return new List<ResTransactionHistoryDto>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<WalletDto> GetWalletByUser(string userId)
        {
            try
            {
                var dateNow = DateTime.Now.Date;
                var getDailyRewards = _context.TransactionHistory.Where(x => x.Created_At.Date == dateNow && x.user_id == userId).ToList();
                var rewards = getDailyRewards.Count() > 0 ? getDailyRewards.Sum(x => Int32.Parse(x.reward_amount)) : 0;
                var result = (from data in _context.Wallet
                              where data.user_id == userId
                              select new WalletDto
                              {
                                  wallet_id = data.wallet_id,
                                  user_id = data.user_id,
                                  female_usd = data.female_usd,
                                  male_usd = data.male_usd,
                                  amount_usd = data.amount_usd,
                                  daily_rewards = rewards.ToString(),
                                  created_at = data.Created_At
                              }).FirstOrDefault();
                if (result != null)
                    return result;

                return new WalletDto();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> JobRanking()
        {
            try
            {
                var result = (from data in _context.Users
                              join wallet in _context.Wallet on
                              data.User_Id equals wallet.user_id into wallet_temp
                              from wallet in wallet_temp.DefaultIfEmpty()
                              select new RewardRankingDto
                              {
                                  user_id = data.User_Id,
                                  reward_amount = wallet.male_usd
                              }
                    ).ToList();

                foreach ( var item in result )
                {
                    var rankingData = (from data in _context.Ranking
                                       where data.user_id == item.user_id
                                       select data
                                       ).FirstOrDefault();

                    if ( rankingData != null )
                    {
                        rankingData.reward_amount = item.reward_amount;
                        _context.Ranking.Update(rankingData);
                    }
                    else
                    {
                        Ranking ranking = new Ranking();
                        ranking.ranking_id = Guid.NewGuid().ToString();
                        ranking.user_id = item.user_id;
                        ranking.reward_amount = item.reward_amount;
                        ranking.type = "0";
                        ranking.status = 0;
                        ranking.Created_At = DateTime.Now;
                        _context.Ranking.AddAsync(ranking);
                    }
                    _context.SaveChanges();
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> UpdateRanking()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                
                    var getListTransaction = await _context.TransactionHistory.Where(x => x.status == 0).ToListAsync();
                    if (getListTransaction != null && getListTransaction.Count > 0)
                    {
                        var ranking = new Ranking();
                        var listUser = await _context.Users.ToListAsync();
                        foreach (var item in listUser)
                        {
                            var listTransactionByUser = getListTransaction.Where(x => x.user_id == item.User_Id).ToList();
                            if (listTransactionByUser != null && listTransactionByUser.Count > 0)
                            {
                                var checkUserInRanking = await _context.Ranking.Where(x => x.user_id == item.User_Id).FirstOrDefaultAsync();
                                if (checkUserInRanking != null)
                                {
                                    var sumAmount = listTransactionByUser.Sum(x => int.Parse(x.reward_amount));
                                    checkUserInRanking.reward_amount = (int.Parse(checkUserInRanking.reward_amount) + sumAmount).ToString();
                                    _context.Ranking.Update(checkUserInRanking);
                                }
                                else
                                {
                                    ranking = new Ranking();
                                    ranking.ranking_id = Guid.NewGuid().ToString();
                                    ranking.user_id = item.User_Id;
                                    var sumAmount = listTransactionByUser.Sum(x => int.Parse(x.reward_amount));
                                    ranking.reward_amount = sumAmount.ToString();
                                    await _context.Ranking.AddAsync(ranking);
                                }

                                foreach (var data in listTransactionByUser)
                                {
                                    data.status = 1;
                                }
                                _context.TransactionHistory.UpdateRange(listTransactionByUser);
                            }
                        }
                        _context.SaveChanges();
                    }
                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return false;
                }
            }
        }
    }
}
