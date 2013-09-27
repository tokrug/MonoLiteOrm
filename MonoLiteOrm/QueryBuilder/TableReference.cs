using System;

namespace Mono.Mlo
{
	public class TableReference
	{
		
		public virtual string Name {get;set;}
		public virtual string Alias {get;set;}
		
		public TableReference ()
		{
		}
		
		public virtual string ToQueryString() {
			return null;
		}
		
	}

}

