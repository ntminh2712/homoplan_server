using SeminarAPI.Data.Dto;
using System.Threading.Tasks;

namespace SeminarAPI.Repositories.Interface
{
	public interface ILoginService
	{
		Task<(UserDto data, string response)> Login(UserLoginDto data);
		Task<string> Register(RegisterDto data);
		Task<(string Success, bool Errors)> CheckUrlToken(string data);
	}
}
