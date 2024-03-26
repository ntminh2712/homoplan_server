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
    public interface IChallengeTasksService
    {
        Task<List<ChallengeTasks>> GetAll();
        Task<List<ChallengeTasks>> GetAllChallengeTaskByUser(string userId);
    }
}
