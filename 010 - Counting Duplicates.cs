using System;
using System.Linq;

// Counting Duplicates

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "010 - Counting Duplicates.cs"

public class Kata
{
	public static int DuplicateCount(string str)
	{
		return str.GroupBy(
			ch => Char.ToLower(ch),
			ch => ch, 
			(ch, chars) => new
			{
				Char = ch,
				Count = chars.Count()
			}
		).Where(data => data.Count > 1)
		.Count();
		//return -1;
	}
}

public static class KataTest
{
	public static void Main()
	{
		Console.WriteLine(Kata.DuplicateCount(""));
		Console.WriteLine(Kata.DuplicateCount("abcde"));
		Console.WriteLine(Kata.DuplicateCount("aabbcde"));
		Console.WriteLine(Kata.DuplicateCount("aabBcde"));
		Console.WriteLine(Kata.DuplicateCount("Indivisibility"));
		Console.WriteLine(Kata.DuplicateCount("Indivisibilities"));
	}
}