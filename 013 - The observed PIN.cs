using System;
using System.Collections.Generic;
using System.Text;

// The observed PIN

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "013 - The observed PIN.cs"

public class Kata
{
	public static List<string> GetPINs(string observed)
	{
		string[] vars = new[] {"08", "124", "2135", "326", "4157", "52468", "6359", "748", "85790", "968"};
		Func<string, List<string>> penum = null;
		penum = (crest) => {
			List<string> res = new List<string>();
			if (crest.Length > 0)
			{
				int n = Convert.ToInt32(crest.Substring(0, 1));
				if (crest.Length > 1)
				{
					List<string> subs = penum(crest.Substring(1));
					for (int i = 0; i < vars[n].Length; i++)
					{
						for (int j = 0; j < subs.Count; j++)
							res.Add((new String(vars[n][i], 1)) + subs[j]);
					}
				}
				else
				{
					for (int i = 0; i < vars[n].Length; i++)
						res.Add(new String(vars[n][i], 1));
				}
			}
			return res;
		};
		return penum(observed);
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
