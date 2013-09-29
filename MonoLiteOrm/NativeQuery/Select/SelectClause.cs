using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class SelectClause : IQueryElement
	{
		
		private List<SelectColumn> selectedColumns = new List<SelectColumn>();
		
		public virtual List<SelectColumn> SelectedColumns {get{return this.selectedColumns;}}
		
		public SelectClause ()
		{
		}
		
		public virtual string ToQueryString() {
			List<string> selected = new List<string>();
			foreach (SelectColumn col in this.selectedColumns) {
				selected.Add (col.ToQueryString ());	
			}
			return "SELECT " + String.Join (", ", selected.ToArray());
		}
	}
}

