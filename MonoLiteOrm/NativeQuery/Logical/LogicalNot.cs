using System;

namespace Mono.Mlo
{
	public class LogicalNot : ILogicalCondition
	{
		public virtual ILogicalCondition Condition {get;set;}
		
		public LogicalNot ()
		{
		}
		
		public virtual string ToQueryString() {
			return "NOT " + Condition.ToQueryString();	
		}
	}
}

