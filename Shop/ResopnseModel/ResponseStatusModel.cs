using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ResopnseModel
{
    public class ResponseStatusModel
    {
		public bool Success { get; set; }
		public string Message { get; set; } = null;
		public int Code { get; set; } = 200;
	}
}
