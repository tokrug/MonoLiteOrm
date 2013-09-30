using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Converts a data set to entities and creates SqlCommands to retrieve required data.
	/// </summary>
	public class EntityAdapter
	{
		
		public EntityAdapter ()
		{
		}
		
		public virtual List<T> toEntities<T>(DataSet dataSet, ClassMapping<T> mapping) where T : new () {
			return null;	
		}
		
		public virtual T toEntity<T>(DataSet dataSet, ClassMapping<T> mapping) where T : new () {
			if (dataSet[mapping.CorrespondingTable.Name].RowCount > 0) {
				return mapEntityProperties<T>(dataSet[mapping.CorrespondingTable.Name].Rows[0], mapping);
			} else {
				return default(T);	
			}
		}
					
		private T mapEntityProperties<T>(DataRow row, ClassMapping<T> mapping) where T : new () {
			T instance = Activator.CreateInstance<T>();
			foreach (FieldMapping<object> fieldMap in mapping.PropertyMappings) {
				fieldMap.ClassField.SetValue(instance, row[fieldMap.Column.Name]);
			}
			return instance;
		}
		
	}
}

