using System;
using System.Data;

namespace Mono.Mlo
{
	public class BooleanFieldConverter : FieldConverter
	{
		public BooleanFieldConverter ()
		{
		}
		
		public string toDatabase(object obj) {
			if (obj == null) {
				return "NULL";
			} else 
				return obj.ToString ();
		}
		
		public object toObject(IDataReader reader, int ordinal) {
			return reader.GetBoolean(ordinal);
		}
		
		public string getColumnTypeName() {
			return "BOOLEAN";
		}
	}
}

