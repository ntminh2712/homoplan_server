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

namespace SeminarAPI.Repositories.Implementation
{
    /// <summary>
    /// OrderService Implementation
    /// </summary>
    public class TransactionHistoryService : Interface.ITransactionHistory
    {
        /// <summary>
        /// _context
        /// </summary>
        protected SeminarDbContext _context;

        /// <summary>
        /// DietService
        /// </summary>
        /// <param name="context"></param>
        public TransactionHistoryService(SeminarDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreatedTransaction(TransactionHistoryDto data)
        {
            try
            {
                TransactionHistory transaction = new TransactionHistory();
                transaction.transaction_history_id = Guid.NewGuid().ToString();
                transaction.daily_tasks_id = data.daily_tasks_id;
                transaction.user_id = data.user_id;
                transaction.challenge_tasks_id = data.challenge_tasks_id;
                transaction.reward_amount = data.reward_amount;
                transaction.type = data.type;
                transaction.status = data.status;
                transaction.Created_At = DateTime.Now;

                await _context.TransactionHistory.AddAsync(transaction);

                var walletUser = _context.Wallet.Where(x => x.user_id == data.user_id).FirstOrDefault();
                var amount = Int32.Parse(walletUser.male_usd) + Int32.Parse(data.reward_amount);
                walletUser.male_usd = amount.ToString();

                _context.Wallet.Update(walletUser);

                await _context.SaveChangesAsync();
                return "Success";
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }

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
                    ).OrderBy(x => x.Created_At).ToList();
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
                    ).OrderBy(x => x.created_at).ToList();
                if (result != null)
                    return result;

                return new List<ResTransactionHistoryDto>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
