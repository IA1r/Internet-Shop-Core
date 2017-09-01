using Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
	public interface ICartRepository
	{
		Task<ShoppingCartDto> GetShoppingCartProducts(string id);
		Task AddProduct(int productID, string userID);
		Task DeleteItem(int productID);
		Task Checkout(OrderDto order);
	}
}
