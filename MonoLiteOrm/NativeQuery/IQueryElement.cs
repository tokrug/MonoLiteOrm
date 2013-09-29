using System;

namespace Mono.Mlo
{
	// base interface for all parts of the query
	public interface IQueryElement
	{
		
		string ToQueryString();
		
	}
}

