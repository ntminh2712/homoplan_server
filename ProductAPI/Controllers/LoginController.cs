using Microsoft.AspNetCore.Mvc;
using SeminarAPI.Data.Dto;
using SeminarAPI.Helpers;
using SeminarAPI.Repositories.Implementation;
using SeminarAPI.Repositories.Interface;
using System.Threading.Tasks;

namespace SeminarAPI.Controllers
{
	/// <summary>
	/// Login API entry point
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly ILoginService _loginService;
		/// <summary>
		/// LoginController Constructor
		/// </summary>
		public LoginController(ILoginService loginService)
		{
			_loginService = loginService;
		}

		/// <summary>
		/// Login 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpPost(template: nameof(Login))]
		public async Task<IActionResult> Login([FromBody] UserDto data)
		{
			if (data == null)
			{
				return BadRequest("Request body is empty");
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var response = await _loginService.Login(data);
			if (response.response == "Success")
			{
				return Ok(response.data);
			}

			return BadRequest(response.response);
		}

		/// <summary>
		/// Register 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpPost(template: nameof(Register))]
		public async Task<IActionResult> Register([FromForm] UserDto data)
		{
			if (data == null)
			{
				return BadRequest("Request body is empty");
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var response = await _loginService.Register(data);
			if (response == "Success")
			{
				return Ok("Success");
			}


			return BadRequest(response);
		}

		/// <summary>
		/// Register 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpGet(template: nameof(CheckUrlToken))]
		public async Task<IActionResult> CheckUrlToken(string data)
		{
			if (data == null)
			{
				return BadRequest("Request body is empty");
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var response = await _loginService.CheckUrlToken(data);
			if (response.Errors)
			{
				return Ok(response.Success);
			}


			return BadRequest(response.Success);
		}
	}
}
