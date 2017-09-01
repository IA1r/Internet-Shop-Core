using Core.Dto;
using Core.Interface.Manager;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager
{
	public class CartManager : ICartManager
	{
		private ICartRepository cartRepository;
		public CartManager(ICartRepository cartRepository)
		{
			this.cartRepository = cartRepository;
		}

		public async Task<ShoppingCartDto> GetShoppingCartProducts(string userID)
		{
			if (string.IsNullOrWhiteSpace(userID))
				throw new ArgumentException("Shopping Cart is empty.");

			return await this.cartRepository.GetShoppingCartProducts(userID);
		}

		public async Task AddProduct(int productID, string userID)
		{
			if (string.IsNullOrWhiteSpace(userID))
				throw new ArgumentException("User not found");

			await this.cartRepository.AddProduct(productID, userID);
		}

		public async Task DeleteItem(int productID)
		{
			await this.cartRepository.DeleteItem(productID);
		}

		public async Task Checkout(OrderDto order)
		{
			if(string.IsNullOrWhiteSpace(order.GuestID) && string.IsNullOrWhiteSpace(order.UserID))
				throw new ArgumentException("User not found");

			await this.cartRepository.Checkout(order);
		}
	}
}
