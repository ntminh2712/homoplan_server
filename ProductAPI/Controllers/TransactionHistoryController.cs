﻿using DocumentFormat.OpenXml.Office2010.ExcelAc;
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
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistory _transactionHistory;

        public TransactionHistoryController(ITransactionHistory transactionHistory)
        {
            _transactionHistory = transactionHistory;
        }

        [HttpGet("GetRewardDailyByUser")]
        public async Task<IActionResult> GetRewardDailyByUser(string userId)
        {
            var response = await _transactionHistory.GetRewardDailyByUser(userId);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("GetRanking")]
        public async Task<IActionResult> GetRanking()
        {
            var response = await _transactionHistory.GetRanking();
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("CreatedTransaction")]
        public async Task<string> CreatedTransaction([FromForm] TransactionHistoryDto data)
        {
            if (data == null)
            {
                return "Request body is empty";
            }

            var response = await _transactionHistory.CreatedTransaction(data);
            if (response == null)
            {
                return "Created false!";
            }

            return response;
        }

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