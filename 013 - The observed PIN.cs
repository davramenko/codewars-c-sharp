using System;
using System.Collections.Generic;
using System.Text;

// The observed PIN

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "012 - Give me a Diamond.cs"

public class Kata
{
	public static List<string> GetPINs(string observed)
	{
		string[] vars = new[] {"08", "124", "2135", "326", "4157", "52468", "6359", "748", "85790", "968"};
		//return null;
	}
}

public static class KataTest
{
	public static void Main()
	{
		Console.WriteLine("[{0}]", string.Join(",", Kata.GetPINs("8").ToArray()));
		Console.WriteLine("[{0}]", string.Join(",", Kata.GetPINs("11").ToArray()));
		Console.WriteLine("[{0}]", string.Join(",", Kata.GetPINs("369").ToArray()));
	}
}
