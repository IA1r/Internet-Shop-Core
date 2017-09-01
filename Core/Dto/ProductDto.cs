using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
	public class ProductDto
	{
		public int ProductID { get; set; }
		public int ProductTypeID { get; set; }
		public int CartContentID { get; set; }
		public string Type { get; set; }
		public Dictionary<string,string> Characteristic { get; set; }
	}
}
