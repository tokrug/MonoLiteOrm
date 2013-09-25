using System;

namespace Mono.Mlo
{
	public class WhereClause
	{
		
		public EqualCondition Equality {get;set;}
		
		public WhereClause ()
		{
		}
		
		public override string ToString ()
		{
			return Equality.ToString ();
		}
	}
}

