using System;
using System.Linq;

// Detect Pangram

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "009 - Detect Pangram.cs"

public static class Kata
{
	public static bool IsPangram(string str)
	{
		return str.Where(ch => Char.IsLetter(ch)).Select(ch => Char.ToLower(ch)).Distinct().Count() == 26;
		//throw new NotImplementedException();
	}
}

static class Program
{
	static void Main()
	{
		Console.WriteLine(Kata.IsPangram("The quick brown fox jumps over the lazy dog."));
	}
}