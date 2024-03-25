using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeminarAPI.Data.Dto;
using SeminarAPI.Data.Model;

namespace SeminarAPI.Repositories.Interface
{
    /// <summary>
    /// OrderService interface
    /// </summary>
    public interface IDailyTasksService
    {
        Task<List<DailyTasks>> GetAll();
        Task<string> SuccessDailyTask(string userId, string dailytask_id);
    }
}
