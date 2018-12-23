using System;
using System.Collections.Generic;

// Find the Missing Letter

// C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /debug+ "006 - Find the Missing Letter.cs"

public class Kata
{
	public static char FindMissingLetter(char[] array)
	{
		char c = array[0];
		for (int i = 0; i < array.Length; i++) {
			if (c != array[i])
				return c;
			c++;
		}
		return ' ';
	}
}

static class Program
{
	static void Main()
	{
		Console.WriteLine(Kata.FindMissingLetter(new char[] {'a', 'b', 'c', 'd', 'f'}));
	}
}