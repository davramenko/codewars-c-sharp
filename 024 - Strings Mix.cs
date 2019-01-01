using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Strings Mix

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "024 - Strings Mix.cs"

public class Mixing 
{
	sealed class StatsData : IComparable<StatsData>, IComparable
	{
		int _pos = 0;
		bool _used = false;
		public char ch {get; set;}
		public int cnt {get; set;}
		public int pos {get { return _pos; } set { _pos = value % 3; _used = true; }}
		public char poscmp { get { if (_pos == 0) return '=';  return _pos.ToString()[0]; } }
		public bool used { get { return _used; } }
		public override string ToString()
		{
			return string.Format("{0}:{1}", poscmp, new String(ch, cnt));
		}

		public int CompareTo(StatsData other)
		{
			int res = other.cnt - this.cnt;
			if (res == 0)
				res = this.poscmp - other.poscmp;
			return (res == 0) ? this.ch - other.ch : res;
		}

		public int CompareTo(Object obj)
		{
			return this.CompareTo(obj  as StatsData);
		}
	}

	static StatsData[] GetStats(string s)
	{
		return s.Where(c => Char.IsLetter(c) && Char.IsLower(c)).GroupBy(
				c => Char.ToLower(c),
				c => c, 
				(c, chars) => new StatsData
				{
					ch = c,
					cnt = chars.Count()
				}
			).Where(data => data.cnt > 1).ToArray();
	}

	public static string Mix(string s1, string s2)
	{
		var data1 = GetStats(s1);
		var data2 = GetStats(s2);
		Console.WriteLine("data1: {0}", string.Join("/", data1.Select(d => "1:" + d.ToString())));
		Console.WriteLine("data2: {0}", string.Join("/", data2.Select(d => "2:" + d.ToString())));
		List<StatsData> res = new List<StatsData>();
		foreach (var d in data1)
		{
			var da2 = data2.Where(ds => ds.ch == d.ch).ToArray();
			if (da2.Length == 0)
			{
				d.pos = 1;
				res.Add(d);
			}
			else if (d.cnt > da2[0].cnt)
			{
				d.pos = 1;
				res.Add(d);
				da2[0].pos = 1;
			}
			else if (d.cnt < da2[0].cnt)
			{
				da2[0].pos = 2;
				res.Add(da2[0]);
			}
			else
			{
				da2[0].pos = 0;
				res.Add(da2[0]);
			}
		}
		foreach (var d2 in data2.Where(ds => !ds.used))
		{
			d2.pos = 2;
			res.Add(d2);
		}
		return string.Join("/", res.OrderBy(x => x).Select(x => x.ToString()).ToArray());
	}
}

public static class KataTest
{
	public static void Main()
	{
		Console.WriteLine("[{0}]", Mixing.Mix("Are they here", "yes, they are here"));
		Console.WriteLine("[{0}]", Mixing.Mix("looping is fun but dangerous", "less dangerous than coding"));
		Console.WriteLine("[{0}]", Mixing.Mix(" In many languages", " there's a pair of functions"));
		Console.WriteLine("[{0}]", Mixing.Mix("Lords of the Fallen", "gamekult"));
		Console.WriteLine("[{0}]", Mixing.Mix("codewars", "codewars"));
		Console.WriteLine("[{0}]", Mixing.Mix("A generation must confront the looming ", "codewarrs"));
	}
}
