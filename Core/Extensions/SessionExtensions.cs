using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Core.Extensions
{
	/// <summary>
	/// Extends  session with get & set methods
	/// </summary>
	public static class SessionExtensions
	{
		/// <summary>
		/// Sets the specified key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="session">The session.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public static void Set<T>(this ISession session, string key, T value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		/// <summary>
		/// Gets the specified key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="session">The session.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static T Get<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value == null ? default(T) :
								  JsonConvert.DeserializeObject<T>(value);
		}
	}
}
