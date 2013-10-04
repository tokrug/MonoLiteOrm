using System;

namespace Mono.Mlo
{
	public interface IConstraint
	{
		
		bool validate(DataRow row);
		
		bool addRow(DataRow row);
		
	}
}

