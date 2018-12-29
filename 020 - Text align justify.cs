using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// NOT RESOLVED: Execution Timed Out
// Text align justify

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "020 - Text align justify.cs"

namespace Solution 
{
	public static class StringBuilderSearching
	{
		public static bool Contains(this StringBuilder haystack, string needle)
		{
			return haystack.IndexOf(needle) != -1;
		}

		public static int IndexOf(this StringBuilder haystack, string needle)
		{
			if (haystack == null || needle == null)
				throw new ArgumentNullException();
			if (needle.Length == 0)
				return 0;//empty strings are everywhere!
			if (needle.Length == 1)//can't beat just spinning through for it
			{
				char c = needle[0];
				for (int idx = 0; idx != haystack.Length; ++idx)
					if(haystack[idx] == c)
						return idx;
					return -1;
			}
			int m = 0;
			int i = 0;
			int[] T = KMPTable(needle);
			while(m + i < haystack.Length)
			{
				if (needle[i] == haystack[m + i])
				{
					if (i == needle.Length - 1)
						return m == needle.Length ? -1 : m;//match -1 = failure to find conventional in .NET
					++i;
				}
				else
				{
					m = m + i - T[i];
					i = T[i] > -1 ? T[i] : 0;
				}
			}
			return -1;
		}

		private static int[] KMPTable(string sought)
		{
			int[] table = new int[sought.Length];
			int pos = 2;
			int cnd = 0;
			table[0] = -1;
			table[1] = 0;
			while (pos < table.Length)
				if(sought[pos - 1] == sought[cnd])
					table[pos++] = ++cnd;
				else if(cnd > 0)
					cnd = table[cnd];
				else
					table[pos++] = 0;
			return table;
		}
	}
	
	public class Kata
	{
		public static string Justify(string str, int len)
		{
			StringBuilder inp = new StringBuilder(str);
			inp.Replace('\n', ' ');
			for (int i = 0; i < inp.Length; i++)
				if (Char.IsWhiteSpace(inp[i]))
					inp[i] = ' ';
			while (inp.Contains("  "))
				inp.Replace("  ", " ");
			while (inp.Length > 0 && inp[0] == ' ')
				inp.Remove(0, 1);
			while (inp.Length > 0 && inp[inp.Length - 1] == ' ')
				inp.Remove(inp.Length - 1, 1);
			if (inp.Length == 0)
				return "";
			List<string> lines = new List<string>();
			int lwi = 0, nli = 0;
			bool fsp = true;
			for (int i = 0; i < inp.Length; i++)
			{
				if (inp[i] == ' ')
				{
					if (!fsp)
					{
						//Console.WriteLine("qqq={0}, len={1}", (i - nli), len);
						if ((i - nli) >= len)
						{
							if (lwi == nli || (i - nli) == len)
							{
								//Console.WriteLine("Line added(1): {0}", inp.ToString(nli, i - nli).Trim());
								lines.Add(inp.ToString(nli, i - nli).Trim());
								nli = i + 1;
							}
							else
							{
								//Console.WriteLine("Line added(2): {0}", inp.ToString(nli, lwi - nli).Trim());
								lines.Add(inp.ToString(nli, lwi - nli).Trim());
								nli = lwi;
							}
						}
						fsp = true;
					}
				}
				else if (fsp)
				{
					lwi = i;
					fsp = false;
				}
				//Console.WriteLine("i={0}, lwi={1}, nli={2}, clen={3}, fsp={4}, c='{5}'", i, lwi, nli, (i - nli), fsp, inp[i]);
			}
			//Console.WriteLine("Fin: lwi={0}, nli={1}, clen={2}, fsp={3}'", lwi, nli, (inp.Length - nli), fsp);
			if ((inp.Length - nli) > 0)
			{
				if (lwi == nli || (inp.Length - nli) <= len)
				{
					//Console.WriteLine("Line added(3): {0}", inp.ToString(nli, inp.Length - nli).Trim());
					lines.Add(inp.ToString(nli, inp.Length - nli).Trim());
				}
				else
				{
					//Console.WriteLine("Line added(4): {0}", inp.ToString(nli, lwi - nli).Trim());
					lines.Add(inp.ToString(nli, lwi - nli).Trim());
					//Console.WriteLine("Line added(5): {0}", inp.ToString(lwi, inp.Length - lwi).Trim());
					lines.Add(inp.ToString(lwi, inp.Length - lwi).Trim());
				}
			}
			StringBuilder res = new StringBuilder();
			for (int i = 0; i < (lines.Count - 1); i++)
			{
				StringBuilder buffer = new StringBuilder(lines[i]);
				int j = 0;
				fsp = true;
				while (buffer.Length < len)
				{
					//Console.WriteLine("j={0}, Line: [{1}]", j, buffer.ToString());
					if (j >= buffer.Length)
					{
						fsp = true;
						j = 0;
					}
					if (buffer[j] == ' ')
					{
						if (!fsp)
						{
							fsp = true;
							buffer.Insert(j, ' ');
						}
					}
					else
					{
						fsp = false;
					}
					j++;
				}
				if (res.Length > 0)
					res.Append('\n');
				res.Append(buffer.ToString());
			}
			if (res.Length > 0)
				res.Append('\n');
			res.Append(lines[lines.Count - 1]);
			return res.ToString();
		}
	}

