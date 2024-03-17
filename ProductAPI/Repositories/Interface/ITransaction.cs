using SeminarAPI.Data.Dto;
using SeminarAPI.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeminarAPI.Repositories.Interface
{
	public interface ITransaction
    {
        Task<List<ResTransactionHistoryDto>> GetRewardDailyByUser(string userId);
        Task<WalletDto> GetWalletByUser(string userId);
        Task<List<ResRankingDto>> GetRanking();
        Task<string> JobRanking();
        Task<string> CreatedTransaction(TransactionHistoryDto data);
    }
}
