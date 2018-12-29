using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// Connect Four

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "022 - Connect Four.cs"

public class ConnectFour
{
	enum Player
	{
		Unset = 0, Red, Yellow, Mix
	}

	sealed class CFHelper
	{
		Dictionary<char, List<Player>> _board = new Dictionary<char, List<Player>>();

		public CFHelper()
		{
			for (char c = 'A'; c <= 'G'; c++)
				_board.Add(c, new List<Player>());
		}

		public bool Drop(char colid, Player p)
		{
			List<Player> col;
			if (_board.TryGetValue(colid, out col))
			{
				col.Add(p);
				return true;
			}
			return false;
		}

		public Player[,] GetBoard()
		{
			Player[,] board = new Player[6,7];
			for (int i = 0; i < board.GetLength(0); i++)
			{
				int j = 0;
				for (char c = 'A'; c <= 'G'; c++, j++)
				{
					board[i, j] = Player.Unset;
					List<Player> col = _board[c];
					if (col.Count > i)
						board[i, j] = col[i];
				}
			}
			return board;
		}

		public Player DropAndCheck(char colid, Player p)
		{
			Player res = Player.Unset;
			if (this.Drop(colid, p))
			{
				Player[,] board = this.GetBoard();
				for (int i = 0; i < board.GetLength(0); i++)
				{
					for (int j = 0; j < board.GetLength(1); j++)
					{
						if (j < (board.GetLength(1) - 3))
						{
							// check row
							res = CheckRow(i, j, board);
							if (res == Player.Red || res == Player.Yellow)
								return res;
							if (i < (board.GetLength(0) - 3))
							{
								// check column, check diag fwd
								res = CheckCol(i, j, board);
								if (res == Player.Red || res == Player.Yellow)
									return res;
								res = CheckDiagFwd(i, j, board);
								if (res == Player.Red || res == Player.Yellow)
									return res;
							}
						}
						if (i < (board.GetLength(0) - 3) && j >= 3)
						{
							// check diag back
							res = CheckDiagBack(i, j, board);
							if (res == Player.Red || res == Player.Yellow)
								return res;
						}
					}
				}
			}
			return res;
		}

		public static Player CheckRow(int i, int j, Player[,] board)
		{
			List<Player> res = new List<Player>();
			for (int k = j; k < (j + 4); k++)
				res.Add(board[i, k]);
			//Console.WriteLine("     CheckRow({0},{1}): {2}", i, j, string.Join(" ", res.Select(x => x.ToString().Substring(0, 1)).ToArray()));
			if (res.Distinct().Count() > 1)
				return Player.Mix;
			return (res.Distinct().ToArray())[0];
		}

		public static Player CheckCol(int i, int j, Player[,] board)
		{
			List<Player> res = new List<Player>();
			for (int l = i; l < (i + 4); l++)
				res.Add(board[l, j]);
			//Console.WriteLine("     CheckCol({0},{1}): {2}", i, j, string.Join(" ", res.Select(x => x.ToString().Substring(0, 1)).ToArray()));
			if (res.Distinct().Count() > 1)
				return Player.Mix;
			return (res.Distinct().ToArray())[0];
		}

		public static Player CheckDiagFwd(int i, int j, Player[,] board)
		{
			List<Player> res = new List<Player>();
			for (int l = i, k = j; l < (i + 4); l++, k++)
				res.Add(board[l, k]);
			//Console.WriteLine(" CheckDiagFwd({0},{1}): {2}", i, j, string.Join(" ", res.Select(x => x.ToString().Substring(0, 1)).ToArray()));
			if (res.Distinct().Count() > 1)
				return Player.Mix;
			return (res.Distinct().ToArray())[0];
		}

		public static Player CheckDiagBack(int i, int j, Player[,] board)
		{
			List<Player> res = new List<Player>();
			for (int l = i, k = j; l < (i + 4); l++, k--)
				res.Add(board[l, k]);
			//Console.WriteLine("CheckDiagBack({0},{1}): {2}", i, j, string.Join(" ", res.Select(x => x.ToString().Substring(0, 1)).ToArray()));
			if (res.Distinct().Count() > 1)
				return Player.Mix;
			return (res.Distinct().ToArray())[0];
		}
	}

