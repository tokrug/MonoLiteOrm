using System;
using System.Data;

namespace Mono.Mlo
{
	public interface FieldConverter
	{
		
		string toDatabase(object obj);
		
		object toObject(IDataReader reader, int ordinal);
		
		string getColumnTypeName();
		
	}
}

