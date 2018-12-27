using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Pyramid Slide Down

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "018 - Pyramid Slide Down.cs"

public class PyramidSlideDown
{
    public static int LongestSlideDown(int[][] pyramid)
    {
		var pyramidSum = new List<int[]>();
		for (int i = 0; i < pyramid.Length; i++)
		{
			int[] row = pyramid[i];
			pyramidSum.Add(row.Select(e => (i == pyramid.Length - 1) ? e : 0).ToArray());
		}

		for (int i = pyramidSum.Count - 2; i >= 0; i--)
		{
			for (int j = 0; j < pyramidSum[i].Length; j++)
			{
				pyramidSum[i][j] = pyramid[i][j] + Math.Max(pyramidSum[i+1][j], pyramidSum[i+1][j+1]);
			}
		}
		return pyramidSum[0][0];
    }
}

public static class KataTest
{
	public static void Main()
	{
        int[][] testArray =
        {
            //arrays themselves are i
            new int[]         {1}, //elements are j
            new int[]       {2, 1},
            new int[]     {3, 78, 7},
            new int[]   {1, 0, 2, 4},
            new int[]  {1, 2, 3, 4, 5},
            new int[] {4, 5, 6, 7, 8, 9}
        };
        Console.WriteLine(PyramidSlideDown.LongestSlideDown(testArray));

		var smallPyramid = new[]
		{
			new[] {3},
			new[] {7, 4},
			new[] {2, 4, 6},
			new[] {8, 5, 9, 3}
		};
        Console.WriteLine(PyramidSlideDown.LongestSlideDown(smallPyramid));

		var mediumPyramid = new[]
		{
			new [] {75},
			new [] {95, 64},
			new [] {17, 47, 82},
			new [] {18, 35, 87, 10},
			new [] {20,  4, 82, 47, 65},
			new [] {19,  1, 23, 75,  3, 34},
			new [] {88,  2, 77, 73,  7, 63, 67},
			new [] {99, 65,  4, 28,  6, 16, 70, 92},
			new [] {41, 41, 26, 56, 83, 40, 80, 70, 33},
			new [] {41, 48, 72, 33, 47, 32, 37, 16, 94, 29},
			new [] {53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14},
			new [] {70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57},
			new [] {91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48},
			new [] {63, 66,  4, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31},
			new [] { 4, 62, 98, 27, 23,  9, 70, 98, 73, 93, 38, 53, 60,  4, 23}
		};
        Console.WriteLine(PyramidSlideDown.LongestSlideDown(mediumPyramid));

	}
}