	public static string WhoIsWinner(List<string> piecesPositionList)
	{
		// retrun "Red" or "Yellow" or "Draw"
		Console.WriteLine(string.Join("\n", piecesPositionList.ToArray()));
		CFHelper game = new CFHelper();
		Regex regex = new Regex(@"^(?<c>[a-g])_(?<p>Red|Yellow)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		foreach (var ln in piecesPositionList)
		{
			Match match;
			if ((match = regex.Match(ln)).Success)
			{
				char ch = Char.ToUpper(match.Groups["c"].Value[0]);
				Player p = (Player)Enum.Parse(typeof(Player), match.Groups["p"].Value, true);
				Player res = game.DropAndCheck(ch, p);
				if (res == Player.Red || res == Player.Yellow)
					return res.ToString();
			}
		}
		/*
		Player[,] board = game.GetBoard();
		StringBuilder buffer = new StringBuilder();
		for (int i = 0; i < board.GetLength(0); i++)
		{
			for (int j = 0; j < board.GetLength(1); j++)
			{
				buffer.Append(' ').Append(board[i, j].ToString()[0]);
			}
			buffer.Append('\n');
		}
		Console.WriteLine(buffer.ToString());
		for (int i = 0; i < board.GetLength(0); i++)
		{
			for (int j = 0; j < board.GetLength(1); j++)
			{
				Player p;
				if (j < (board.GetLength(1) - 3))
				{
					// check row
					p = CFHelper.CheckRow(i, j, board);
					if (p == Player.Red || p == Player.Yellow)
						return p.ToString();
					if (i < (board.GetLength(0) - 3))
					{
						// check column, check diag fwd
						p = CFHelper.CheckCol(i, j, board);
						if (p == Player.Red || p == Player.Yellow)
							return p.ToString();
						p = CFHelper.CheckDiagFwd(i, j, board);
						if (p == Player.Red || p == Player.Yellow)
							return p.ToString();
					}
				}
				if (i < (board.GetLength(0) - 3) && j >= 3)
				{
					// check diag back
					p = CFHelper.CheckDiagBack(i, j, board);
					if (p == Player.Red || p == Player.Yellow)
						return p.ToString();
				}
			}
		}
		*/
		return "Draw";
	}
}

public class MyTestConnectFour
{
	public static void Main()
	{
		List<string> myList1 = new List<string>() {
				"A_Red",
				"B_Yellow",
				"A_Red",
				"B_Yellow",
				"A_Red",
				"B_Yellow",
				"G_Red",
				"B_Yellow"
			};
		Console.WriteLine(ConnectFour.WhoIsWinner(myList1));

		List<string> myList2 = new List<string>() {
				"C_Yellow",
				"E_Red",
				"G_Yellow",
				"B_Red",
				"D_Yellow",
				"B_Red",
				"B_Yellow",
				"G_Red",
				"C_Yellow",
				"C_Red",
				"D_Yellow",
				"F_Red",
				"E_Yellow",
				"A_Red",
				"A_Yellow",
				"G_Red",
				"A_Yellow",
				"F_Red",
				"F_Yellow",
				"D_Red",
				"B_Yellow",
				"E_Red",
				"D_Yellow",
				"A_Red",
				"G_Yellow",
				"D_Red",
				"D_Yellow",
				"C_Red"
			};
		Console.WriteLine(ConnectFour.WhoIsWinner(myList2));

		List<string> myList3 = new List<string>() {
				"A_Yellow",
				"B_Red",
				"B_Yellow",
				"C_Red",
				"G_Yellow",
				"C_Red",
				"C_Yellow",
				"D_Red",
				"G_Yellow",
				"D_Red",
				"G_Yellow",
				"D_Red",
				"F_Yellow",
				"E_Red",
				"D_Yellow"
		};
		Console.WriteLine(ConnectFour.WhoIsWinner(myList3));
	}
}
