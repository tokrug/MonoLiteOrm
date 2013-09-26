using System;
using System.Text;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class UpdateStatementBuilder
	{
		
		private List<string> columns = new List<string>();
		private ValueSet values = new ValueSet();
		
		public virtual string TableName {get;set;}
		public virtual List<string> Columns {get{return this.columns;}}
		public virtual ValueSet Values {get{return this.values;}}
		public virtual WhereClause Where {get;set;}
		
		public UpdateStatementBuilder ()
		{
		}
		
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("UPDATE " + TableName + " ");
			for (int i = 0; i < columns.Count; i++) {
				builder.Append ("SET " +  columns[i] + " = " + values[i] + ", ");
			}
			builder.Remove (builder.Length-2,2);
			builder.Append(Where.ToString ());
			builder.Append (";");
			return builder.ToString ();
		}
	}
}

