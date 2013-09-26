using System;

namespace Mono.Mlo
{
	public class Transaction
	{
		
		private System.Data.IDbTransaction transaction;
		
		public Transaction (System.Data.IDbTransaction transaction)
		{
			this.transaction = transaction;
		}
		
		public virtual void commit() {
			transaction.Commit();	
		}
		
		public virtual void rollback() {
			transaction.Rollback();	
		}
	}
}

