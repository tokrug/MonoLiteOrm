using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using Mono.Mlo;
using Moq;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class DataSetFillerTest
	{
		[Test()]
		public void TestCase ()
		{
			
			LogicalQueryTable queryTable = CreateSingleEntityTable();
			LogicalQueryTable partTable = CreatePartOfEntityTable();
			
			IDataReader mockReader = CreateSingleRowReader();
			
			DataSetFiller filler = new DataSetFiller();
			filler.AddData(mockReader, new List<LogicalQueryTable>() {queryTable, partTable});
			
			Mono.Mlo.DataTable table = filler.GetDataSet()["Entity"];
			
			Assert.IsNotNull (table);
			Assert.AreEqual(1, table.RowCount);
			
			Console.WriteLine(table.Rows[0].ToString ());
			
			Assert.AreEqual("test name", table.Rows[0]["name"]);
			
			Mono.Mlo.DataTable secondTable = filler.GetDataSet()["EntityPart"];
			Assert.IsNotNull (secondTable);
			Assert.AreEqual(1, secondTable.RowCount);
			
			Assert.AreEqual("test name", secondTable.Rows[0]["name"]);
			Assert.IsNull (secondTable.Columns.Find ((x) => x.Name.Equals ("id")));
			
			
		}
		
		private IDataReader CreateSingleRowReader()
		{
			
		    var moq = new Mock<IDataReader>();
		
		    // This var stores current position in 'ojectsToEmulate' list
		    bool readFlag = true;
		
		    moq.Setup(x => x.Read())
		        // Return 'True' while list still has an item
		        .Returns(() => readFlag)
		        // Go to next position
		        .Callback(() => readFlag = false);
			
		    moq.SetupGet(x => x["Entity_0.id"])
		        // Again, use lazy initialization via lambda expression
		        .Returns(1);
			moq.SetupGet(x => x["Entity_0.name"])
		        // Again, use lazy initialization via lambda expression
		        .Returns("test name");
			
		
		    return moq.Object;
		}
		
		private LogicalQueryTable CreateSingleEntityTable() {
			LogicalQueryTable queryTable = new LogicalQueryTable() {Alias = "Entity_0"};
			
			LogicalTable logicalTable = new LogicalTable() {Name = "Entity", TableType = LogicalTableType.ENTITY};
			queryTable.Table = logicalTable;
			
			TableDefinition physicalTable = new TableDefinition() {Name = "Entity"};
			
			logicalTable.PartOfTable = physicalTable;
			
			TableColumn idColumn = new TableColumn() {IsPrimaryKey = true, Name="id", Table = physicalTable};
			TableColumn nameColumn = new TableColumn() {Name="name", Table = physicalTable};
			
			physicalTable.addColumn(idColumn);
			physicalTable.addColumn(nameColumn);
			
			logicalTable.AddColumn(idColumn);
			logicalTable.AddColumn(nameColumn);
			
			return queryTable;
		}
		
		private LogicalQueryTable CreatePartOfEntityTable() {
			LogicalQueryTable queryTable = new LogicalQueryTable() {Alias = "Entity_0"};
			
			LogicalTable logicalTable = new LogicalTable() {Name = "EntityPart", TableType = LogicalTableType.ENTITY};
			queryTable.Table = logicalTable;
			
			TableDefinition physicalTable = new TableDefinition() {Name = "Entity"};
			
			logicalTable.PartOfTable = physicalTable;
			
			TableColumn nameColumn = new TableColumn() {Name="name", Table = physicalTable};
			
			physicalTable.addColumn(nameColumn);
			logicalTable.AddColumn(nameColumn);
			
			return queryTable;
		}
		
	}
}

