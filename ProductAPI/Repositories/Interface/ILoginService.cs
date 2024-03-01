using SeminarAPI.Data.Dto;
using System.Threading.Tasks;

namespace SeminarAPI.Repositories.Interface
{
	public interface ILoginService
	{
		Task<(UserDto data, string response)> Login(UserDto data);
		Task<string> Register(UserDto data);
		Task<(string Success, bool Errors)> CheckUrlToken(string data);
	}
}
