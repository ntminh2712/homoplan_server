using Microsoft.AspNetCore.Mvc;
using SeminarAPI.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeminarAPI.Repositories.Interface
{
	public interface ITransactionHistory
    {
        Task<List<ResTransactionHistoryDto>> GetRewardDailyByUser(string userId);
        Task<List<ResRankingDto>> GetRanking();
        Task<string> JobRanking();
        Task<string> CreatedTransaction(TransactionHistoryDto data);
    }
}
