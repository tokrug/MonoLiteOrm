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
			string result = "";
			if (orderBy.Count > 0) {
				List<string> sorts = new List<string>();
				foreach (OrderByElement el in orderBy) {
					sorts.Add (el.ToQueryString ());	
				}
				result = "ORDER BY " + String.Join (", ", sorts.ToArray());
			}
			return result;
		}
	}
}

