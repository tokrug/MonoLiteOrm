using System;

namespace Mono.Mlo
{
	/// <summary>
	/// Class query builder. Higher abstraction. Uses class mappings as a basis for native SQL query creation.
	/// </summary>
	public class ClassQueryBuilder
	{
		public ClassQueryBuilder ()
		{
		}
		
		public virtual string selectAllQuery(ClassMapping classMapping) {
			NativeQueryBuilder builder = new NativeQueryBuilder();
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				builder.SelectedColumns.Add (new SelectColumn() {TableName = classMapping.CorrespondingTable.Name, ColumnName = fieldMap.Column.Name});
			}
			builder.From = new FromClause() {Source = new TableReference() {Name = classMapping.CorrespondingTable.Name}};
			return builder.ToString ();
		}
		
		public virtual string selectByIdQuery(ClassMapping classMapping) {
			NativeQueryBuilder builder = new NativeQueryBuilder();
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				builder.SelectedColumns.Add (new SelectColumn() {TableName = classMapping.CorrespondingTable.Name, ColumnName = fieldMap.Column.Name});
			}
			builder.From = new FromClause() {Source = new TableReference() {Name = classMapping.CorrespondingTable.Name}};
			builder.Where = new WhereClause() {Equality = new EqualCondition() {ColumnName = classMapping.IdMapping.Column.Name, EqualTo = "@" + classMapping.IdMapping.Field.Field.Name}};
			return builder.ToString ();
		}
		
		public virtual string insertQuery(ClassMapping classMapping) {
			InsertStatementBuilder builder = new InsertStatementBuilder();
			ValueSet singleSet = new ValueSet();
			builder.ValueSets.Add (singleSet);
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				builder.Columns.Add (fieldMap.Column.Name);
				singleSet.addParameter(fieldMap.Field.Field.Name);
			}
			return builder.ToString();
		}
		
		public virtual string deleteQuery(ClassMapping classMapping) {
			DeleteStatementBuilder builder = new DeleteStatementBuilder();
			builder.TableName = classMapping.CorrespondingTable.Name;
			builder.Where = new WhereClause() {Equality = new EqualCondition() {ColumnName = classMapping.IdMapping.Column.Name, EqualTo = "@" + classMapping.IdMapping.Field.Field.Name}};
			return builder.ToString ();
		}
		
		public virtual string updateQuery(ClassMapping classMapping) {
			UpdateStatementBuilder builder = new UpdateStatementBuilder();
			builder.TableName = classMapping.CorrespondingTable.Name;
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				if (!fieldMap.Field.IsId) {
					builder.Columns.Add (fieldMap.Column.Name);
					builder.Values.addParameter(fieldMap.Field.Field.Name);
				}
			}
			builder.Where = new WhereClause() {Equality = new EqualCondition() {ColumnName = classMapping.IdMapping.Column.Name, EqualTo = "@" + classMapping.IdMapping.Field.Field.Name}};
			return builder.ToString ();
		}
		
	}
}

