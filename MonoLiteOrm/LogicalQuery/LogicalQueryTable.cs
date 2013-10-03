using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	
	/// <summary>
	/// Logical query table. 
	/// </summary>
	public class LogicalQueryTable
	{
		
		public virtual LogicalTable Table {get;set;}
		public virtual string Alias {get;set;}
		
		public LogicalQueryTable ()
		{
		}
		
		public TableReference ToTableReference() {
			return From.Table (Table.PartOfTable.Name, Alias);
		}
		
		public bool IsLogicalTableEqual(LogicalQueryTable queryTable) {
			return Table.IsPartOfTheSameTable(queryTable.Table);
		}
		
	}
}

