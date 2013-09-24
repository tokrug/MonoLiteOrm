using System;
using System.Data;
	
namespace Mono.Mlo
{
	public class StringFieldConverter : FieldConverter
	{
		public StringFieldConverter ()
		{
		}
		
		public string toDatabase(object obj) {
			if (obj == null) {
				return "NULL";
			} else 
				return "'" + obj.ToString() + "'";
		}
		
		public object toObject(IDataReader reader, int ordinal) {
			return reader.GetString (ordinal);
		}
		
		public string getColumnTypeName() {
			return "TEXT";	
		}
	}
}

