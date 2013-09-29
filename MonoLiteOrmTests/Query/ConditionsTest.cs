using NUnit.Framework;
using System;
using Mono.Mlo;
using Moq;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class ConditionsTest
	{
		
		private Mock<IQueryExpression> firstExpression;
		private Mock<IQueryExpression> secondExpression;
		
		[SetUp()]
		public void Setup() {
			firstExpression = new Mock<IQueryExpression>();
			firstExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			secondExpression = new Mock<IQueryExpression>();
			secondExpression.Setup (x => x.ToQueryString()).Returns ("B");	
		}
		
		[Test()]
		public void EqualConditionTest ()
		{
			
			string expectedResult = "A = B";
			
			var tested = new ComparisonCondition() {FirstExpression = firstExpression.Object, SecondExpression = secondExpression.Object, ComparisonType = ComparisonType.E};
			
			string actualResult = tested.ToQueryString ();
			
			Assert.AreEqual(expectedResult, actualResult);
			
		}
		
		[Test()]
		public void NotEqualConditionTest ()
		{
			
			string expectedResult = "A != B";
			
			var tested = new ComparisonCondition() {FirstExpression = firstExpression.Object, SecondExpression = secondExpression.Object, ComparisonType = ComparisonType.NE};
			
			string actualResult = tested.ToQueryString ();
			
			Assert.AreEqual(expectedResult, actualResult);
			
		}
		
		[Test()]
		public void GreaterConditionTest ()
		{
			
			string expectedResult = "A > B";
			
			var tested = new ComparisonCondition() {FirstExpression = firstExpression.Object, SecondExpression = secondExpression.Object, ComparisonType = ComparisonType.GT};
			
			string actualResult = tested.ToQueryString ();
			
			Assert.AreEqual(expectedResult, actualResult);
			
		}
		
		[Test()]
		public void GreaterEqualConditionTest ()
		{

			string expectedResult = "A >= B";
			
			var tested = new ComparisonCondition() {FirstExpression = firstExpression.Object, SecondExpression = secondExpression.Object, ComparisonType = ComparisonType.GTE};
			
			string actualResult = tested.ToQueryString ();
			
			Assert.AreEqual(expectedResult, actualResult);
			
		}
		
		[Test()]
		public void LesserConditionTest ()
		{

			string expectedResult = "A < B";
			
			var tested = new ComparisonCondition() {FirstExpression = firstExpression.Object, SecondExpression = secondExpression.Object, ComparisonType = ComparisonType.LS};
			
			string actualResult = tested.ToQueryString ();
			
			Assert.AreEqual(expectedResult, actualResult);
			
		}
		
		[Test()]
		public void LesserEqualConditionTest ()
		{

			string expectedResult = "A <= B";
			
			var tested = new ComparisonCondition() {FirstExpression = firstExpression.Object, SecondExpression = secondExpression.Object, ComparisonType = ComparisonType.LSE};
			
			string actualResult = tested.ToQueryString ();
			
			Assert.AreEqual(expectedResult, actualResult);
			
		}
	}
}

