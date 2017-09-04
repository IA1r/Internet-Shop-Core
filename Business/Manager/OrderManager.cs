using Core.Dto;
using Core.Interface.Manager;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager
{
	/// <summary>
	/// Implements functionality to manage order.
	/// </summary>
	/// <seealso cref="Core.Interface.Manager.IOrderManager" />
	public class OrderManager : IOrderManager
	{
		/// <summary>
		/// The order repository
		/// </summary>
		private IOrderRepository orderRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderManager"/> class.
		/// </summary>
		/// <param name="orderRepository">The order repository.</param>
		public OrderManager(IOrderRepository orderRepository)
		{
			this.orderRepository = orderRepository;
		}

		/// <summary>
		/// Gets the order.
		/// </summary>
		/// <param name="orderID">The order identifier.</param>
		/// <returns>Order Dto</returns>
		/// <exception cref="ArgumentException">Invalid order ID</exception>
		public async Task<OrderDto> GetOrder(int orderID)
		{
			if (orderID <= 0)
				throw new ArgumentException("Invalid order ID");

			return await this.orderRepository.GetOrder(orderID);
		}

		/// <summary>
		/// Gets the order list for specified user.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		/// <param name="guestID">The guest identifier.</param>
		/// <returns>Order list</returns>
		public async Task<OrderDto[]> GetOrderList(string userID, string guestID)
		{
			if (string.IsNullOrWhiteSpace(userID) && string.IsNullOrWhiteSpace(guestID))
				return null;

			return await this.orderRepository.GetOrderList(userID, guestID);
		}

		/// <summary>
		/// Gets the order list of all users.
		/// </summary>
		/// <returns>Order list</returns>
		public async Task<OrderDto[]> GetOrderList()
		{
			return await this.orderRepository.GetOrderList();
		}

		/// <summary>
		/// Confirms the specified order.
		/// </summary>
		/// <param name="orderID">The order identifier.</param>
		/// <exception cref="ArgumentException">Invalid order id</exception>
		public async Task ConfirmOrder(int orderID)
		{
			if (orderID <= 0)
				throw new ArgumentException("Invalid order id");

			await this.orderRepository.ConfirmOrder(orderID);
		}

		/// <summary>
		/// Deletes the specified order.
		/// </summary>
		/// <param name="orderID">The order identifier.</param>
		public async Task DeleteOrder(int orderID)
		{
			await this.orderRepository.DeleteOrder(orderID);
		}
	}
}
