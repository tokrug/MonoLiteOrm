using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class UniqueRowConstraint : IConstraint
	{
		
		private HashSet<object> rows = new HashSet<object>();
		
		public UniqueRowConstraint ()
		{
		}
		
		public bool validate(DataRow row) {
			return !rows.Contains(row);
		}
		
		public bool addRow(DataRow row) {
			return rows.Add (row);
		}
		
	}
}

