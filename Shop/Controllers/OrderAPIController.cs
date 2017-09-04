using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Interface.Manager;
using Core.Dto;
using Core.Extensions;
using Shop.ResopnseModel;

namespace Shop.Controllers
{
	/// <summary>
	/// Implemets API controller to manage orders
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Produces("application/json")]
	[Route("api/OrderAPI/[action]")]
	public class OrderAPIController : Controller
	{
		/// <summary>
		/// The order manager
		/// </summary>
		private IOrderManager orderManager;

		/// <summary>
		/// The response status
		/// </summary>
		private ResponseStatusModel responseStatus;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderAPIController"/> class.
		/// </summary>
		/// <param name="orderManager">The order manager.</param>
		public OrderAPIController(IOrderManager orderManager)
		{
			this.orderManager = orderManager;
		}

		/// <summary>
		/// Gets the specified order.
		/// </summary>
		/// <param name="id">The identifier.</param>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrder(int id)
		{
			try
			{
				OrderDto order = await this.orderManager.GetOrder(id);

				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus, Order = order });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = true, Message = ex.Message, Code = 404 };
				return NotFound(new { ResponseStatus = this.responseStatus });
			}
		}

		/// <summary>
		/// Gets the order list.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetOrderList()
		{
			string guestID = HttpContext.Session.Get<string>("guestID");
			string userID = (HttpContext.Session.Get<UserDto>("current-user"))?.ID;
			OrderDto[] orders = await this.orderManager.GetOrderList(userID, guestID);

			if (orders == null || orders.Length == 0)
				return NoContent();

			this.responseStatus = new ResponseStatusModel { Success = true };
			return Ok(new { ResponseStatus = this.responseStatus, OrderList = orders });
		}
	}
}
