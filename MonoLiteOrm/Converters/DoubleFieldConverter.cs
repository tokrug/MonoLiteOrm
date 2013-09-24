using System;
using System.Data;

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
		
		public object toObject(IDataReader reader, int ordinal) {
			return reader.GetDouble(ordinal);
		}
		
		public string getColumnTypeName() {
			return "DOUBLE";	
		}
	}
}

