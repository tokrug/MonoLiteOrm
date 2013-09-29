using System;

namespace Mono.Mlo
{
	/// <summary>
	/// Main query object. Without it subqueries are not available.
	/// </summary>
	public class Query : IQueryElement
	{
		
		public virtual SelectClause Select {get;set;}
		public virtual WhereClause Where {get;set;}
		public virtual FromClause From {get;set;}
		public virtual OrderClause Order {get;set;}
		
		public Query ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			return Select.ToQueryString() + " " + From.ToString() + " " + Where.ToQueryString() + " " + Order.ToQueryString() + ";";
		}
	}
}

