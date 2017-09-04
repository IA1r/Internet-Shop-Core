using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.RequestModel
{
	/// <summary>
	/// Checkout Request Model
	/// </summary>
	public class CheckoutRequestModel
    {
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
		/// Gets or sets the total price.
		/// </summary>
		/// <value>
		/// The total price.
		/// </value>
		public double TotalPrice { get; set; }

	}
}
