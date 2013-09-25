using System;

namespace Mono.Mlo
{
	public class DeleteStatementBuilder
	{
		
		public string TableName {get;set;}
		public WhereClause Where {get;set;}
		
		public DeleteStatementBuilder ()
		{
		}
		
		public override string ToString ()
		{
			return "DELETE FROM " + TableName + " " + Where.ToString() + ";";
		}
		
	}
}

