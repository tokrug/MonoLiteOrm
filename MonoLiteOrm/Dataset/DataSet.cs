using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Data set. Data returned from Select statements is converted to a dataset. Queries can be too complicated to be translated to entities directly.
	/// This set is later used to create entities.
	/// </summary>
	public class DataSet
	{
		
		private List<DataTable> tables = new List<DataTable>();
		
		public List<DataTable> Tables {get{return this.tables;}}
		
		public DataSet ()
		{
		}
		
		public DataTable this[string tableName] {
			get {
				return tables.Find((x) => x.Name.Equals (tableName));	
			}
		}
		
	}
}

