using Metro.Test.Nancy.Interfaces;

namespace Metro.Test.Nancy.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IContext _context;

		public UnitOfWork(IContext context)
		{
			_context = context;
		}

		public void SaveChanges()
		{
			_context.CommitTransactions();
		}

		public void RollbackChanges()
		{
			_context.RollbackTransactions();
		}
	}
}
