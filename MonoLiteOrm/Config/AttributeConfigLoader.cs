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
		
		public virtual ClassMapping createMapping(Type type) {
			ClassMapping mapping = new ClassMapping();
			// type
			mapping.ClassType = type;
			// table
			TableDefinition table = new TableDefinition();
			mapping.CorrespondingTable = table;
			Table tableAttr = AttributeUtils.getSingleAttribute<Table>(type);
			if (tableAttr != null) {
				table.Name = tableAttr.Name;
			} else {
				table.Name = generateTableName(type.Name);
			}
			// all fields including id
			foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)) {
				if (isFieldPersistent(field)) {
					PersistentField persistentField = new PersistentField() {ClassField = field};

					FieldMapping fieldMapping = new FieldMapping();
					fieldMapping.PersistentField = persistentField;
					
					TableColumn column = new TableColumn();
					Column colAttr = AttributeUtils.getSingleAttribute<Column>(field);
					if (colAttr != null) {
						column.Name = colAttr.Name;
					} else {
						column.Name = field.Name;
					}
					fieldMapping.Column = column;
					table.addColumn(column);
					mapping.addPropertyMapping (fieldMapping);
					Id idAttr = AttributeUtils.getSingleAttribute<Id>(field);
					if (idAttr != null) {
						mapping.IdMapping = fieldMapping;
						column.IsPrimaryKey = true;
						persistentField.IsId = true;
					}
				}
			}
			return mapping;
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

