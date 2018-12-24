using System;
using System.Text;

// Give me a Diamond

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "012 - Give me a Diamond.cs"

public static class Diamond
{
	public static string PadBoth(this string s, int width)
	{
		if (s.Length >= width)
			return s;

		int leftPadding = (width - s.Length) / 2;
		int rightPadding = width - s.Length - leftPadding;
		return new string(' ', leftPadding) + s + new string(' ', rightPadding);
	}

	public static string print(int n)
	{
		if (n <= 0 || n % 2 == 0)
			return null;
		StringBuilder buffer = new StringBuilder();
		for (int i = 1; i <= n; i += 2)
			buffer.Append(new String('*', i).PadBoth(n).TrimEnd()).Append('\n');
		for (int i = n - 2; i > 0; i -= 2)
			buffer.Append(new String('*', i).PadBoth(n).TrimEnd()).Append('\n');
		return buffer.ToString();
	}
}

public static class KataTest
{
	public static void Main()
	{
		Console.WriteLine("[{0}]", Diamond.print(1));
		Console.WriteLine("[{0}]", Diamond.print(2));
		Console.WriteLine("[{0}]", Diamond.print(3));
		Console.WriteLine("[{0}]", Diamond.print(5));
	}
}
