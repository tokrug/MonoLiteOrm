using System;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class IntFieldConverter : FieldConverter
	{
		public IntFieldConverter ()
		{
		}
		
		public string toDatabase(object obj) {
			if (obj == null) {
				return "NULL";
			} else 
				return obj.ToString ();
		}
		
		public object toObject(SqliteDataReader reader, int ordinal) {
			return reader.GetInt32(ordinal);
		}
		
		public string getColumnTypeName() {
			return "INTEGER";	
		}
		
	}
}

