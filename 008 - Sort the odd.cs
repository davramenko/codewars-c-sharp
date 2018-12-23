using System;
using System.Linq;

// Sort the odd

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "008 - Sort the odd.cs"

public class Kata
{
	public static int[] SortArray(int[] array)
	{
		if (array.Length == 0)
			return new int[] {};
		var subset = array.Where(num => (num % 2) != 0).OrderBy(num => num).Select(num => num).ToArray();
		int[] res = (int[])Array.CreateInstance(typeof(int), array.Length);
		for (int i = 0, j = 0; i < array.Length; i++)
		{
			if ((array[i] % 2) == 0)
				res[i] = array[i];
			else
				res[i] = subset[j++];
		}
		return res;
	}
}

static class Program
{
	static void Main()
	{
		Console.WriteLine("[{0}]", string.Join(",", Kata.SortArray(new int[] { 5, 3, 2, 8, 1, 4 }).Select(num => num.ToString()).ToArray()));
		Console.WriteLine("[{0}]", string.Join(",", Kata.SortArray(new int[] { 5, 3, 1, 8, 0 }).Select(num => num.ToString()).ToArray()));
		Console.WriteLine("[{0}]", string.Join(",", Kata.SortArray(new int[] { }).Select(num => num.ToString()).ToArray()));
	}
}
