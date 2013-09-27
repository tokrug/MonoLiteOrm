using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Native SQL query builder.
	/// </summary>
	public class NativeQueryBuilder
	{
		
		private List<SelectColumn> selectedColumns = new List<SelectColumn>();
		
		public virtual List<SelectColumn> SelectedColumns {get{return this.selectedColumns;}}
		public virtual WhereClause Where {get;set;}
		public virtual FromClause From {get;set;}
		public virtual OrderClause Order {get;set;}
		
		public NativeQueryBuilder ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			List<string> selected = new List<string>();
			foreach (SelectColumn col in this.selectedColumns) {
				selected.Add (col.ToQueryString ());	
			}
			return "SELECT " + String.Join (", ", selected.ToArray()) + From.ToString() + Where.ToQueryString() + Order.ToQueryString() + ";";
		}
		
	}
}

