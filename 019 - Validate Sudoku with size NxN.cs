using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Validate Sudoku with size `NxN`

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "019 - Validate Sudoku with size NxN.cs"

class Sudoku
{
	private int[][] _sudokuData = null;

	public Sudoku(int[][] sudokuData)
	{
		_sudokuData = sudokuData;
	}

	private List<int> getRow(int num)
	{
		int sz = _sudokuData[0].Length;
		List<int> lst = new List<int>();
		for (int j = 0; j < sz; j++)
		{
			int v = _sudokuData[num][j];
			lst.Add(v);
			if (v < 1 || v > sz)
				throw new ApplicationException(string.Format("Bad data in row {0}, pos {1}: {2}", num, j, v));
		}
		return lst;
	}

	private List<int> getColumn(int num)
	{
		int sz = _sudokuData[0].Length;
		List<int> lst = new List<int>();
		for (int i = 0; i < sz; i++)
		{
			int v = _sudokuData[i][num];
			lst.Add(v);
			if (v < 1 || v > sz)
				throw new ApplicationException(string.Format("Bad data in column {0}, pos {1}: {2}", num, i, v));
		}
		return lst;
	}

	private List<int> getSquare(int num)
	{
		int sz = _sudokuData[0].Length;
		int sqsz = (int)Math.Sqrt(sz);
		int frow = (num / sqsz) * sqsz;
		int fcol = (num % sqsz) * sqsz;
		List<int> lst = new List<int>();
		Console.WriteLine("frow: {0}; fcol: {1}; sqsz: {2}", frow, fcol, sqsz);
		for (int i = frow; i < (frow + sqsz); i++)
		{
			for (int j = fcol; j < (fcol + sqsz); j++)
			{
				int v = _sudokuData[i][j];
				lst.Add(v);
				if (v < 1 || v > sz)
					throw new ApplicationException(string.Format("Bad data in square {0}x{1}: {2}", i, j, v));
				Console.Write("{0}x{1}: {2}, ", i, j, v);
			}
		}
		Console.WriteLine();
		return lst;
	}

	public bool IsValid()
	{
		try
		{
			int sz = _sudokuData[0].Length;
			int sqsz = (int)Math.Sqrt(sz);
			if ((sqsz * sqsz) != _sudokuData[0].Length)
				throw new ApplicationException("Bad size");
			for (int i = 0; i < sz; i++)
			{
				if (getRow(i).Distinct().Count() != sz)
					throw new ApplicationException(string.Format("Bad row {0}", i));
				if (getColumn(i).Distinct().Count() != sz)
					throw new ApplicationException(string.Format("Bad column {0}", i));
				if (getSquare(i).Distinct().Count() != sz)
					throw new ApplicationException(string.Format("Bad square {0}", i));
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Exception: {0}", ex.ToString());
			return false;
		}
		return true;
	}
}

public static class KataTest
{
	public static void Main()
	{
		var goodSudoku1 = new Sudoku(
			new int[][] {
			new int[] {7,8,4, 1,5,9, 3,2,6},
			new int[] {5,3,9, 6,7,2, 8,4,1},
			new int[] {6,1,2, 4,3,8, 7,5,9},
    
			new int[] {9,2,8, 7,1,5, 4,6,3},
			new int[] {3,5,7, 8,4,6, 1,9,2},
			new int[] {4,6,1, 9,2,3, 5,8,7},
      
			new int[] {8,7,6, 3,9,4, 2,1,5},
			new int[] {2,4,3, 5,6,1, 9,7,8},
			new int[] {1,9,5, 2,8,7, 6,3,4}
		});
		Console.WriteLine("[{0}]", goodSudoku1.IsValid());
  
		var goodSudoku2 = new Sudoku(
			new int[][] {
			new int[] {1,4, 2,3},
			new int[] {3,2, 4,1},
    
			new int[] {4,1, 3,2},
			new int[] {2,3, 1,4}
		});    
		Console.WriteLine("[{0}]", goodSudoku2.IsValid());
  
		var badSudoku1 = new Sudoku(
			new int[][] {
			new int[] {1,2,3, 4,5,6, 7,8,9},
			new int[] {1,2,3, 4,5,6, 7,8,9},
			new int[] {1,2,3, 4,5,6, 7,8,9},
    
			new int[] {1,2,3, 4,5,6, 7,8,9},
			new int[] {1,2,3, 4,5,6, 7,8,9},
			new int[] {1,2,3, 4,5,6, 7,8,9},
      
			new int[] {1,2,3, 4,5,6, 7,8,9},
			new int[] {1,2,3, 4,5,6, 7,8,9},
			new int[] {1,2,3, 4,5,6, 7,8,9}
		});
		Console.WriteLine("[{0}]", badSudoku1.IsValid());
  
		var badSudoku2 = new Sudoku(
			new int[][] {
			new int[] {1,2,3,4,5},
			new int[] {1,2,3,4},
    
			new int[] {1,2,3,4},
			new int[] {1}
		});   
		Console.WriteLine("[{0}]", badSudoku2.IsValid());
	}
}
