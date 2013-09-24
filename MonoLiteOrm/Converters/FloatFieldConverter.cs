using System;
using System.Data;

namespace Mono.Mlo
{
	public class FloatFieldConverter : FieldConverter
	{
		public FloatFieldConverter ()
		{
		}
		
		public string toDatabase(object obj) {
			if (obj == null) {
				return "NULL";
			} else 
				return obj.ToString ();
		}
		
		public object toObject(IDataReader reader, int ordinal) {
			return reader.GetFloat(ordinal);
		}
		
		public string getColumnTypeName() {
			return "FLOAT";	
		}
		
	}
}

