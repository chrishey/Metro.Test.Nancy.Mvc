using System;

namespace Metro.Test.Nancy.Interfaces
{
	/// <summary>
	/// Interface for a cache helper
	/// </summary>
	public interface ICache
	{
		// generics
		void Insert<T>(string key, T value, DateTime absoluteExpiration) where T : class;
		void Insert<T>(string key, T value, TimeSpan slidingExpiration) where T : class;
		T Get<T>(string key) where T : class;

		// objects
		void Insert(string key, object value, DateTime absoluteExpiration);
		void Insert(string key, object value, TimeSpan slidingExpiration);
		object Get(string key);

		// removal
		void Remove(string key);
		void Clear();

		// indexer
		object this[string key] { get; set; }

		// get/insert
		T Fetch<T>(string key, Func<T> getData) where T : class;
		T Fetch<T>(string key, Func<T> getData, DateTime absoluteExpiration) where T : class;
		T Fetch<T>(string key, Func<T> getData, TimeSpan slidingExpiration) where T : class;
	}
}
