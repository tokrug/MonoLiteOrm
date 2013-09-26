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
		
		public virtual ReadOnlyCollection<FieldMapping> PropertyMappings {get{return new ReadOnlyCollection<FieldMapping>(this.propMappings);}}
		public virtual FieldMapping IdMapping {get;set;}
		public virtual Type ClassType {get;set;}
		public virtual TableDefinition CorrespondingTable {get;set;}
		
		public ClassMapping() {}
		
		public virtual void addPropertyMapping(FieldMapping mapping) {
			propMappings.Add (mapping);
			propertyMappings.Add (mapping.Field.Field, mapping);	
		}
		
		public virtual int getIdValue(object obj) {
			return (int) this.IdMapping.Field.Field.GetValue (obj);	
		}
		
		public virtual void setIdValue(object obj, int id) {
			this.IdMapping.Field.Field.SetValue(obj, id);	
		}
	}
}

