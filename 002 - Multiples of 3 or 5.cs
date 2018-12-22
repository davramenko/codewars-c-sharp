using System;
using System.Linq;
//using System.Collections;
//using System.Collections.Generic;

// Multiples of 3 or 5

// C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /debug+ "002 - Multiples of 3 or 5.cs"

public static class Kata
{
	public static int Solution(int value)
	{
		if (value < 3)
			return 0;
		return Enumerable.Range(3, value - 3).Where(x => (x % 3) == 0).Concat(Enumerable.Range(5, value - 5).Where(x => (x % 5) == 0)).Distinct().Sum();
	}
}

static class Program
{
	static void Main()
	{
		Console.WriteLine(Kata.Solution(10));
	}
}