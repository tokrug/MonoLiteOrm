using System;

namespace Mono.Mlo
{
	/// <summary>
	/// Handles cross join as well. It just ignores On property.
	/// </summary>
	public class JoinedTables : ITableExpression
	{
		
		public virtual ITableExpression Table {get;set;}
		public virtual ITableExpression JoinedWith {get;set;}
		public virtual ILogicalCondition On {get;set;}
		public virtual JoinType JoinType {get;set;}
		public virtual string Alias {get;set;}
		
		public JoinedTables ()
		{
		}
		
		public virtual string ToQueryString() {
			return "(" + Table.ToQueryString() + " " + joinTypeToString(JoinType) + " " + JoinedWith.ToQueryString()
				+ (JoinType == JoinType.CROSSJOIN ? "" : " ON " + On.ToQueryString ()) + ")" 
					+ (Alias == null ? "" : " " + Alias);
		}
		
		private string joinTypeToString(JoinType type) {
			string result = "";
			switch (JoinType) {
				case JoinType.INNERJOIN: {
					result = "INNER JOIN";
					break;
				}
				case JoinType.CROSSJOIN: {
					result = "CROSS JOIN";	
					break;
				}
				case JoinType.LEFTJOIN: { 
					result = "LEFT JOIN";
					break;
				}
			}
			return result;
		}
		
	}
}

