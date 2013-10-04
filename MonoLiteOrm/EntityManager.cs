using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class EntityManager
	{
		// it has to be injected somehow
		private IDbConnection con;
		private AssemblyMapping mapping;
		
		private Transaction transaction;
		
		private EntityAdapter adapt = new EntityAdapter();
		private ClassQueryBuilder queryBuilder = new ClassQueryBuilder();
		
		public EntityManager (IDbConnection con, AssemblyMapping mappings)
		{
			this.con = con;
			this.mapping = mappings;
		}
		
		public virtual List<T> load<T>() where T : new() {
			List<T> result = new List<T>();
			
			
			
			return result;
		}
		
		public virtual T load<T>(int id) where T : new() {
			DataSetFiller dataSetFiller = new DataSetFiller();
			ClassMapping<T> classMapping = mapping.GetMapping<T>();
			LogicalQuery query = classMapping.SelectSingleQuery;
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query.NativeQuery;
			IDataParameter idParam = cmd.CreateParameter();
			cmd.Parameters.Add (idParam);
			idParam.ParameterName = classMapping.IdMapping.ClassField.Name;
			idParam.Value = id;
			cmd.Prepare();
			IDataReader reader = cmd.ExecuteReader();
			DataSet dataSet = dataSetFiller.AddData(reader, query.LogicalTables).GetDataSet();
			return adapt.toEntity<T>(dataSet, classMapping);
		}
		
		public virtual void save<T>(T obj) where T : new() {
			ClassMapping<T> mapp = mapping.GetMapping<T>();
			string query = this.queryBuilder.insertQuery(mapp);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			foreach (FieldMapping<T, object> fieldMap in mapp.GetPropertyMappings()) {
				IDataParameter param = cmd.CreateParameter();
				cmd.Parameters.Add (param);
				param.ParameterName = fieldMap.ClassField.Name;
				param.Value = fieldMap.ClassField.GetValue (obj);
			}
			cmd.Prepare();
			int affectedRows = cmd.ExecuteNonQuery();
			// Sqlite only
			if (mapp.GetIdValue (obj) == null) {
				mapp.SetIdValue (obj, ((SqliteConnection) con).LastInsertRowId);
			}
		}
		
		public virtual void update<T>(T obj) where T : new() {
			ClassMapping<T> mapp = mapping.GetMapping<T>();
			string query = this.queryBuilder.updateQuery(mapp);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			foreach (FieldMapping<T, object> fieldMap in mapp.GetPropertyMappings()) {
				IDataParameter param = cmd.CreateParameter();
				cmd.Parameters.Add (param);
				param.ParameterName = fieldMap.ClassField.Name;
				param.Value = fieldMap.ClassField.GetValue (obj);
			}
			cmd.Prepare();
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public virtual void delete<T>(T obj) where T : new() {
			ClassMapping<T> mapp = mapping.GetMapping<T>();
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

