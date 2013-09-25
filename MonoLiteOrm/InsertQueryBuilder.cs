using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class InsertQueryBuilder
	{
		
		private List<string> columnNames;
		
		public List<string> Columns {get{return this.columnNames;}set{this.columnNames = value;}}
		
		public InsertQueryBuilder ()
		{
		}
		
		public string toString() {
			StringBuilder build = new StringBuilder();
			build.Append ("INSERT INTO ");
			build.Append(this.tableName);
			build.Append (" (");
			build.Append (this.createColumnList());
			build.Append (") VALUES (");
			foreach (FieldMapping fieldMap in this.fieldMappings.Values) {
				build.Append ("@" + fieldMap.Field.Name + ", ");
			}
			build.Remove (build.Length-2,2);
			build.Append (");");
			return build.ToString ();
		}
		
	}
}

