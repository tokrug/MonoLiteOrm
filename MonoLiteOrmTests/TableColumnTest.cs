using NUnit.Framework;
using System;
using Mono.Mlo;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class TableColumnTest
	{
		[Test()]
		public void PrimaryColumnSchemaDefinitionTest ()
		{
			TableColumn col = new TableColumn();
			col.Name = "id";
			col.IsPrimaryKey = true;
			col.Type = "INT";
			
			string columnDef = col.getColumnDefinition();
			string expectedDef = "id INT PRIMARY KEY ASC";
			
			Assert.AreEqual (expectedDef, columnDef);
		}
		
		[Test()]
		public void NormalColumnSchemaDefinitionTest () {
			TableColumn col = new TableColumn();
			col.Name = "name";
			col.IsPrimaryKey = false;
			col.Type = "STRING";
			
			string columnDef = col.getColumnDefinition();
			string expectedDef = "name STRING";
			
			Assert.AreEqual (expectedDef, columnDef);
		}
	}
}

