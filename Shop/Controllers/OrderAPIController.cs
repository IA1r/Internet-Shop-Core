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
	[Produces("application/json")]
	[Route("api/OrderAPI/[action]")]
	public class OrderAPIController : Controller
	{
		private IOrderManager orderManager;
		private ResponseStatusModel responseStatus;
		public OrderAPIController(IOrderManager orderManager)
		{
			this.orderManager = orderManager;
		}

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
