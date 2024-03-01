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

namespace SeminarAPI.Repositories.Implementation
{
    /// <summary>
    /// OrderService Implementation
    /// </summary>
    public class ChallengeTasksService : Interface.IChallengeTasksService
    {
        /// <summary>
        /// _context
        /// </summary>
        protected SeminarDbContext _context;

        /// <summary>
        /// DietService
        /// </summary>
        /// <param name="context"></param>
        public ChallengeTasksService(SeminarDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChallengeTasks>> GetAll()
        {
            try
            {
                var result = await _context.ChallengeTasks.ToListAsync();
                if (result != null)
                    return result;

                return new List<ChallengeTasks>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
