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
		
		public List<SelectColumn> SelectedColumns {get{return this.selectedColumns;}}
		public WhereClause Where {get;set;}
		public FromClause From {get;set;}
		public OrderClause Order {get;set;}
		
		public NativeQueryBuilder ()
		{
		}
		
		public override string ToString ()
		{
			List<string> selected = new List<string>();
			foreach (SelectColumn col in this.selectedColumns) {
				selected.Add (col.ToString ());	
			}
			return "SELECT " + String.Join (", ", selected.ToArray()) + From.ToString() + Where.ToString() + Order.ToString() + ";";
		}
		
	}
}

