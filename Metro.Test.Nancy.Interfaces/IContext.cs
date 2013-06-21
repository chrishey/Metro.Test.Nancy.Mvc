using System;

namespace Metro.Test.Nancy.Interfaces
{
	///<summary>
	/// Interface for the Context used in the application
	///</summary>
	public interface IContext
	{
		string ConnectionString { get; }

		bool IsCachingEnabled { get; }

		dynamic GetContext();

		dynamic BeginTransaction();

		void CommitTransactions();

		void RollbackTransactions();

		object GetFromCache(string key, ICache cache);

		void AddToCache(string key, object obj, DateTime expiry, ICache cache);
	}
}