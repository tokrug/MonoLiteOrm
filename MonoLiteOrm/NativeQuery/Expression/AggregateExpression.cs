using System;

namespace Mono.Mlo
{
	public class AggregateExpression : IQueryExpression
	{
		
		public virtual IQueryExpression Expression {get;set;}
		public virtual AggregateFunction Aggregate {get;set;}
		
		public AggregateExpression ()
		{
		}
		
		public virtual string ToQueryString() {
			return convertFunctionToString(Aggregate) + "(" + Expression.ToQueryString() + ")";
		}
		
		private string convertFunctionToString(AggregateFunction aggregate) {
			string result = "";
			switch (aggregate) {
			case AggregateFunction.COUNT: {
				result = "COUNT";
				break;
			}
			case AggregateFunction.AVERAGE: {
				result = "AVG";
				break;
			}
			case AggregateFunction.MAX: {
				result = "MAX";
				break;
			}
			case AggregateFunction.MIN: {
				result = "MIN";
				break;
			}
			case AggregateFunction.SUM: {
				result = "SUM";
				break;
			}
			}
			return result;
		}
	}
}

