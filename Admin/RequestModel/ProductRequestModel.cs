using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.RequestModel
{
    public class ProductRequestModel
    {
		public int ProductID { get; set; }
		public int ProductTypeID { get; set; }
		public Dictionary<string, string> Characteristic { get; set; }
	}
}
