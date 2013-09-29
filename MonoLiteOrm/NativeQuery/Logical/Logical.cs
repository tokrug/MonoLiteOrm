using System;

namespace Mono.Mlo
{
	/// <summary>
	/// Class used as a reservoir of static constructors for query parts.
	/// </summary> 
	public static class Logical
	{
		
		public static ILogicalCondition And(params ILogicalCondition[] conditions) {
			return new LogicalAnd(conditions);
		}
		
		public static ILogicalCondition Or(params ILogicalCondition[] conditions) {
			return new LogicalOr(conditions);	
		}
		
		public static ILogicalCondition Not(ILogicalCondition condition) {
			return new LogicalNot() {Condition = condition};	
		}
		
		public static ILogicalCondition Equal(IQueryExpression expression, IQueryExpression equalsTo) {
			return new ComparisonCondition() {FirstExpression = expression, SecondExpression = equalsTo, ComparisonType = ComparisonType.E};
		}
		
		public static ILogicalCondition NotEqual(IQueryExpression expression, IQueryExpression notEqualsTo) {
			return new ComparisonCondition() {FirstExpression = expression, SecondExpression = notEqualsTo, ComparisonType = ComparisonType.NE};
		}
		
		public static ILogicalCondition Greater(IQueryExpression firstExpression, IQueryExpression secondExpression) {
			return new ComparisonCondition() {FirstExpression = firstExpression, SecondExpression = secondExpression, ComparisonType = ComparisonType.GT};
		}
		
		public static ILogicalCondition GreaterEqual(IQueryExpression firstExpression, IQueryExpression secondExpression) {
			return new ComparisonCondition() {FirstExpression = firstExpression, SecondExpression = secondExpression, ComparisonType = ComparisonType.GTE};
		}
		
		public static ILogicalCondition Lesser(IQueryExpression firstExpression, IQueryExpression secondExpression) {
			return new ComparisonCondition() {FirstExpression = firstExpression, SecondExpression = secondExpression, ComparisonType = ComparisonType.LS};
		}
		
		public static ILogicalCondition LesserEqual(IQueryExpression firstExpression, IQueryExpression secondExpression) {
			return new ComparisonCondition() {FirstExpression = firstExpression, SecondExpression = secondExpression, ComparisonType = ComparisonType.LSE};
		}
		
	}
}

