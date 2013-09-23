using System;
using Mono.Data.SqliteClient;

namespace Mono.Ormo
{
	public interface FieldConverter
	{
		
		string toDatabase(object obj);
		
		object toObject(SqliteDataReader reader, int ordinal);
		
		string getColumnTypeName();
		
	}
}

