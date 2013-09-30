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
		private AssemblyMapping mapping;
		
		private Transaction transaction;
		
		private DataSetFiller dataSetFiller = new DataSetFiller();
		private EntityAdapter adapt = new EntityAdapter();
		private ClassQueryBuilder queryBuilder = new ClassQueryBuilder();
		
		public EntityManager (IDbConnection con, AssemblyMapping mappings)
		{
			this.con = con;
			this.mapping = mappings;
		}
		
		public virtual T load<T>(int id) where T : new() {
			ClassMapping<T> classMapping = mapping.getMapping<T>();
			string query = this.queryBuilder.selectByIdQuery(classMapping);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			IDataParameter idParam = cmd.CreateParameter();
			cmd.Parameters.Add (idParam);
			idParam.ParameterName = classMapping.IdMapping.ClassField.Name;
			idParam.Value = id;
			cmd.Prepare();
			IDataReader reader = cmd.ExecuteReader();
			DataSet dataSet = dataSetFiller.queryResultToDataSet(reader, classMapping.CorrespondingTable);
			return adapt.toEntity<T>(dataSet, classMapping);
		}
		
		public virtual void save<T>(T obj) where T : new() {
			ClassMapping<T> mapp = mapping.getMapping<T>();
			string query = this.queryBuilder.insertQuery(mapp);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			foreach (FieldMapping<object> fieldMap in mapp.PropertyMappings) {
				IDataParameter param = cmd.CreateParameter();
				cmd.Parameters.Add (param);
				param.ParameterName = fieldMap.ClassField.Name;
				param.Value = fieldMap.ClassField.GetValue (obj);
			}
			cmd.Prepare();
			int affectedRows = cmd.ExecuteNonQuery();
			// Sqlite only
			if (mapp.IdMapping.ClassField.GetValue (obj) == null) {
				mapp.IdMapping.ClassField.SetValue (obj, ((SqliteConnection) con).LastInsertRowId);
			}
		}
		
		public virtual void update<T>(T obj) where T : new() {
			ClassMapping<T> mapp = mapping.getMapping<T>();
			string query = this.queryBuilder.updateQuery(mapp);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			foreach (FieldMapping<object> fieldMap in mapp.PropertyMappings) {
				IDataParameter param = cmd.CreateParameter();
				cmd.Parameters.Add (param);
				param.ParameterName = fieldMap.ClassField.Name;
				param.Value = fieldMap.ClassField.GetValue (obj);
			}
			cmd.Prepare();
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public virtual void delete<T>(T obj) where T : new() {
			ClassMapping<T> mapp = mapping.getMapping<T>();
			string query = this.queryBuilder.deleteQuery(mapp);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			IDataParameter idParam = cmd.CreateParameter();
			cmd.Parameters.Add (idParam);
			idParam.ParameterName = mapp.IdMapping.ClassField.Name;
			idParam.Value = mapp.IdMapping.ClassField.GetValue (obj);
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public virtual Transaction startTransaction() {
			this.transaction = new Transaction(con.BeginTransaction());
			return this.transaction;
		}
		
	}
}

