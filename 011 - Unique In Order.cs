using System;
using System.Collections.Generic;
using System.Linq;

// Unique In Order

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "011 - Unique In Order.cs"

public static class Kata
{
	public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable) 
	{
		if (iterable.Count() == 0)
			return iterable;
		T val = iterable.First();
		bool first = true;
		return iterable.Where(item => {
			bool res = first || !item.Equals(val);
			first = false;
			val = item;
			return res;
		});
		//your code here...
		//return "";
	}
}

public static class KataTest
{
	public static void Main()
	{
		Console.WriteLine(Kata.UniqueInOrder("AAAABBBCCDAABBB").ToArray());
	}
}
