using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Tic-Tac-Toe Checker

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "015 - Tic-Tac-Toe Checker.cs"

public class TicTacToe
{
	public enum LineState
	{
		Undefined = -1,
		CompletelyFree = 0,
		FullyOccupedBySomeone = 1,
		MayBeOccupedByAnyone = 2,
		FullyOccupedByBoth = 3,
		NotCompletelyOccupedByBoth = 4
	}

	public class CkLineStatus { public LineState State {get; set;} public int Who {get; set;} }

	public int IsSolved(int[,] board)
	{
		int[][][] line_defs = new int[][][] {
			new int[][] {new int[] {0,0}, new int[] {0,1}, new int[] {0,2}},
			new int[][] {new int[] {1,0}, new int[] {1,1}, new int[] {1,2}},
			new int[][] {new int[] {2,0}, new int[] {2,1}, new int[] {2,2}},
			new int[][] {new int[] {0,0}, new int[] {1,0}, new int[] {2,0}},
			new int[][] {new int[] {0,1}, new int[] {1,1}, new int[] {2,1}},
			new int[][] {new int[] {0,2}, new int[] {1,2}, new int[] {2,2}},
			new int[][] {new int[] {0,0}, new int[] {1,1}, new int[] {2,2}},
			new int[][] {new int[] {0,2}, new int[] {1,1}, new int[] {2,0}}
		};
		Func<int[][], CkLineStatus> ckLine = null;
		ckLine = (line_def) => {
			CkLineStatus res = new CkLineStatus() { State = LineState.CompletelyFree,  Who = 0 };
			List<int> what = new List<int>();
			foreach (int[] a in line_def)
			{
				int sym = board[a[0], a[1]];
				if (sym != 0)
				{
					res.State = LineState.Undefined;
					what.Add(sym);
				}
			}
			if (res.State != LineState.CompletelyFree)
			{
				int sum = what.Count();
				int cnt = what.Distinct().Count();
				if (cnt == 2)
				{
					res.State = LineState.NotCompletelyOccupedByBoth;
					if (sum == 3)
						res.State = LineState.FullyOccupedByBoth;
				}
				else
				{
					// what.cnt == 1
					res.Who = what[0];
					res.State = LineState.MayBeOccupedByAnyone;
					if (sum == 3)
						res.State = LineState.FullyOccupedBySomeone;
				}
			}
			Console.WriteLine("ck_line: stat={0}, who={1}", res.State, res.Who);
			return res;
		};
		// -1 if the board is not yet finished (there are empty spots),
		// 1 if "X" won,
		// 2 if "O" won,
		// 0 if it's a cat's game (i.e. a draw).
		int rc = 0;
		int count = 0;
		for (int i = 0; i < line_defs.Length; i++)
		{
			CkLineStatus st = ckLine(line_defs[i]);
			Console.WriteLine("status={0}, who={1}, rc={2}, count={3}", st.State, st.Who, rc, count);
			switch (st.State)
			{
				case LineState.FullyOccupedBySomeone:
					rc = st.Who;
					break;
				case LineState.CompletelyFree:
				case LineState.MayBeOccupedByAnyone:
					count++;
					break;
			}
			if (rc != 0)
				break;
		}
		if (rc == 0 && count > 0)
			rc = -1;
		return rc;
	}
}

public static class KataTest
{
	public static void Main()
	{
		TicTacToe tictactoe = new TicTacToe();
		int[,] board = new int[,] { { 2, 1, 2 },
		                            { 1, 1, 2 },
		                            { 0, 2, 1 }
		                          };
		Console.WriteLine("[{0}]", tictactoe.IsSolved(board));
	}
}
