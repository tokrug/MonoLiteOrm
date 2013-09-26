using NUnit.Framework;
using Moq;
using System;
using Mono.Mlo;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class DataSetTest
	{
		[Test()]
		public void DataSetIndexerTest ()
		{
			// tested item
			DataSet dataSet = new DataSet();
			
			// mock creation
			string mockTableName = "TestName";
			var mock = new Mock<DataTable>();
			mock.Setup(foo => foo.Name).Returns(mockTableName);
			
			// test itself
			dataSet.Tables.Add (mock.Object);
			
			var retrievedTable = dataSet[mockTableName];
			
			Assert.AreSame(retrievedTable, mock.Object);
			
		}
	}
}

