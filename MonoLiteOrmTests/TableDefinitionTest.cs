using NUnit.Framework;
using System;
using Mono.Mlo;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class TableDefinitionTest
	{
		[Test()]
		public void TestCase ()
		{
			TableColumn idCol = new TableColumn();
			idCol.Name = "id";
			idCol.Type = "INT";
			idCol.IsPrimaryKey = true;
			
			TableColumn nameCol = new TableColumn();
			nameCol.Name = "name";
			nameCol.Type = "STRING";
			
			TableDefinition table = new TableDefinition();
			table.Name = "TestTable";
				
			table.addColumn(idCol);
			table.addColumn(nameCol);
			
			string retrievedSchema = table.getTableSchema();
			string expectedSchema = "CREATE TABLE TestTable (id INT PRIMARY KEY ASC, name STRING);";
			
			Assert.AreEqual(expectedSchema, retrievedSchema);
		}
	}
}

