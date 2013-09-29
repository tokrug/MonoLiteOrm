using System;

namespace Mono.Mlo
{
	public class Parameter : IQueryExpression
	{
		public virtual string Name {get;set;}
		
		public Parameter ()
		{
		}
		
		public virtual string ToQueryString() {
			return "@" + Name;	
		}
	}
}

