using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Model;
using Admin.RequestModel;
using Core.Dto;
using Microsoft.AspNetCore.Identity;
using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Admin.ResopnseModel;

namespace Admin.Controllers
{
	[Produces("application/json")]
	[Route("api/AccountAPI/[action]")]
	public class AccountAPIController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private ResponseStatusModel responseStatus;
		public AccountAPIController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
		}

		[HttpPost]
		public async Task<IActionResult> Registration([FromBody]RegistrationRequestModel model)
		{
			if (await this.userManager.FindByNameAsync(model.Login) != null)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = "This login already use.", Code = 400 };
				return StatusCode(409, new { ResponseStatus = this.responseStatus });
			}

			if (await this.userManager.FindByEmailAsync(model.Email) != null)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = "This email already use.", Code = 400 };
				return StatusCode(409, new { ResponseStatus = this.responseStatus });
			}

			var result = await this.userManager
				.CreateAsync(new User
				{
					UserName = model.Login,
					Email = model.Email,
					Country = model.Country,
					PhoneNumber = model.Phone,
					Year = model.Year

				}, model.Password);

			if (result.Succeeded)
			{
				User user = await this.userManager.FindByNameAsync(model.Login);
				await userManager.AddToRoleAsync(user, "admin");

				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus });
			}

			this.responseStatus = new ResponseStatusModel { Success = false, Code = 400 };
			return BadRequest(new { ResponseStatus = this.responseStatus });
		}

		[HttpPost]
		public async Task<IActionResult> SignIn([FromBody]SignInRequestModel model)
		{
			User user = await this.userManager.FindByNameAsync(model.Login);
			if (user != null)
				if (!await this.userManager.IsInRoleAsync(user, "admin"))
				{
					this.responseStatus = new ResponseStatusModel { Success = false, Message = "This user is not an administrator", Code = 400 };
					return BadRequest(new { ResponseStatus = this.responseStatus });
				}

			var result = await this.signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
			if (result.Succeeded)
			{
				UserDto userDto = new UserDto
				{
					ID = user.Id,
					Name = user.UserName,
					Email = user.Email,
					Country = user.Country,
					Phone = user.PhoneNumber,
					Year = user.Year
				};
				HttpContext.Session.Set<UserDto>("current-user", userDto);

				this.responseStatus = new ResponseStatusModel { Success = result.Succeeded };
				return Ok(new { ResponseStatus = this.responseStatus });
			}
			else
			{
				this.responseStatus = new ResponseStatusModel { Success = result.Succeeded, Message = "Invalid login or password", Code = 400 };
				return BadRequest(new { ResponseStatus = this.responseStatus });
			}
		}

		[HttpGet]
		public IActionResult IsAuthenticated()
		{
			if (HttpContext.User.Identity.IsAuthenticated)
				return Ok(new { UserName = HttpContext.Session.Get<UserDto>("current-user").Name, IsAuthenticated = true });
			else
				return Ok(new { UserName = "Guest", IsAuthenticated = false });
		}

		[Authorize]
		[HttpGet]
		public async Task SignOut()
		{
			HttpContext.Session.Remove("current-user");
			await signInManager.SignOutAsync();
		}
	}
}
