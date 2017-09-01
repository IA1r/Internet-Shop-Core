using Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Manager
{
	public interface IOrderManager
	{
		Task<OrderDto> GetOrder(int orderID);
		Task<OrderDto[]> GetOrderList(string userID, string guestID);
		Task<OrderDto[]> GetOrderList();
		Task ConfirmOrder(int orderID);
		Task DeleteOrder(int orderID);
	}
}
