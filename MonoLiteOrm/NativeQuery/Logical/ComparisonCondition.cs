using System;

namespace Mono.Mlo
{
	public class ComparisonCondition : ILogicalCondition
	{
		
		public virtual IQueryExpression FirstExpression {get;set;}
		public virtual IQueryExpression SecondExpression {get;set;}
		public virtual ComparisonType ComparisonType {get;set;}
		
		public ComparisonCondition ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			return FirstExpression.ToQueryString () + " " + comparisonTypeToString (this.ComparisonType) + " " + SecondExpression.ToQueryString ();
		}
		
		private string comparisonTypeToString(ComparisonType type) {
			string result = "";
			switch (type) {
			case ComparisonType.E: {
				result = "=";
				break;
			}
			case ComparisonType.NE: {
				result = "!=";
				break;
			}
			case ComparisonType.GT: {
				result = ">";
				break;
			}
			case ComparisonType.GTE: {
				result = ">=";
				break;
			}
			case ComparisonType.LS: {
				result = "<";
				break;
			}
			case ComparisonType.LSE: {
				result = "<=";
				break;
			}
			}
			return result;
		}
	}
}

