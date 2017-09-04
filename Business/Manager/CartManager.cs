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
	/// Implements functionality to manage shopping cart
	/// </summary>
	/// <seealso cref="Core.Interface.Manager.ICartManager" />
	public class CartManager : ICartManager
	{
		/// <summary>
		/// The shopping cart repository
		/// </summary>
		private ICartRepository cartRepository;
		/// <summary>
		/// Initializes a new instance of the <see cref="CartManager"/> class.
		/// </summary>
		/// <param name="cartRepository">The cart repository.</param>
		public CartManager(ICartRepository cartRepository)
		{
			this.cartRepository = cartRepository;
		}

		/// <summary>
		/// Gets the shopping cart products.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		/// <returns>Shopping Cart Dto</returns>
		/// <exception cref="ArgumentException">Shopping Cart is empty.</exception>
		public async Task<ShoppingCartDto> GetShoppingCartProducts(string userID)
		{
			if (string.IsNullOrWhiteSpace(userID))
				throw new ArgumentException("Shopping Cart is empty.");

			return await this.cartRepository.GetShoppingCartProducts(userID);
		}

		/// <summary>
		/// Adds the product to shopping cart.
		/// </summary>
		/// <param name="productID">The product identifier.</param>
		/// <param name="userID">The user/cart identifier.</param>
		/// <exception cref="ArgumentException">User not found</exception>
		public async Task AddProduct(int productID, string userID)
		{
			if (string.IsNullOrWhiteSpace(userID))
				throw new ArgumentException("User not found");

			await this.cartRepository.AddProduct(productID, userID);
		}

		/// <summary>
		/// Deletes the item from shopping cart.
		/// </summary>
		/// <param name="cartContentID">The cart item identifier.</param>
		public async Task DeleteItem(int productID)
		{
			await this.cartRepository.DeleteItem(productID);
		}

		/// <summary>
		/// Checkouts from shopping cart.
		/// </summary>
		/// <param name="order">The order.</param>
		/// <exception cref="ArgumentException">User not found</exception>
		public async Task Checkout(OrderDto order)
		{
			if (string.IsNullOrWhiteSpace(order.GuestID) && string.IsNullOrWhiteSpace(order.UserID))
				throw new ArgumentException("User not found");

			await this.cartRepository.Checkout(order);
		}
	}
}
