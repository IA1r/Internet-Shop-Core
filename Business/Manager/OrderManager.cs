using Core.Dto;
using Core.Interface.Manager;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager
{
	public class OrderManager : IOrderManager
	{
		private IOrderRepository orderRepository;
		public OrderManager(IOrderRepository orderRepository)
		{
			this.orderRepository = orderRepository;
		}

		public async Task<OrderDto> GetOrder(int orderID)
		{
			if (orderID <= 0)
				throw new ArgumentException("Invalid order ID");

			return await this.orderRepository.GetOrder(orderID);
		}
		public async Task<OrderDto[]> GetOrderList(string userID, string guestID)
		{
			if (string.IsNullOrWhiteSpace(userID) && string.IsNullOrWhiteSpace(guestID))
				return null;

			return await this.orderRepository.GetOrderList(userID, guestID);
		}

		public async Task<OrderDto[]> GetOrderList()
		{
			return await this.orderRepository.GetOrderList();
		}

		public async Task ConfirmOrder(int orderID)
		{
			if (orderID <= 0)
				throw new ArgumentException("Invalid order id");

			await this.orderRepository.ConfirmOrder(orderID);
		}

		public async Task DeleteOrder(int orderID)
		{
			await this.orderRepository.DeleteOrder(orderID);
		}
	}
}
