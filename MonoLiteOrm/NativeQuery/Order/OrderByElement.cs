using System;

namespace Mono.Mlo
{
	public class OrderByElement
	{
		
		public virtual IQueryExpression Expression {get;set;}
		public virtual SortDirection Direction {get;set;}
		
		public OrderByElement ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			 return Expression.ToQueryString () + " " + directionToString (Direction);
		}
		
		private string directionToString (SortDirection direction) {
			return direction.Equals(SortDirection.ASC) ? "ASC" : "DESC";
		}
		
	}
}

