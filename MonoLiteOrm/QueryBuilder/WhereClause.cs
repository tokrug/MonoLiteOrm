using System;

namespace Mono.Mlo
{
	public class WhereClause
	{
		
		public virtual EqualCondition Equality {get;set;}
		
		public WhereClause ()
		{
		}
		
		public override string ToString ()
		{
			return Equality.ToString ();
		}
	}
}

