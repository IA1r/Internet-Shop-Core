using Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
	/// <summary>
	/// Represents methods for OrderRepository. 
	/// </summary>
	public interface IOrderRepository
	{
		/// <summary>
		/// Gets the order.
		/// </summary>
		/// <param name="orderID">The order identifier.</param>
		/// <returns>Order Dto</returns>
		Task<OrderDto> GetOrder(int orderID);

		/// <summary>
		/// Gets the order list for specified user.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		/// <param name="guestID">The guest identifier.</param>
		/// <returns>Order list</returns>
		Task<OrderDto[]> GetOrderList(string userID, string guestID);

		/// <summary>
		/// Gets the order list of all users.
		/// </summary>
		/// <returns>Order list</returns>
		Task<OrderDto[]> GetOrderList();

		/// <summary>
		/// Confirms the specified order.
		/// </summary>
		/// <param name="orderID">The order identifier.</param>
		Task ConfirmOrder(int orderID);


		/// <summary>
		/// Deletes the specified order.
		/// </summary>
		/// <param name="orderID">The order identifier.</param>
		Task DeleteOrder(int orderID);
	}
}
