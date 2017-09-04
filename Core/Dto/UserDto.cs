using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dto
{
	/// <summary>
	/// User data transfer object
	/// </summary>
	public class UserDto
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>
		/// The country.
		/// </value>
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the user phone.
		/// </summary>
		/// <value>
		/// The phone number.
		/// </value>
		public string Phone { get; set; }

		/// <summary>
		/// Gets or sets the year of birth.
		/// </summary>
		/// <value>
		/// The year.
		/// </value>
		public string Year { get; set; }
	}
}
