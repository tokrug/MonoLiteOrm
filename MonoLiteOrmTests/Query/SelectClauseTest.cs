using NUnit.Framework;
using System;
using Mono.Mlo;
using Moq;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class SelectClauseTest
	{
		[Test()]
		public void ColumnWithoutAlias ()
		{
			
			var mockExpression = new Mock<IQueryExpression>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			var column = new SelectColumn() {Select = mockExpression.Object};
			
			string expected = "A";
			string actual = column.ToQueryString();
			
			Assert.AreEqual (expected, actual);
			
		}
		
		[Test()]
		public void ColumnWithAlias ()
		{
			
			var mockExpression = new Mock<IQueryExpression>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			var column = new SelectColumn() {Select = mockExpression.Object, Alias = "B"};
			
			string expected = "A B";
			string actual = column.ToQueryString();
			
			Assert.AreEqual (expected, actual);
			
		}
		
		[Test()]
		public void MultiColumn ()
		{
			
			var mockExpression = new Mock<SelectColumn>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			var tested = new SelectClause();
			tested.SelectedColumns.Add (mockExpression.Object);
			tested.SelectedColumns.Add (mockExpression.Object);
			tested.SelectedColumns.Add (mockExpression.Object);
			
			string expected = "SELECT A, A, A";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual (expected, actual);
			
		}
	}
}

