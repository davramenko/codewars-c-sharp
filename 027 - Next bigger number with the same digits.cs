using System;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

// Next bigger number with the same digits

// https://www.codewars.com/kata/next-bigger-number-with-the-same-digits/train/csharp

// "C:\Program Files (x86)\MSBuild\14.0\Bin\csc.exe" /debug+ /langversion:6 /r:"C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\mstest.testframework\1.3.2\lib\net45\Microsoft.VisualStudio.TestPlatform.TestFramework.dll" /r:C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Runtime.dll "027 - Next bigger number with the same digits.cs"

public class Kata
{
	public static long NextBiggerNumber(long n)
	{
		if (n < 12)
			return -1;

		Console.WriteLine("n={0}", n);
		StringBuilder res = null;
		StringBuilder buff = new StringBuilder();
		buff.Append(n);
		for (int i = (buff.Length - 2); i >= 0; i--)
		{
			if (buff[i] < buff[i + 1])
			{
				char val = '9';
				int k = i + 1;
				for (int j = (i + 1); j < buff.Length; j++)
				{
					if (buff[j] > buff[i] && buff[j] < val)
					{
						val = buff[j];
						k = j;
					}
				}
				res = new StringBuilder();
				res.Append(buff.ToString());
				val = res[i];
				res[i] = res[k];
				res[k] = val;
				if (i < (res.Length - 2))
				{
					Console.WriteLine("i={0}; res-len={1}", i, res.Length);
					bool fswap = false;
					int len = res.Length;
					do
					{
						fswap = false;
						for (int j = (i + 2); j < len; j++)
						{
							if (res[j - 1] > res[j])
							{
								fswap = true;
								val = res[j - 1];
								res[j - 1] = res[j];
								res[j] = val;
							}
						}
						len--;
					} while (fswap);
				}
				break;
			}
		}
		if (res != null)
		{
			long v = Convert.ToInt64(res.ToString());
			Console.WriteLine("v={0}", v);
			return v;
		}
		return -1;
	}
}

public class NextBiggerNumberTests
{
	public static void Main()
	{
		try
		{
			Console.WriteLine("****** Small Number");
			Assert.AreEqual(21, Kata.NextBiggerNumber(12));
			Assert.AreEqual(531, Kata.NextBiggerNumber(513));
			Assert.AreEqual(2071, Kata.NextBiggerNumber(2017));
			Assert.AreEqual(441, Kata.NextBiggerNumber(414));
			Assert.AreEqual(414, Kata.NextBiggerNumber(144));

			Assert.AreEqual(-1, Kata.NextBiggerNumber(9));
			Assert.AreEqual(-1, Kata.NextBiggerNumber(111));
			Assert.AreEqual(-1, Kata.NextBiggerNumber(531));

			//Assert.AreEqual(397521, Kata.NextBiggerNumber(297531));

			Assert.AreEqual(1234567908, Kata.NextBiggerNumber(1234567890)); // 1234567980
			Assert.AreEqual(1051106629, Kata.NextBiggerNumber(1051106296)); // 1051106692
			Assert.AreEqual(1853978458, Kata.NextBiggerNumber(1853975884)); // 1853978584
		}
		catch (Exception ex)
		{
			Console.WriteLine("Exception: {0}", ex.ToString());
			Environment.Exit(1);
		}
	}
}