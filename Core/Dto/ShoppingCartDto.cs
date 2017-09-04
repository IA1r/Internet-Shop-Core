using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
	/// <summary>
	/// ShoppingCart data transfer object
	/// </summary>
	public class ShoppingCartDto
	{
		/// <summary>
		/// Gets or sets the shopping cart identifier.
		/// </summary>
		/// <value>
		/// The shopping cart identifier.
		/// </value>
		public string ShoppingCartID { get; set; }

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the user phone.
		/// </summary>
		/// <value>
		/// The phone.
		/// </value>
		public string Phone { get; set; }

		/// <summary>
		/// Gets or sets the total price of shopping cart.
		/// </summary>
		/// <value>
		/// The total price.
		/// </value>
		public double TotalPrice { get; set; }

		/// <summary>
		/// Gets or sets the products.
		/// </summary>
		/// <value>
		/// The products.
		/// </value>
		public ProductDto[] Products { get; set; }
	}
}
