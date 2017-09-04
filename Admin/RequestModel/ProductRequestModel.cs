using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.RequestModel
{
	/// <summary>
	/// Product Request Model
	/// </summary>
	public class ProductRequestModel
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
		/// Gets or sets the product characteristic.
		/// </summary>
		/// <value>
		/// The characteristic.
		/// </value>
		public Dictionary<string, string> Characteristic { get; set; }
	}
}
