using SeminarAPI.Data;
using SeminarAPI.Data.Model;
using System.Threading.Tasks;
using System;
using SeminarAPI.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SeminarAPI.Data.Dto;
using SeminarAPI.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Renci.SshNet.Common;

namespace SeminarAPI.Repositories.Implementation
{
	public class LoginService : ILoginService
	{
		/// <summary>
		/// _context
		/// </summary>
		protected SeminarDbContext _context;
        protected readonly IConfiguration _configuration;

        /// <summary>
        /// FoodService
        /// </summary>
        /// <param name="context"></param>
        public LoginService(SeminarDbContext context, IConfiguration configuration)
        {
			_context = context;
            _configuration = configuration;
        }

		/// <summary>
		/// Login
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public async Task<(UserDto data, string response)> Login(UserDto data)
		{
			try
			{
				if(string.IsNullOrEmpty(data.User_Name))
				{
					return (new UserDto(), $"Username cannot be blank");
				}

				if (string.IsNullOrEmpty(data.Password))
				{
					return (new UserDto(), $"Password cannot be blank");
				}

				var getUser = await _context.Users.Where(x => x.User_Name == data.User_Name).FirstOrDefaultAsync();
				if(getUser == null)
				{
					return (new UserDto(), $"Username does not exist"); 
				}

				var verifyPassword= Encryption.VerifyPassword(getUser.Password, data.Password, getUser.Salt);
				if (verifyPassword)
				{
                    var serverUrl = _configuration["AppSettings:UrlServer"];

                    var userReturn = new UserDto();
					userReturn.User_Id = data.User_Id;
					userReturn.User_Name = getUser.User_Name;
					userReturn.avatar_path = serverUrl + "/api/Delegate/get-image/" + System.IO.Path.GetFileName(getUser.avatar);
                    userReturn.Phone = getUser.Phone;
                    userReturn.Email = getUser.Email;
                    userReturn.Full_Name = getUser.Full_Name;
                    userReturn.Country = getUser.Country;
                    userReturn.Reference_Id = getUser.Reference_Id;
                    userReturn.reference_count = getUser.reference_count;
                    userReturn.partner_id = getUser.partner_id;
					userReturn.Created_At = getUser.Created_At;
                    return (userReturn, $"Success");
				}

				return (new UserDto(), $"Login information is incorrect");

			}
			catch (Exception ex)
			{
				return (new UserDto(), ex.ToString());
			}

		}

		/// <summary>
		/// Register
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public async Task<string> Register(UserDto data)
		{
			try
			{
				Users newUser = new Users();
				var idAuto = Guid.NewGuid().ToString();
				if (string.IsNullOrEmpty(data.User_Name))
				{
					return $"Username cannot be blank";
				}

				if (string.IsNullOrEmpty(data.Password))
				{
					return $"Password cannot be blank";
				}

				var getUser = await _context.Users.Where(x => x.User_Name == data.User_Name).FirstOrDefaultAsync();
				if (getUser != null)
				{
					return $"Username already exists";
				}

				string salt = Encryption.GenerateSalt();
                Random rnd = new Random();

                var folderPath = _configuration["AppSettings:FolderPath"];
                var host = _configuration["SshServerConfig:Host"];
                var username = _configuration["SshServerConfig:Username"];
                var password = _configuration["SshServerConfig:Password"];

                var getPath = folderPath + "/Avatar";
                var sshNetHelper = new ServerPathHelper(host, username, password);
                var avatarUpload = data.avatar;
                



                string hashedPassword = Helpers.Encryption.HashPassword(data.Password, salt);
				newUser.User_Id = idAuto;
				newUser.User_Name = data.User_Name.Trim();
				newUser.Password = hashedPassword;
				newUser.Phone = data.Phone;
				newUser.Full_Name = data.Full_Name;
				newUser.Country = data.Country;
				newUser.Email = data.Email;
				newUser.Salt = salt;
				newUser.partner_id = data.partner_id;
				newUser.Reference_Id = rnd.Next().ToString();
                newUser.reference_count = 0;
				
                if (avatarUpload != null)
                {
                    var folderPathDelegate = folderPath + "/Avatar";
                    var avatarName = System.IO.Path.GetFileName(avatarUpload.FileName).Replace(@"\", "");
                    var filePathUpload = System.IO.Path.Combine(getPath + "/", avatarName);

                    var checkFileExists = sshNetHelper.CheckFileExists(filePathUpload);
                    if (checkFileExists)
                    {
                        avatarName = Guid.NewGuid().ToString() + "_" + avatarName;
                        filePathUpload = System.IO.Path.Combine(getPath + "/", avatarName);
                    }
                    var filePathUser = sshNetHelper.UploadFile(avatarUpload, filePathUpload);
                    newUser.avatar = filePathUser;
                }
                

                newUser.Created_At = DateTime.Now;

				var result = await _context.Users.AddAsync(newUser);

				Wallet walletUser = new Wallet();
				walletUser.wallet_id = Guid.NewGuid().ToString();
				walletUser.user_id = idAuto;
				walletUser.female_usd = "0";
                walletUser.male_usd = "0";
                walletUser.amount_usd = "0";
                walletUser.Created_At = DateTime.Now;

                await _context.Wallet.AddAsync(walletUser);

				if (!string.IsNullOrEmpty(data.partner_id))
				{
					var partner = _context.Users.Where(x => x.Reference_Id == data.partner_id).FirstOrDefault();
					if (partner != null)
					{
                        partner.reference_count = partner.reference_count + 1;
                        _context.Users.Update(partner);

						var walletPartner = _context.Wallet.Where(x => x.user_id == partner.User_Id).FirstOrDefault();
						if (walletPartner != null)
						{
                            walletPartner.male_usd = (Int32.Parse(walletPartner.male_usd) + 100).ToString();
                            _context.Wallet.Update(walletPartner);
                        }
                    }
				}

                await _context.SaveChangesAsync();

				return $"Success";

			}
			catch (Exception ex)
			{
				return ex.ToString();
			}

		}

		/// <summary>
		/// Register
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public async Task<(string Success, bool Errors)> CheckUrlToken(string data)
		{
			try
			{
				var checkToken = await _context.Users.Where(x => x.User_Id == data).FirstOrDefaultAsync();
				if(checkToken != null)
				{
					return ("Token hợp lệ", true);
				}
				return ("Token không hợp lệ", false);
			}
			catch (Exception ex)
			{
				return ("Token không hợp lệ", false);
			}
		}
	}
}
