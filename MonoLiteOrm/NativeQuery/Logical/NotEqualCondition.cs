using System;

namespace Mono.Mlo
{
	public class NotEqualCondition
	{
		public virtual IQueryExpression Expression {get;set;}
		public virtual IQueryExpression NotEqualsExpression {get;set;}
		
		public NotEqualCondition ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			return Expression.ToQueryString () + " != " + NotEqualsExpression.ToQueryString ();
		}
	}
}

