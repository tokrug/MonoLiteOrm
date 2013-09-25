using System;
using System.Data;
using System.Text;

namespace Mono.Mlo
{
	public class EntityAdapter<T> where T : new()
	{
		private ClassMapping classMapping;
		private IDbConnection con;
		
		public EntityAdapter (ClassMapping mapping, IDbConnection connection)
		{
			this.classMapping = mapping;
			this.con = connection;
		}
		
		private void selectAllQuery() {
			NativeQueryBuilder builder = new NativeQueryBuilder();
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				builder.SelectedColumns.Add (new SelectColumn() {TableName = classMapping.CorrespondingTable.Name, ColumnName = fieldMap.Column.Name});
			}
			builder.From = new FromClause() {Source = new TableReference() {Name = classMapping.CorrespondingTable.Name}};
		}
		
		private void selectByIdQuery() {
			NativeQueryBuilder builder = new NativeQueryBuilder();
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				builder.SelectedColumns.Add (new SelectColumn() {TableName = classMapping.CorrespondingTable.Name, ColumnName = fieldMap.Column.Name});
			}
			builder.From = new FromClause() {Source = new TableReference() {Name = classMapping.CorrespondingTable.Name}};
			builder.Where = new WhereClause() {Equality = new EqualCondition() {ColumnName = classMapping.IdMapping.Column.Name, EqualTo = "@" + classMapping.IdMapping.Field.Field.Name}};
		}
		
		private void insertQuery() {
			IDbCommand command = new Mono.Data.SqliteClient.SqliteCommand();
			
			InsertStatementBuilder builder = new InsertStatementBuilder();
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				builder.Columns.Add (fieldMap.Column.Name);
			}
			
			command.CommandText = builder.ToString();
			
		}
		
		private void deleteQuery() {
			DeleteStatementBuilder builder = new DeleteStatementBuilder();
			builder.TableName = classMapping.CorrespondingTable.Name;
			builder.Where = new WhereClause() {Equality = new EqualCondition() {ColumnName = classMapping.IdMapping.Column.Name, EqualTo = "@" + classMapping.IdMapping.Field.Field.Name}};
		}
		
	}
}

