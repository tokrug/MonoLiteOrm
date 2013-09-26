using System;

namespace Mono.Mlo
{
	public class DeleteStatementBuilder
	{
		
		public virtual string TableName {get;set;}
		public virtual WhereClause Where {get;set;}
		
		public DeleteStatementBuilder ()
		{
		}
		
		public override string ToString ()
		{
			return "DELETE FROM " + TableName + " " + Where.ToString() + ";";
		}
		
	}
}

