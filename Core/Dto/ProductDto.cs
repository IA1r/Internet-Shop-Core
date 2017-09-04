using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
	/// <summary>
	/// Product data transfer object
	/// </summary>
	public class ProductDto
	{
		/// <summary>
		/// Gets or sets the product identifier.
		/// </summary>
		/// <value>
		/// The product identifier.
		/// </value>
		public int ProductID { get; set; }

		/// <summary>
		/// Gets or sets the product type identifier.
		/// </summary>
		/// <value>
		/// The product type identifier.
		/// </value>
		public int ProductTypeID { get; set; }

		/// <summary>
		/// Gets or sets the cart content identifier.
		/// </summary>
		/// <value>
		/// The cart content identifier.
		/// </value>
		public int CartContentID { get; set; }

		/// <summary>
		/// Gets or sets the product type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the product characteristics.
		/// </summary>
		/// <value>
		/// The characteristic.
		/// </value>
		public Dictionary<string,string> Characteristic { get; set; }
	}
}
