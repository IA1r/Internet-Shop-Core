using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
	/// <summary>
	/// Order data transfer object
	/// </summary>
	public class OrderDto
	{
		/// <summary>
		/// Gets or sets the order identifier.
		/// </summary>
		/// <value>
		/// The order identifier.
		/// </value>
		public int OrderID { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public string UserID { get; set; }

		/// <summary>
		/// Gets or sets the guest identifier.
		/// </summary>
		/// <value>
		/// The guest identifier.
		/// </value>
		public string GuestID { get; set; }

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
		/// Gets or sets the delivery address.
		/// </summary>
		/// <value>
		/// The delivery address.
		/// </value>
		public string DeliveryAddress { get; set; }

		/// <summary>
		/// Gets or sets the total price of order.
		/// </summary>
		/// <value>
		/// The total price.
		/// </value>
		public double TotalPrice { get; set; }

		/// <summary>
		/// Gets or sets the checkout date.
		/// </summary>
		/// <value>
		/// The date.
		/// </value>
		public DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is approve/confirmed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is approve/confirmed; otherwise, <c>false</c>.
		/// </value>
		public bool IsApprove { get; set; }

		/// <summary>
		/// Gets or sets the products of order.
		/// </summary>
		/// <value>
		/// The products array.
		/// </value>
		public ProductDto[] Products { get; set; }
	}
}
