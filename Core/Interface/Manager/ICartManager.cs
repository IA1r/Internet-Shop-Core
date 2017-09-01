using Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Manager
{
	public interface ICartManager
	{
		Task<ShoppingCartDto> GetShoppingCartProducts(string userID);
		Task AddProduct(int productID, string userID);
		Task DeleteItem(int productID);
		Task Checkout(OrderDto order);
	}
}
