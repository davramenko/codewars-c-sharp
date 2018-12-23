using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Mexican Wave

// C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /debug+ "007 - Mexican Wave.cs"

namespace CodeWars
{
    public class Kata
    {
        public List<string> wave(string str)
        {
	        List<string> arr = new List<string> {};
	        for (int i = 0; i < str.Length; i++) {
	        	if (Char.IsWhiteSpace(str[i]))
	        		continue;
	        	StringBuilder curr = new StringBuilder();
	        	curr.Append(str.ToLower());
	        	curr[i] = Char.ToUpper(str[i]);
	        	arr.Add(curr.ToString());
	        }
            return arr;
        }
    }
}

static class Program
{
	static void Main()
	{
		CodeWars.Kata kata = new CodeWars.Kata();
		Console.WriteLine(string.Join(",", kata.wave("hello").ToArray()));
		Console.WriteLine(string.Join(",", kata.wave("two words").ToArray()));
	}
}
