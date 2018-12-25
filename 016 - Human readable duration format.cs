using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// Human readable duration format

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "016 - Human readable duration format.cs"

public class HumanTimeFormat
{
	public static string formatDuration(int seconds)
	{
		List<string> list = new List<string>();
		if (seconds < 0)
			throw new ArgumentException("Invalid parameter");
		if (seconds == 0)
			return "now";
		int s = seconds % 60;
		if (s > 0)
		{
			if (s > 1)
				list.Add(string.Format("{0} seconds", s));
			else
				list.Add("1 second");
		}
		seconds /= 60;
		int m = seconds % 60;
		if (m > 0)
		{
			if (m > 1)
				list.Add(string.Format("{0} minutes", m));
			else
				list.Add("1 minute");
		}
		seconds /= 60;
		int h = seconds % 24;
		if (h > 0)
		{
			if (h > 1)
				list.Add(string.Format("{0} hours", h));
			else
				list.Add("1 hour");
		}
		seconds /= 24;
		int d = seconds % 365;
		if (d > 0)
		{
			if (d > 1)
				list.Add(string.Format("{0} days", d));
			else
				list.Add("1 day");
		}
		int y = seconds / 365;
		if (y > 0)
		{
			if (y > 1)
				list.Add(string.Format("{0} years", y));
			else
				list.Add("1 year");
		}
		if (list.Count == 1)
			return list[0];
		StringBuilder buffer = new StringBuilder();
		for (int i = (list.Count - 1); i >= 0; i--)
		{
			if (buffer.Length > 0)
			{
				if (i == 0)
					buffer.Append(" and ");
				else
					buffer.Append(", ");
			}
			buffer.Append(list[i]);
		}
		return buffer.ToString();
	}
}

public static class KataTest
{
	public static void Main()
	{
		Console.WriteLine("[{0}]", HumanTimeFormat.formatDuration(0));
		Console.WriteLine("[{0}]", HumanTimeFormat.formatDuration(1));
		Console.WriteLine("[{0}]", HumanTimeFormat.formatDuration(62));
		Console.WriteLine("[{0}]", HumanTimeFormat.formatDuration(120));
		Console.WriteLine("[{0}]", HumanTimeFormat.formatDuration(3662));
	}
}
