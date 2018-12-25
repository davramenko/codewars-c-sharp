using System;
using System.Collections.Generic;

// Can you get the loop ?

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "014 - Can you get the loop.cs"

public class Kata{
	public static int getLoopSize(LoopDetector.Node startNode){
		Dictionary<LoopDetector.Node, int> dict = new Dictionary<LoopDetector.Node, int>();
		int len = 0, i = 0;
		LoopDetector.Node nextNode = startNode;
		while (nextNode != null)
		{
			dict.Add(nextNode, i++);
			nextNode = nextNode.next;
			if (dict.ContainsKey(nextNode))
			{
				len = i - dict[nextNode];
				break;
			}
		}
		return len;
	}
}