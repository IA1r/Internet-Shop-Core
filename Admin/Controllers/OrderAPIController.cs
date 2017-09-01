using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Interface.Manager;
using Core.Dto;
using Admin.ResopnseModel;

namespace Admin.Controllers
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

		[HttpGet]
		public async Task<IActionResult> GetOrderList()
		{
			OrderDto[] orders = await this.orderManager.GetOrderList();

			if (orders == null || orders.Length == 0)
				return NoContent();

			this.responseStatus = new ResponseStatusModel { Success = true };
			return Ok(new { ResponseStatus = this.responseStatus, OrderList = orders });
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

		[HttpPost("{id}")]
		public async Task<IActionResult> ConfirmOrder(int id)
		{
			try
			{
				await this.orderManager.ConfirmOrder(id);
				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = true, Message = ex.Message, Code = 404 };
				return NotFound(new { ResponseStatus = this.responseStatus });
			}
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			try
			{
				await this.orderManager.DeleteOrder(id);
				this.responseStatus = new ResponseStatusModel { Success = true };
				return Ok(new { ResponseStatus = this.responseStatus });
			}
			catch (ArgumentException ex)
			{
				this.responseStatus = new ResponseStatusModel { Success = true, Message = ex.Message, Code = 404 };
				return NotFound(new { ResponseStatus = this.responseStatus });
			}
		}
	}
}
