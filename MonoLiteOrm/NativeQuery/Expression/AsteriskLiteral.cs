using System;

namespace Mono.Mlo
{
	public class AsteriskLiteral : IQueryExpression
	{
		
		public AsteriskLiteral ()
		{
		}
		
		public virtual string ToQueryString() {
			return "*";	
		}
	}
}

