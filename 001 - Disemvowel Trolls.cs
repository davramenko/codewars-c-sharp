using System;
using System.Text;
using System.Text.RegularExpressions;

// Disemvowel Trolls

// C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /debug+ "001 - Disemvowel Trolls.cs"

public static class Kata
{
	public static string Disemvowel(string str)
	{
		return Regex.Replace(str, @"[aeiou]", "", RegexOptions.IgnoreCase);
	}
}

static class Program
{
	static void Main()
	{
		Console.WriteLine(Kata.Disemvowel("This website is for losers LOL!"));
	}
}