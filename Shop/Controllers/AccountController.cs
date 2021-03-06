using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.RequestModel;
using Microsoft.AspNetCore.Identity;
using Core.Model;
using Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Core.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shop.ResopnseModel;

namespace Shop.Controllers
{
	/// <summary>
	/// Implemets API controller to manage users
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	public class AccountController : Controller
	{
		/// <summary>
		/// The user manager
		/// </summary>
		private readonly UserManager<User> userManager;

		/// <summary>
		/// The sign in manager
		/// </summary>
		private readonly SignInManager<User> signInManager;

		/// <summary>
		/// The role manager
		/// </summary>
		private readonly RoleManager<IdentityRole> roleManager;

		/// <summary>
		/// The response status
		/// </summary>
		private ResponseStatusModel responseStatus;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		/// <param name="userManager">The user manager.</param>
		/// <param name="signInManager">The sign in manager.</param>
		/// <param name="roleManager">The role manager.</param>
		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
		}

		/// <summary>
		/// Registrations the specified user.
		/// </summary>
		/// <param name="model">The model.</param>
		[HttpPost]
		public async Task<IActionResult> Registration([FromBody]RegistrationRequestModel model)
		{
			if (await this.userManager.FindByNameAsync(model.Login) != null)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = "This login already use.", Code = 409 };
				return StatusCode(409, new { ResponseStatus = this.responseStatus });
			}

			if (await this.userManager.FindByEmailAsync(model.Email) != null)
			{
				this.responseStatus = new ResponseStatusModel { Success = false, Message = "This email already use.", Code = 409 };
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
				await userManager.AddToRoleAsync(user, "user");

				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus });
			}

			this.responseStatus = new ResponseStatusModel { Success = false, Code = 400 };
			return BadRequest(new { ResponseStatus = this.responseStatus });
		}

		/// <summary>
		/// Authorization the specified user.
		/// </summary>
		/// <param name="model">The model.</param>
		[HttpPost]
		public async Task<IActionResult> SignIn([FromBody]SignInRequestModel model)
		{
			var result = await this.signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
			if (result.Succeeded)
			{

				User user = await this.userManager.FindByNameAsync(model.Login);
				UserDto userDto = new UserDto
				{
					ID = user.Id,
					Name = user.UserName,
					Email = user.Email,
					Country = user.Country,
					Phone = user.PhoneNumber,
					Year = user.Year
				};

				HttpContext.Session.Remove("guestID");
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

		/// <summary>
		/// Determines whether user is authenticated.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult IsAuthenticated()
		{
			if (HttpContext.User.Identity.IsAuthenticated)
				return Ok(new { UserName = HttpContext.Session.Get<UserDto>("current-user").Name, IsAuthenticated = true });
			else
				return Ok(new { UserName = "Guest", IsAuthenticated = false });
		}

		/// <summary>
		/// Represents an event that is raised when the sign-out operation is complete.
		/// </summary>
		[Authorize]
		[HttpGet]
		public async Task SignOut()
		{
			HttpContext.Session.Remove("current-user");
			await signInManager.SignOutAsync();
		}

	}
}
