using Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
	/// <summary>
	/// Represents methods for CartRepository. 
	/// </summary>
	public interface ICartRepository
	{
		/// <summary>
		/// Gets the shopping cart products.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		/// <returns>Shopping Cart Dto</returns>
		Task<ShoppingCartDto> GetShoppingCartProducts(string id);

		/// <summary>
		/// Adds the product to shopping cart.
		/// </summary>
		/// <param name="productID">The product identifier.</param>
		/// <param name="userID">The user/cart identifier.</param>
		Task AddProduct(int productID, string userID);

		/// <summary>
		/// Deletes the item from shopping cart.
		/// </summary>
		/// <param name="cartContentID">The cart item identifier.</param>
		Task DeleteItem(int productID);

		/// <summary>
		/// Checkouts from shopping cart.
		/// </summary>
		/// <param name="order">The order.</param>
		Task Checkout(OrderDto order);
	}
}
