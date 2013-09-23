using System;
using System.Reflection;
using Mono.Data.SqliteClient;
using UnityEngine;

namespace Mono.Ormo
{
	public class FieldMapping
	{
		
		public FieldInfo Field {get;set;}
		public bool IsId {get;set;}
		
		public string ColumnName {get;set;}
		
		private FieldConverter converter;
		
		public FieldMapping(FieldInfo field) {
			this.Field = field;
			Column colAttr = AttributeUtils.getSingleAttribute<Column>(field);
			if (colAttr != null) {
				this.ColumnName = colAttr.Name;
			} else {
				this.ColumnName = generateColumnName(field.Name);
			}
			this.IsId = AttributeUtils.isAttributePresent<Id>(field);
			this.converter = FieldConverterFactory.getConverter(field.FieldType);
			Debug.Log (this.converter);
		}
		
		private string generateColumnName(string fieldName) {
			return fieldName;	
		}
		
		public string getColumnDefinition() {
			if (IsId) {
				return ColumnName + " " + converter.getColumnTypeName() + " PRIMARY KEY ASC";	
			} else {
				return ColumnName + " " + converter.getColumnTypeName();	
			}
		}
		
		public void assignProperty(object instance, SqliteDataReader reader, int ordinal) {
			this.Field.SetValue(instance, converter.toObject(reader, ordinal));
		}
		
		public string toSQLString(object instance) {
			object fieldValue = this.Field.GetValue(instance);
			return this.converter.toDatabase(fieldValue);
		}
		
	}
}

