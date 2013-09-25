using System;
using System.Reflection;
using System.Data;

namespace Mono.Mlo
{
	public class FieldMapping
	{	
		public PersistentField Field {get;set;}
		public TableColumn Column {get;set;}
		
		private FieldConverter converter;
		
		public FieldMapping() {}
		
		public FieldMapping(PersistentField field) {
			this.Field = field;
			this.converter = FieldConverterFactory.getConverter(field.Field.FieldType);
		}
		
		public void assignProperty(object instance, IDataReader reader, int ordinal) {
			this.Field.SetValue(instance, converter.toObject(reader, ordinal));
		}
		
		public string toSQLString(object instance) {
			object fieldValue = this.Field.GetValue(instance);
			return this.converter.toDatabase(fieldValue);
		}
		
	}
}

