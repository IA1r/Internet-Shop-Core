using Core.Dto;
using Core.Interface.UoW;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Interface.Repository;

namespace Data.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private IUnitOfWork unitOfWork;

		public OrderRepository(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<OrderDto> GetOrder(int orderID)
		{
			OrderDto order = await this.unitOfWork.Set<Order>()
				.Where(or => or.OrderID == orderID)
				.Select(or => new OrderDto
				{
					OrderID = or.OrderID,
					UserName = or.UserName,
					Phone = or.Phone,
					DeliveryAddress = or.DeliveryAddress,
					TotalPrice = or.TotalPrice,
					Date = or.Date,
					IsApprove = or.IsApprove,
					Products = or.OrderContents.Select(p => new ProductDto
					{
						ProductID = p.ProductID,
						Type = p.Product.ProductType.Value,
						Characteristic = p.Product.ProductCharacteristics
						.Select(pc => new
						{
							Key = pc.Characteristic.Name,
							Value = pc.Value
						}).ToDictionary(d => d.Key, d => d.Value)
					}).ToArray()
				}).SingleOrDefaultAsync();

			return order;
		}

		public async Task<OrderDto[]> GetOrderList(string userID, string guestID)
		{
			OrderDto[] orders = await this.unitOfWork.Set<Order>()
				.Where(or => or.UserID == userID && or.GuestID == guestID)
				.Select(or => new OrderDto
				{
					OrderID = or.OrderID,
					Date = or.Date,
					IsApprove = or.IsApprove
				}).ToArrayAsync();

			return orders;
		}

		public async Task<OrderDto[]> GetOrderList()
		{
			OrderDto[] orders = await this.unitOfWork.Set<Order>()
				.Select(or => new OrderDto
				{
					OrderID = or.OrderID,
					Date = or.Date,
					IsApprove = or.IsApprove,
					GuestID = or.GuestID,
					UserID = or.UserID
				}).ToArrayAsync();

			return orders;
		}

		public async Task ConfirmOrder(int orderID)
		{
			if (!await this.unitOfWork.Set<Order>().AnyAsync(o => o.OrderID == orderID))
				throw new ArgumentException("Order not found");

			Order order = await this.unitOfWork.Set<Order>().FindAsync(orderID);
			order.IsApprove = true;

			await this.unitOfWork.SaveAsync();
		}

		public async Task DeleteOrder(int orderID)
		{
			if (!await this.unitOfWork.Set<Order>().AnyAsync(o => o.OrderID == orderID))
				throw new ArgumentException("Order not found");

			OrderContent[] content = await this.unitOfWork.Set<OrderContent>()
				.Where(oc => oc.OrderID == orderID)
				.ToArrayAsync();

			foreach (OrderContent item in content)
			{
				this.unitOfWork.Set<OrderContent>().Remove(item);
			}

			await this.unitOfWork.SaveAsync();

			this.unitOfWork.Set<Order>().Remove(new Order { OrderID = orderID });
			await this.unitOfWork.SaveAsync();

		}
	}
}
