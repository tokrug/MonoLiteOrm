using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class PhysicalQueryTable
	{
		
		public virtual TableDefinition Table {get;set;}
		public virtual string Alias {get;set;}
		
		public PhysicalQueryTable ()
		{
		}
		
		public virtual TableReference ToTableReference() {
			return From.Table(Table.Name, Alias);
		}
		
		public virtual List<SelectColumn> GetSelectedColumns() {
			List<SelectColumn> selected = new List<SelectColumn>();
			foreach (TableColumn col in Table.getColumns()) {
				selected.Add (Select.Column(Alias, col.Name));	
			}
			return selected;
		}
	}
}

