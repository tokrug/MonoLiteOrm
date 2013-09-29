using System;

namespace Mono.Mlo
{
	/// <summary>
	/// Number literal. Use with caution. Type argument can't be limited to primite number types as intended.
	/// </summary>
	public class NumberLiteral<T> : IQueryExpression
	{
		
		public virtual T Value {get;set;}
		
		public NumberLiteral ()
		{
		}
		
		public virtual string ToQueryString() {
			return Value.ToString ();
		}
	}
}

