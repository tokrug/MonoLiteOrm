using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Data;

namespace Mono.Mlo
{
	public class ClassMapping
	{	
		
		private Dictionary<FieldInfo, FieldMapping> propertyMappings = new Dictionary<FieldInfo, FieldMapping>();
		private List<FieldMapping> propMappings = new List<FieldMapping>();
		
		public ReadOnlyCollection<FieldMapping> PropertyMappings {get{return new ReadOnlyCollection<FieldMapping>(this.propMappings);}}
		public FieldMapping IdField {get;set;}
		public Type ClassType {get;set;}
		public TableDefinition CorrespondingTable {get;set;}
		
		public ClassMapping() {}
		
		public void addPropertyMapping(FieldMapping mapping) {
			propMappings.Add (mapping);
			propertyMappings.Add (mapping.Field.Field, mapping);	
		}
		
		public object toObject(IDataReader reader) {
			object newInstance = Activator.CreateInstance(this.ClassType);
			IdField.assignProperty(newInstance, reader, 0);
			int i = 1;
			foreach (FieldInfo field in this.propertyMappings.Keys) {
				FieldMapping fieldMap = propertyMappings[field];
				fieldMap.assignProperty(newInstance, reader, i);
				i++;
			}
			return newInstance;
		}
		
		public string toSQLParams(string query, object obj) {
			object[] paramArray = new object[this.propertyMappings.Count+1];
			paramArray[0] = IdField.toSQLString(obj);
			int i = 1;
			foreach (FieldInfo field in this.propertyMappings.Keys) {
				FieldMapping fieldMap = propertyMappings[field];
				paramArray[i] = fieldMap.toSQLString(obj);
				i++;
			}
			return String.Format (query, paramArray);
		}
		
		public int getIdValue(object obj) {
			return (int) this.IdField.Field.Field.GetValue (obj);	
		}
		
		public void setIdValue(object obj, int id) {
			this.IdField.Field.Field.SetValue(obj, id);	
		}
	}
}

