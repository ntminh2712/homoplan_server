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
using Irony.Parsing;

namespace SeminarAPI.Repositories.Implementation
{
    /// <summary>
    /// OrderService Implementation
    /// </summary>
    public class DailyTasksService : IDailyTasksService
    {
        /// <summary>
        /// _context
        /// </summary>
        protected SeminarDbContext _context;

        /// <summary>
        /// DietService
        /// </summary>
        /// <param name="context"></param>
        public DailyTasksService(SeminarDbContext context)
        {
            _context = context;
        }

        public async Task<List<DailyTasks>> GetAll()
        {
            try
            {
                var result = await _context.DailyTasks.ToListAsync();
                if (result != null)
                    return result;

                return new List<DailyTasks>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> SuccessDailyTask(string userId, string dailytask_id)
        {
            try
            {
                var getDailyTask = await _context.DailyTasks.Where(x => x.daily_tasks_id == dailytask_id).FirstOrDefaultAsync();
                if (getDailyTask == null)
                {
                    return "Không tìm thấy mã nhiệm vụ, vui lòng thử lại.";
                }

                if(getDailyTask != null)
                {
                    if(getDailyTask.status == 1)
                    {
                        return "Nhiệm vụ đã hoàn thành, vui lòng tải lại trang.";
                    }
                }


                getDailyTask.status = 1;
                _context.DailyTasks.Update(getDailyTask);

                var getWalletByUser = await _context.Wallet.Where(x => x.user_id == userId).FirstOrDefaultAsync();
                if(getWalletByUser == null)
                {
                    return "Không tìm thấy id người dùng";
                }

                getWalletByUser.male_usd = Convert.ToString(Convert.ToInt32(getWalletByUser.male_usd) +  Convert.ToInt32(getDailyTask.reward_amount));
                _context.Wallet.Update(getWalletByUser);

                TransactionHistory transactionHistory = new TransactionHistory();
                transactionHistory.transaction_history_id = Guid.NewGuid().ToString();
                transactionHistory.user_id = userId;
                transactionHistory.reward_amount = getDailyTask.reward_amount;
                transactionHistory.daily_tasks_id = getDailyTask.daily_tasks_id;
                transactionHistory.type = "1";
                await _context.TransactionHistory.AddAsync(transactionHistory);

                _context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
