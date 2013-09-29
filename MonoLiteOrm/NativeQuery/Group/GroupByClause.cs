using System;
using System.Text;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class GroupByClause : IQueryElement
	{
		
		private List<IQueryExpression> expressions = new List<IQueryExpression>();
		
		public virtual List<IQueryExpression> Expressions {get{return this.expressions;}}
		
		public GroupByClause ()
		{
		}
		
		public virtual string ToQueryString() {
			string result = "";
			if (expressions.Count > 0) {
				StringBuilder builder = new StringBuilder();
				builder.Append ("GROUP BY ");
				foreach (IQueryExpression exp in this.expressions) {
					builder.Append (exp.ToQueryString() + ", ");	
				}
				builder.Remove (builder.Length-2,2);
				result = builder.ToString ();
			}
			return result;
		}
	}
}

