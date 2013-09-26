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
		public FieldMapping IdMapping {get;set;}
		public Type ClassType {get;set;}
		public TableDefinition CorrespondingTable {get;set;}
		
		public ClassMapping() {}
		
		public void addPropertyMapping(FieldMapping mapping) {
			propMappings.Add (mapping);
			propertyMappings.Add (mapping.Field.Field, mapping);	
		}
		
		public int getIdValue(object obj) {
			return (int) this.IdMapping.Field.Field.GetValue (obj);	
		}
		
		public void setIdValue(object obj, int id) {
			this.IdMapping.Field.Field.SetValue(obj, id);	
		}
	}
}

