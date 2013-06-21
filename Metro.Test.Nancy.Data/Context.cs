using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Metro.Test.Nancy.Interfaces;
using Simple.Data;

namespace Metro.Test.Nancy.Data
{
	/// <summary>
	/// Implementation of the Context interface giving access to database connection, cache and transactional support
	/// </summary>
	public class Context : IContext
	{
		private List<SimpleTransaction> _transactions;
		private dynamic _db;

		private bool? _isCachingEnabled;
		private string _connectionString;

		public string ConnectionString
		{
			get
			{
				if (string.IsNullOrEmpty(_connectionString))
				{
					_connectionString =
						ConfigurationManager.ConnectionStrings["Simple.Data.Properties.Settings.DefaultConnectionString"].ConnectionString;
				}

				return _connectionString;
			}
		}

		public bool IsCachingEnabled
		{
			get
			{
				if (_isCachingEnabled != null)
				{
					return _isCachingEnabled.Value;
				}

				bool isCachingEnabled;

				if (bool.TryParse(ConfigurationManager.AppSettings["Context.IsCachingEnabled"], out isCachingEnabled))
				{
					_isCachingEnabled = isCachingEnabled;
				}

				return _isCachingEnabled ?? false;
			}
		}

		public Context()
		{
			_transactions = new List<SimpleTransaction>();
			_db = Database.Open();
		}

		public dynamic GetContext()
		{
			return _db;
		}

		public dynamic BeginTransaction()
		{
			var transaction = _db.BeginTransaction();
			_transactions.Add(transaction);

			return transaction;
		}

		public void CommitTransactions()
		{
			foreach (var transaction in _transactions)
			{
				transaction.Commit();
			}

			_transactions.Clear();
		}

		public void RollbackTransactions()
		{
			foreach (var transaction in _transactions)
			{
				transaction.Rollback();
			}

			_transactions.Clear();
		}

		public object GetFromCache(string key, ICache cache)
		{
			return cache.Get(key);
		}

		public void AddToCache(string key, object obj, DateTime expiry, ICache cache)
		{
			cache.Insert(key, obj, expiry);
		}
	}
}
