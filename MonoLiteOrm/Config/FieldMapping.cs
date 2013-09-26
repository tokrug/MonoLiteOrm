using System;
using System.Reflection;
using System.Data;

namespace Mono.Mlo
{
	public class FieldMapping
	{	
		public virtual PersistentField Field {get;set;}
		public virtual TableColumn Column {get;set;}
		
		public FieldMapping() {}
		
		public FieldMapping(PersistentField field) {
			this.Field = field;
		}
		
	}
}

