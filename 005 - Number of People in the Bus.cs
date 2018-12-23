using System;
using System.Collections.Generic;

public class Kata
{
	public static int Number(List<int[]> peopleListInOut)
	{
		var res = 0;
		foreach (var stop in peopleListInOut)
			res += stop[0] - stop[1];
		return res;
	}
}