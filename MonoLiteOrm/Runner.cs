using System;
using System.Reflection;
using System.Data.SqlClient;

namespace Mono.Mlo
{
	public class Runner
	{
		public int myField1 = 0;
	    protected string myField2 = null;
		
	    public static void Main()
	    {
			SqlConnection con = new SqlConnection("sadf");
			SqlCommand com = new SqlCommand();
			com.Connection = con;
	    }
	}
}

