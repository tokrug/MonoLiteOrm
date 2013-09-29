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
			Query query = new Query();
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				query.Select.SelectedColumns.Add (Select.Column(classMapping.CorrespondingTable.Name, fieldMap.Column.Name));
			}
			query.From = new FromClause() {Source = From.Table (classMapping.CorrespondingTable.Name)};
			return query.ToQueryString ();
		}
		
		public virtual string selectByIdQuery(ClassMapping classMapping) {
			Query query = new Query();
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				query.Select.SelectedColumns.Add (Select.Column(classMapping.CorrespondingTable.Name, fieldMap.Column.Name));
			}
			query.From = new FromClause() {Source = From.Table (classMapping.CorrespondingTable.Name)};
			query.Where.Condition = Logical.Equal(
				Expression.Column (classMapping.IdMapping.Column.Name),
				Expression.Parameter(classMapping.IdMapping.Field.Field.Name));
			return query.ToQueryString ();
		}
		
		public virtual string insertQuery(ClassMapping classMapping) {
			InsertStatementBuilder builder = new InsertStatementBuilder();
			ValueSet singleSet = new ValueSet();
			builder.ValueSets.Add (singleSet);
			foreach (FieldMapping fieldMap in classMapping.PropertyMappings) {
				builder.Columns.Add (fieldMap.Column.Name);
				singleSet.addParameter(fieldMap.Field.Field.Name);
			}
			return builder.ToQueryString();
		}
		
		public virtual string deleteQuery(ClassMapping classMapping) {
			DeleteStatementBuilder builder = new DeleteStatementBuilder();
			builder.TableName = classMapping.CorrespondingTable.Name;
			builder.Where.Condition = Logical.Equal(
				Expression.Column (classMapping.IdMapping.Column.Name),
				Expression.Parameter(classMapping.IdMapping.Field.Field.Name));
			return builder.ToQueryString ();
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
			builder.Where.Condition = Logical.Equal(
				Expression.Column (classMapping.IdMapping.Column.Name),
				Expression.Parameter(classMapping.IdMapping.Field.Field.Name));
			return builder.ToQueryString ();
		}
		
	}
}

