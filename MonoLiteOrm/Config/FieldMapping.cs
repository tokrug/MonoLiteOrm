using System;
using System.Reflection;
using System.Data;

namespace Mono.Mlo
{
	/// <summary>
	/// Field mapping. T denotes type of the class containing this field. F is for the type of the field itself.
	/// </summary>
	public class FieldMapping<T,F> where T : new ()
	{	
		public virtual FieldInfo ClassField {get;set;}
		public virtual TableColumn Column {get;set;}
		public virtual bool IsId {get;set;}
		
		public FieldMapping() {}
		
		public virtual F GetValue(T obj) {
			return (F) ClassField.GetValue (obj);
		}
		
		public virtual void SetValue(T obj, F value) {
			ClassField.SetValue(obj, value);	
		}
		
	}
}

