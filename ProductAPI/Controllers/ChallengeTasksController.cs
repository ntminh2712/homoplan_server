using DocumentFormat.OpenXml.Office2010.ExcelAc;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeminarAPI.Data.Dto;
using SeminarAPI.Data.Model;
using SeminarAPI.Repositories.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeminarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeTasksController : ControllerBase
    {
        private readonly IChallengeTasksService _challengeTasksService;

        public ChallengeTasksController(IChallengeTasksService challengeTasksService)
        {
            _challengeTasksService = challengeTasksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _challengeTasksService.GetAll();
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        //[HttpPost("Created")]
        //public async Task<string> Post([FromForm] DailyTasksDto data)
        //{
        //    if (data == null)
        //    {
        //        return "Request body is empty";
        //    }

        //    var response = await _dailyTasksService.CreatedDailyTask(data);
        //    if (response == null)
        //    {
        //        return "Created false!";
        //    }

        //    return response;
        //}

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
