using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class OrderClause
	{
		
		private List<OrderByElement> orderBy = new List<OrderByElement>();
		
		public virtual List<OrderByElement> OrderBy {get{return this.orderBy;}}
		
		public OrderClause ()
		{
		}
		
		public virtual string ToQueryString() {
			if (orderBy.Count < 0) {
				return "";
			} else {
				List<string> sorts = new List<string>();
				foreach (OrderByElement el in orderBy) {
					sorts.Add (el.ToQueryString ());	
				}
				return "ORDER BY " + String.Join (", ", sorts.ToArray());
			}
			
		}
	}
}

