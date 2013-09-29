using System;

namespace Mono.Mlo
{
	/// <summary>
	/// Main query object. Without it subqueries are not available.
	/// </summary>
	public class Query : IQueryElement
	{
		
		public virtual SelectClause Select {get;set;}
		public virtual FromClause From {get;set;}
		public virtual WhereClause Where {get;set;}
		public virtual GroupByClause GroupBy {get;set;}
		public virtual HavingClause Having {get;set;}
		public virtual OrderClause Order {get;set;}
		
		public Query ()
		{
			this.Select = new SelectClause();
			this.From = new FromClause();
			this.Where = new WhereClause();
			this.GroupBy = new GroupByClause();
			this.Having = new HavingClause();
			this.Order = new OrderClause();
		}
		
		public virtual string ToQueryString ()
		{
			string whereString = Where.ToQueryString ();
			whereString = (whereString == "" ? "" : " " + whereString);
			string orderString = Order.ToQueryString ();
			orderString = (orderString == "" ? "" : " " + orderString);
			string groupByString = GroupBy.ToQueryString ();
			groupByString = (groupByString == "" ? "" : " " + groupByString);
			string having = Having.ToQueryString ();
			having = (having == "" ? "" : " " + having);
			
			return Select.ToQueryString() + " " + From.ToQueryString() + whereString + groupByString + having + orderString + ";";
		}
	}
}

