namespace Metro.Test.Nancy.Interfaces
{

	/// <summary>
	/// Interface for a Unit Of Work pattern
	/// </summary>
	public interface IUnitOfWork
	{
		void SaveChanges();
		void RollbackChanges();
	}
}
