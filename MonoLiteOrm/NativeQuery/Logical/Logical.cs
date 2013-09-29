using System;

namespace Mono.Mlo
{
	/// <summary>
	/// Class used as a reservoir of static constructors for query parts.
	/// </summary> 
	public static class Logical
	{
		
		public static LogicalAnd And(params ILogicalCondition[] conditions) {
			return new LogicalAnd(conditions);
		}
		
		public static LogicalOr Or(params ILogicalCondition[] conditions) {
			return new LogicalOr(conditions);	
		}
		
		public static EqualCondition Equal(IQueryExpression expression, IQueryExpression equalsTo) {
			return new EqualCondition() {Expression = expression, EqualsExpression = equalsTo};
		}
		
		public static NotEqualCondition NotEqual(IQueryExpression expression, IQueryExpression notEqualsTo) {
			return new NotEqualCondition() {Expression = expression, NotEqualsExpression = notEqualsTo};	
		}
		
	}
}

