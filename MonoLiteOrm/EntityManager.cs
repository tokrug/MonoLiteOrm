using System;
using System.Text;
using System.Data;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class EntityManager
	{
		// it has to be injected somehow
		private IDbConnection con;
		private DatabaseMappings mapping;
		
		private Transaction transaction;
		
		private DataSetFiller dataSetFiller = new DataSetFiller();
		private EntityAdapter adapt = new EntityAdapter();
		private ClassQueryBuilder queryBuilder = new ClassQueryBuilder();
		
		public EntityManager (IDbConnection con, DatabaseMappings mappings)
		{
			this.con = con;
			this.mapping = mappings;
		}
		
		public T load<T>(int id) where T : new() {
			ClassMapping classMapping = mapping.getMapping<T>();
			string query = this.queryBuilder.selectByIdQuery(classMapping);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			IDataParameter idParam = cmd.CreateParameter();
			cmd.Parameters.Add (idParam);
			idParam.ParameterName = classMapping.IdMapping.Field.Field.Name;
			idParam.Value = id;
			cmd.Prepare();
			IDataReader reader = cmd.ExecuteReader();
			DataSet dataSet = dataSetFiller.queryResultToDataSet(reader, classMapping.CorrespondingTable);
			return adapt.toEntity<T>(dataSet, classMapping);
		}
		
		public void save<T>(T obj) where T : new() {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = this.queryBuilder.insertQuery(mapp);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			foreach (FieldMapping fieldMap in mapp.PropertyMappings) {
				IDataParameter param = cmd.CreateParameter();
				cmd.Parameters.Add (param);
				param.ParameterName = fieldMap.Field.Field.Name;
				param.Value = fieldMap.Field.Field.GetValue (obj);
			}
			cmd.Prepare();
			int affectedRows = cmd.ExecuteNonQuery();
			// Sqlite only
			if (mapp.IdMapping.Field.Field.GetValue (obj) == null) {
				mapp.IdMapping.Field.Field.SetValue (obj, ((SqliteConnection) con).LastInsertRowId);
			}
		}
		
		public void update<T>(T obj) where T : new() {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = this.queryBuilder.updateQuery(mapp);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			foreach (FieldMapping fieldMap in mapp.PropertyMappings) {
				IDataParameter param = cmd.CreateParameter();
				cmd.Parameters.Add (param);
				param.ParameterName = fieldMap.Field.Field.Name;
				param.Value = fieldMap.Field.Field.GetValue (obj);
			}
			cmd.Prepare();
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public void delete<T>(T obj) where T : new() {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = this.queryBuilder.deleteQuery(mapp);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			IDataParameter idParam = cmd.CreateParameter();
			cmd.Parameters.Add (idParam);
			idParam.ParameterName = mapp.IdMapping.Field.Field.Name;
			idParam.Value = mapp.IdMapping.Field.Field.GetValue (obj);
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public Transaction startTransaction() {
			this.transaction = new Transaction(con.BeginTransaction());
			return this.transaction;
		}
		
	}
}

