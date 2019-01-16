using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Sudoku Solution Validator

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "023 - Sudoku Background.cs"

public class Sudoku
{
	class SudokuBoard
	{
		private int[][] _sudokuData = null;

		public SudokuBoard(int[][] sudokuData)
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

	public static bool ValidateSolution(int[][] board)
	{
		var sudoku = new SudokuBoard(board);
		return sudoku.IsValid();
	}
}


public static class KataTest
{
	public static void Main()
	{
		int[][] board;
		board = new int[][]
		{
			new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2}, 
			new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
			new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
			new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
			new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
			new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
			new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
			new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
			new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
		};
		Console.WriteLine("[{0}]", Sudoku.ValidateSolution(board));
		
		board = new int[][]
		{
			new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2}, 
			new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
			new int[] {1, 9, 8, 3, 0, 2, 5, 6, 7},
			new int[] {8, 5, 0, 7, 6, 1, 4, 2, 3},
			new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
			new int[] {7, 0, 3, 9, 2, 4, 8, 5, 6},
			new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
			new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
			new int[] {3, 0, 0, 2, 8, 6, 1, 7, 9},
		};
		Console.WriteLine("[{0}]", Sudoku.ValidateSolution(board));
	}
}