	public static class KataTest
	{
		public static void Main()
		{
			//Console.WriteLine("[{0}]", Kata.Justify("123 45 67 8", 7));
			//Console.WriteLine("[{0}]", Kata.Justify("123 45 6", 7));
			//Console.WriteLine("[{0}]", Kata.Justify("123 45 6", 6));
			Console.WriteLine("[{0}]", Kata.Justify("123 45 678 90 1", 7));
			Environment.Exit(1);
			string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et malesuada fames ac turpis egestas maecenas pharetra convallis. Neque convallis a cras semper. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Erat imperdiet sed euismod nisi porta lorem. Sed felis eget velit aliquet. At lectus urna duis convallis convallis tellus id interdum. Sed enim ut sem viverra aliquet. Enim neque volutpat ac tincidunt vitae semper. Pharetra pharetra massa massa ultricies mi quis hendrerit dolor magna. Pretium quam vulputate dignissim suspendisse in est ante in. Nisl pretium fusce id velit ut tortor pretium viverra. Rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt lobortis. Feugiat in fermentum posuere urna nec tincidunt praesent. Duis ultricies lacus sed turpis tincidunt id. Nulla aliquet porttitor lacus luctus accumsan tortor posuere. Semper quis lectus nulla at volutpat diam. Pellentesque habitant morbi tristique senectus et netus et malesuada. Ultrices gravida dictum fusce ut. Quis risus sed vulputate odio ut enim blandit volutpat. Justo laoreet sit amet cursus sit amet dictum. Elementum sagittis vitae et leo duis ut. In egestas erat imperdiet sed euismod nisi. Diam quis enim lobortis scelerisque fermentum. Elementum nisi quis eleifend quam adipiscing vitae proin sagittis nisl. Nunc eget lorem dolor sed viverra ipsum. Vel quam elementum pulvinar etiam non quam lacus suspendisse faucibus. Malesuada bibendum arcu vitae elementum curabitur vitae nunc. Ultricies mi eget mauris pharetra et. Non nisi est sit amet facilisis magna etiam. Luctus accumsan tortor posuere ac ut consequat semper viverra nam. Semper feugiat nibh sed pulvinar. Est ultricies integer quis auctor elit sed vulputate mi sit. Sagittis aliquam malesuada bibendum arcu. Adipiscing elit pellentesque habitant morbi tristique senectus. Fermentum posuere urna nec tincidunt praesent semper feugiat. Egestas fringilla phasellus faucibus scelerisque eleifend donec. Venenatis urna cursus eget nunc. Feugiat pretium nibh ipsum consequat nisl vel pretium lectus. Nullam vehicula ipsum a arcu cursus vitae congue mauris rhoncus.";
			Console.WriteLine("[{0}]", Kata.Justify(lorem, 30));
			Environment.Exit(1);
			string l2 = @"Lorem  ipsum  dolor  sit amet,
consectetur  adipiscing  elit.
Vestibulum    sagittis   dolor
mauris,  at  elementum  ligula
tempor  eget.  In quis rhoncus
nunc,  at  aliquet orci. Fusce
at   dolor   sit   amet  felis
suscipit   tristique.   Nam  a
imperdiet   tellus.  Nulla  eu
vestibulum    urna.    Vivamus
tincidunt  suscipit  enim, nec
ultrices   nisi  volutpat  ac.
Maecenas   sit   amet  lacinia
arcu,  non dictum justo. Donec
sed  quam  vel  risus faucibus
euismod.  Suspendisse  rhoncus
rhoncus  felis  at  fermentum.
Donec lorem magna, ultricies a
nunc    sit    amet,   blandit
fringilla  nunc. In vestibulum
velit    ac    felis   rhoncus
pellentesque. Mauris at tellus
enim.  Aliquam eleifend tempus
dapibus. Pellentesque commodo,
nisi    sit   amet   hendrerit
fringilla,   ante  odio  porta
lacus,   ut   elementum  justo
nulla et dolor.";
			Console.WriteLine("[{0}]", Kata.Justify(l2, 30));
		}
	}
}
