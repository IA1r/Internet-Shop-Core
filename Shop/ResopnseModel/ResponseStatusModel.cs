﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ResopnseModel
{
	/// <summary>
	/// Response Status Model
	/// </summary>
	public class ResponseStatusModel
    {
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ResponseStatusModel"/> is success.
		/// </summary>
		/// <value>
		///   <c>true</c> if success; otherwise, <c>false</c>.
		/// </value>
		public bool Success { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public string Message { get; set; } = null;

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		public int Code { get; set; } = 200;
	}
}
