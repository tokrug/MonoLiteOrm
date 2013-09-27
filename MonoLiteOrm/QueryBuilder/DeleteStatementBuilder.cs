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
		
		public virtual string ToQueryString ()
		{
			return "DELETE FROM " + TableName + " " + Where.ToQueryString() + ";";
		}
		
	}
}

