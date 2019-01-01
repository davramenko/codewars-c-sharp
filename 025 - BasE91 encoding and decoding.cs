using System;
using System.Collections.Generic;

// BasE91 encoding & decoding

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "025 - BasE91 encoding and decoding.cs"

class Base91
{
	private static char[] EncodeTable = new char[] 
	{
		'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
		'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
		'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '!', '#', '$',
		'%', '&', '(', ')', '*', '+', ',', '.', '/', ':', ';', '<', '=',
		'>', '?', '@', '[', ']', '^', '_', '`', '{', '|', '}', '~', '"'
	};

	private static Dictionary<byte, int> DecodeTable = null;

	public static string Encode(string input)
	{
		string res = "";
		int queue = 0, nbits = 0;
		for (int i = 0; i < input.Length; i++)
		{
			queue |= (byte)input[i] << nbits;
			nbits += 8;
			if (nbits > 13)
			{
				int val = queue & 8191;
				if (val > 88)
				{
					queue >>= 13;
					nbits -= 13;
				}
				else
				{
					val = queue & 16383;
					queue >>= 14;
					nbits -= 14;
				}
				res += (char)EncodeTable[val % 91];
				res += (char)EncodeTable[val / 91];
			}
		}
		if (nbits != 0)
		{
			res += (char)EncodeTable[queue % 91];
			if (nbits > 7 || queue > 90)
				res += (char)EncodeTable[queue / 91];
		}
		return res;
	}

	public static string Decode(string input)
	{
		if (DecodeTable == null)
		{
			DecodeTable = new Dictionary<byte, int>();
			for (int i = 0; i < 255; i++)
				DecodeTable[(byte)i] = -1;
			for (int i = 0; i < EncodeTable.Length; i++)
				DecodeTable[(byte)EncodeTable[i]] = i;
		}

		string res = "";
		int d = 0, val = -1, queue = 0, nbits = 0;
		for (int i = 0; i < input.Length; i++)
		{
			d = DecodeTable[(byte)input[i]];
			if (d == -1)
				continue;
			if (val < 0)
			{
				val = d;
			}
			else
			{
				val += d * 91;
				queue |= val << nbits;
				nbits += (val & 8191) > 88 ? 13 : 14;
				do
				{
					res += (char)(queue & 255);
					queue >>= 8;
					nbits -= 8;
				} while (nbits > 7);
				val = -1;
			}
		}
		if (val + 1 != 0)
		{
			res += (char)((queue | val << nbits) & 255);
		}
		return res;
	}
}

public static class KataTest
{
	public static void Main()
	{
		Console.WriteLine("[{0}]", Base91.Encode("test"));
		Console.WriteLine("[{0}]", Base91.Encode("Hello World!"));
		Console.WriteLine("[{0}]", Base91.Decode("fPNKd"));
		Console.WriteLine("[{0}]", Base91.Decode(">OwJh>Io0Tv!8PE"));
	}
}
