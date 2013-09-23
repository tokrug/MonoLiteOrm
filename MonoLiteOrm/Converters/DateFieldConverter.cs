using System;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class DateFieldConverter : FieldConverter
	{
		public DateFieldConverter ()
		{
		}
		
		public string toDatabase(object obj) {
			if (obj == null) {
				return "NULL";
			} else {
				DateTime date = (DateTime)obj;
				return String.Format ("'{0}-{1}-{2} {3}:{4}:{5}'",
					date.Year, date.Month.ToString ().PadLeft (2,'0'), date.Day.ToString ().PadLeft (2,'0'),
					date.Hour.ToString ().PadLeft(2,'0'), date.Minute.ToString ().PadLeft(2, '0'), date.Second.ToString ().PadLeft(2,'0'));
			}
		}
		
		public object toObject(SqliteDataReader reader, int ordinal) {
			return reader.GetDateTime(ordinal);
		}
		
		public string getColumnTypeName() {
			return "DATE";
		}
		
	}
}

