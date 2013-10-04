using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.Mlo
{
	public class AttributeConfigLoader
	{
		public AttributeConfigLoader ()
		{
		}
		
		public virtual ClassMapping<object> createMapping(Type type) {
			ClassMapping<object> mapping = new ClassMapping<object>();
			// type
			mapping.ClassType = type;
			// table
			LogicalTable logicalTable = new LogicalTable();
			mapping.CorrespondingTable = logicalTable;
			
			TableDefinition table = new TableDefinition();
			
			Table tableAttr = AttributeUtils.getSingleAttribute<Table>(type);
			if (tableAttr != null) {
				table.Name = tableAttr.Name;
			} else {
				table.Name = generateTableName(type.Name);
			}
			
			logicalTable.Name = table.Name;
			
			// all fields including id
			foreach (FieldInfo field in getClassFields(type)) {
				if (isFieldPersistent(field)) {
					FieldMapping<object, object> fieldMapping = new FieldMapping<object, object>() {ClassField = field};
					
					
					TableColumn column = new TableColumn();
					logicalTable.AddColumn(column);
					Column colAttr = AttributeUtils.getSingleAttribute<Column>(field);
					if (colAttr != null) {
						column.Name = colAttr.Name;
					} else {
						column.Name = field.Name;
					}
					fieldMapping.Column = column;
					table.addColumn(column);
					mapping.AddPropertyMapping (fieldMapping);
					Id idAttr = AttributeUtils.getSingleAttribute<Id>(field);
					if (idAttr != null) {
						mapping.IdMapping = fieldMapping;
						column.IsPrimaryKey = true;
						fieldMapping.IsId = true;
					}
				}
			}
			return mapping;
		}
		
		private FieldInfo[] getClassFields(Type type) {
			return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
		}
		
		private string generateTableName(string className) {
			return className + "s";
		}
		
		private bool isFieldPersistent(FieldInfo field) {
			// if not marked as transient and not a collection (not yet as least)
			return !AttributeUtils.isAttributePresent<Transient>(field) && !typeof(ICollection<>).IsAssignableFrom(field.GetType());
		}
	}
}

