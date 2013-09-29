using System;

namespace Mono.Mlo
{
	public class HavingClause : IQueryElement
	{
		
		public virtual ILogicalCondition Condition {get;set;}
		
		public HavingClause ()
		{
		}
		
		public virtual string ToQueryString() {
			return (Condition == null ? "" : "HAVING " + Condition.ToQueryString ());
		}
	}
}

