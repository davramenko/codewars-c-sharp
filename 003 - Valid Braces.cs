using System;
using System.Linq;
using System.Collections.Generic;

// Valid Braces

// C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /debug+ "003 - Valid Braces.cs"

public class Brace
{
	public static bool validBraces(String braces)
	{
		Stack<char> stack = new Stack<char>();
		for (int i = 0; i < braces.Length; i++) {
			char ch = braces[i];
			if (stack.Count > 0 && ")]}".Contains(ch)) {
				int pos = "([{".IndexOf(stack.Pop());
				char rqbr = ")]}"[pos];
				if (rqbr != ch)
					return false;
			} else if ("([{".Contains(ch)) {
				stack.Push(ch);
			} else {
				return false;
			}
		}
		return stack.Count() == 0;
	}
}

static class Program
{
	static void Main()
	{
		string[] arr = new[] {"()", "[(])", "{([]{()})}", "{([]{()})}(", "{([]{()})"};
		foreach (string sample in arr)
		{
			Console.WriteLine("{0}: {1}", sample, Brace.validBraces(sample));
		}
	}
}