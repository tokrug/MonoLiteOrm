using System;
using System.Text;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class InsertStatementBuilder
	{
		
		private List<string> columns = new List<string>();
		private List<ValueSet> valueSets = new List<ValueSet>();
		
		public string TableName {get;set;}
		public List<string> Columns {get{return this.columns;}}
		public List<ValueSet> ValueSets {get{return this.valueSets;}}
		
		public InsertStatementBuilder ()
		{
		}
		
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append ("INSERT INTO " + TableName + "(");
			builder.Append (String.Join (", ", this.columns.ToArray()));
			builder.Append (") VALUES ");
			foreach (ValueSet valueSet in this.valueSets) {
				builder.Append ("(" + valueSet.ToString() + "), ");
			}
			builder.Remove(builder.Length-2,2);
			builder.Append (";");
			return builder.ToString();
		}
		
	}
}

