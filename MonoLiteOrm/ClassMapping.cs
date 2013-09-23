using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Mono.Data.SqliteClient;

namespace Mono.Ormo
{
	public class ClassMapping
	{
		private Type type;
		private string tableName;
		private FieldMapping idField;
		// does not include id
		private Dictionary<FieldInfo, FieldMapping> fieldMappings = new Dictionary<FieldInfo, FieldMapping>();
		
		private string insertQuery;
		private string deleteQuery;
		private string selectQuery;
		private string updateQuery;
		
		// static constructor
		public ClassMapping(Type type) {
			// type
			this.type = type;
			// table
			Table tableAttr = AttributeUtils.getSingleAttribute<Table>(this.type);
			if (tableAttr != null) {
				this.tableName = tableAttr.Name;
			} else {
				this.tableName = generateTableName(this.type.Name);
			}
			// all fields including id
			foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)) {
				if (isFieldPersistent(field)) {
					FieldMapping mapping = new FieldMapping(field);
					Id idAttr = AttributeUtils.getSingleAttribute<Id>(field);
					if (idAttr != null) {
						this.idField = mapping;
					} else {
						this.fieldMappings.Add (field, mapping);
					}
				}
			}
			// create queries
			this.insertQuery = createInsertQuery();
			this.selectQuery = createSelectQuery();
			this.deleteQuery = createDeleteQuery();
			this.updateQuery = createUpdateQuery();
		}
		
		private string generateTableName(string className) {
			return className + "s";
		}
		
		private bool isFieldPersistent(FieldInfo field) {
			// if not marked as transient and not a collection (not yet as least)
			return !AttributeUtils.isAttributePresent<Transient>(field) && !typeof(ICollection<>).IsAssignableFrom(field.GetType());
		}
		
		private string createInsertQuery() {
			StringBuilder build = new StringBuilder();
			build.Append ("INSERT INTO ");
			build.Append(this.tableName);
			build.Append (" (");
			build.Append (this.createColumnList());
			build.Append (") VALUES (");
			for (int i = 0; i < this.fieldMappings.Count + 1;i++) {
				build.Append ("{" + i + "}, ");	
			}
			build.Remove (build.Length-2,2);
			build.Append (");");
			return build.ToString ();
		}
		
		private string createSelectQuery() {
			StringBuilder build = new StringBuilder();
			build.Append ("SELECT ");
			build.Append (this.createColumnList());
			build.Append (" FROM ");
			build.Append (this.tableName);
			build.Append (" WHERE ");
			build.Append (this.idField.ColumnName);
			build.Append (" = {0};");
			return build.ToString();
		}
		
		private string createDeleteQuery() {
			StringBuilder build = new StringBuilder();
			build.Append ("DELETE FROM ");
			build.Append (this.tableName);
			build.Append (" WHERE ");
			build.Append (this.idField.ColumnName);
			build.Append (" = {0};");
			return build.ToString();
		}
		
		private string createUpdateQuery() {
			StringBuilder build = new StringBuilder();
			build.Append ("UPDATE ");
			build.Append (this.tableName);
			build.Append (" ");
			build.Append (this.createUpdateColumnList());
			build.Append (" WHERE ");
			build.Append (this.idField.ColumnName);
			build.Append (" = {0};");
			return build.ToString ();
		}
		
		private string createColumnList() {
			StringBuilder build = new StringBuilder();
			build.Append (this.idField.ColumnName);
			build.Append (", ");
			foreach (FieldInfo field in this.fieldMappings.Keys) {
				FieldMapping fieldMap = this.fieldMappings[field];
				build.Append (fieldMap.ColumnName);
				build.Append (", ");
			}
			build.Remove(build.Length-2,2);
			return build.ToString ();
		}
		
		private string createUpdateColumnList() {
			StringBuilder build = new StringBuilder();
			int i = 1;
			foreach (FieldInfo field in this.fieldMappings.Keys) {
				FieldMapping fieldMap = this.fieldMappings[field];
				build.Append ("SET ");
				build.Append (fieldMap.ColumnName);
				build.Append (" = {" + i + "}, ");
				i++;
			}
			build.Remove(build.Length-2,2);
			return build.ToString ();
		}
		
		public string getSchemaScript() {
			StringBuilder build = new StringBuilder();
			
			build.Append ("CREATE TABLE ");
			build.Append (this.tableName);
			build.Append ("(");
			build.Append (idField.getColumnDefinition());	
			build.Append(", ");
			foreach (FieldMapping fieldMap in this.fieldMappings.Values) {
				build.Append (fieldMap.getColumnDefinition());	
				build.Append(", ");
			}
			build.Remove(build.Length - 2, 2);
			build.Append (");");
			
			return build.ToString();
		}
		
		public string getLoadQuery() {
			return this.selectQuery;
		}
		
		public string getInsertQuery() {
			return this.insertQuery;	
		}
		
		public string getDeleteQuery() {
			return this.deleteQuery;
		}
		
		public string getUpdateQuery() {
			return this.updateQuery;
		}
		
		public object toObject(SqliteDataReader reader) {
			object newInstance = Activator.CreateInstance(this.type);
			idField.assignProperty(newInstance, reader, 0);
			int i = 1;
			foreach (FieldInfo field in this.fieldMappings.Keys) {
				FieldMapping fieldMap = fieldMappings[field];
				fieldMap.assignProperty(newInstance, reader, i);
				i++;
			}
			return newInstance;
		}
		
		public string toSQLParams(string query, object obj) {
			object[] paramArray = new object[this.fieldMappings.Count+1];
			paramArray[0] = idField.toSQLString(obj);
			int i = 1;
			foreach (FieldInfo field in this.fieldMappings.Keys) {
				FieldMapping fieldMap = fieldMappings[field];
				paramArray[i] = fieldMap.toSQLString(obj);
				i++;
			}
			return String.Format (query, paramArray);
		}
		
		public int getIdValue(object obj) {
			return (int) this.idField.Field.GetValue (obj);	
		}
		
		public void setIdValue(object obj, int id) {
			this.idField.Field.SetValue(obj, id);	
		}
	}
}

