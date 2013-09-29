using System;

namespace Mono.Mlo
{
	public class TableReference : ITableExpression
	{
		
		public virtual string Name {get;set;}
		public virtual string Alias {get;set;}
		
		public TableReference ()
		{
		}
		
		public virtual string ToQueryString() {
			return Name + (Alias == null ? "" : " " + Alias);
		}
		
	}

}

