using NUnit.Framework;
using System;
using Moq;
using Mono.Mlo;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class WhereClauseTest
	{
		[Test()]
		public void EmptyWhere ()
		{
			
			var tested = new WhereClause();
			
			string expected = "";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual (expected, actual);
			
		}
		
		[Test()]
		public void NotEmptyWhere ()
		{
			
			var tested = new WhereClause();
			
			var mockCondition = new Mock<ILogicalCondition>();
			mockCondition.Setup (x => x.ToQueryString()).Returns("A");
			
			tested.Condition = mockCondition.Object;
			
			string expected = "WHERE A";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
		}
	}
}

