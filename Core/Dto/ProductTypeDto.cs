using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
	/// <summary>
	/// Product Type data transfer object
	/// </summary>
	public class ProductTypeDto
	{
		/// <summary>
		/// Gets or sets the product type identifier.
		/// </summary>
		/// <value>
		/// The product type identifier.
		/// </value>
		public int ProductTypeID { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public string Value { get; set; }
	}
}
