using System;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class DoubleFieldConverter : FieldConverter
	{
		public DoubleFieldConverter ()
		{
		}
		
		public string toDatabase(object obj) {
			if (obj == null) {
				return "NULL";
			} else 
				return obj.ToString ();
		}
		
		public object toObject(SqliteDataReader reader, int ordinal) {
			return reader.GetDouble(ordinal);
		}
		
		public string getColumnTypeName() {
			return "DOUBLE";	
		}
	}
}

