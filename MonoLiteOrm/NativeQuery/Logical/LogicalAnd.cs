using System;
using System.Text;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class LogicalAnd : ILogicalCondition
	{
		
		private List<ILogicalCondition> conditions;
		
		public List<ILogicalCondition> Conditions {get{return this.conditions;}}
		
		public LogicalAnd ()
		{
			this.conditions = new List<ILogicalCondition>();
		}
		
		public LogicalAnd (params ILogicalCondition[] conditions) {
			this.conditions = new List<ILogicalCondition> (conditions);
		}
		
		public string ToQueryString() {
			StringBuilder builder = new StringBuilder();
			builder.Append ("(");
			this.conditions.ForEach((x) => builder.Append(x.ToQueryString() + " AND "));
			builder.Remove (builder.Length - 5,5);
			builder.Append (")");
			return builder.ToString ();
		}
		
	}
}

