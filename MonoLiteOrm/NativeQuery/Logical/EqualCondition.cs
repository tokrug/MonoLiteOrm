using System;

namespace Mono.Mlo
{
	public class EqualCondition : ILogicalCondition
	{
		public virtual IQueryExpression Expression {get;set;}
		public virtual IQueryExpression EqualsExpression {get;set;}
		
		public EqualCondition ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			return Expression.ToQueryString () + " = " + EqualsExpression.ToQueryString ();
		}
	}
}

