using SeminarAPI.Data.Dto;
using SeminarAPI.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeminarAPI.Repositories.Interface
{
	public interface ITransaction
    {
        Task<List<ResTransactionHistoryDto>> GetRewardDailyByUser(string userId);
        Task<List<DailyTasks>> GetDailyTasksByUser(string userId);
        Task<WalletDto> GetWalletByUser(string userId);
        Task<ResRankingUser> GetRanking(string userId);
        Task<string> JobRanking();
        Task<bool> UpdateRanking();
        //Task<string> CreatedTransaction(RequestTransactionDto data);
    }
}
