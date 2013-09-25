using System;

namespace Mono.Mlo
{
	public class EqualCondition
	{
		public string ColumnName {get;set;}
		public string EqualTo {get;set;}
		
		public EqualCondition ()
		{
		}
		
		public override string ToString ()
		{
			return ColumnName + " = " + EqualTo;
		}
	}
}

