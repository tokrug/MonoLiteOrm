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
		
		public virtual string selectAllQuery<T>(ClassMapping<T> classMapping) where T : new () {
			Query query = new Query();
			foreach (FieldMapping<T, object> fieldMap in classMapping.GetPropertyMappings()) {
				query.Select.SelectedColumns.Add (Select.Column(classMapping.CorrespondingTable.Name, fieldMap.Column.Name));
			}
			query.From = new FromClause() {Source = From.Table (classMapping.CorrespondingTable.Name)};
			return query.ToQueryString ();
		}
		
		public virtual string selectByIdQuery<T>(ClassMapping<T> classMapping) where T : new ()  {
			Query query = new Query();
			foreach (FieldMapping<T, object> fieldMap in classMapping.GetPropertyMappings()) {
				query.Select.SelectedColumns.Add (Select.Column(classMapping.CorrespondingTable.Name, fieldMap.Column.Name));
			}
			query.From = new FromClause() {Source = From.Table (classMapping.CorrespondingTable.Name)};
			query.Where.Condition = Logical.Equal(
				Expression.Column (classMapping.IdMapping.Column.Name),
				Expression.Parameter(classMapping.IdMapping.ClassField.Name));
			return query.ToQueryString ();
		}
		
		public virtual string insertQuery<T>(ClassMapping<T> classMapping) where T : new ()  {
			InsertStatementBuilder builder = new InsertStatementBuilder();
			ValueSet singleSet = new ValueSet();
			builder.ValueSets.Add (singleSet);
			foreach (FieldMapping<T, object> fieldMap in classMapping.GetPropertyMappings()) {
				builder.Columns.Add (fieldMap.Column.Name);
				singleSet.Values.Add(Expression.Parameter(fieldMap.ClassField.Name));
			}
			return builder.ToQueryString();
		}
		
		public virtual string deleteQuery<T>(ClassMapping<T> classMapping) where T : new ()  {
			DeleteStatementBuilder builder = new DeleteStatementBuilder();
			builder.TableName = classMapping.CorrespondingTable.Name;
			builder.Where.Condition = Logical.Equal(
				Expression.Column (classMapping.IdMapping.Column.Name),
				Expression.Parameter(classMapping.IdMapping.ClassField.Name));
			return builder.ToQueryString ();
		}
		
		public virtual string updateQuery<T>(ClassMapping<T> classMapping) where T : new ()  {
			UpdateStatementBuilder builder = new UpdateStatementBuilder();
			builder.TableName = classMapping.CorrespondingTable.Name;
			foreach (FieldMapping<T, object> fieldMap in classMapping.GetPropertyMappings()) {
				if (!fieldMap.IsId) {
					builder.Columns.Add (fieldMap.Column.Name);
					builder.ValueSet.Values.Add(Expression.Parameter(fieldMap.ClassField.Name));
				}
			}
			builder.Where.Condition = Logical.Equal(
				Expression.Column (classMapping.IdMapping.Column.Name),
				Expression.Parameter(classMapping.IdMapping.ClassField.Name));
			return builder.ToQueryString ();
		}
		
	}
}

