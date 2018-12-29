using System;
using System.Collections;
using System.Collections.Generic;

// Battleship field validator

// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /debug+ "021 - Battleship field validator.cs"

namespace Solution
{
	public class BattleshipField
	{
		private static bool ck_cell(int i, int j, bool mode, int[,] field)
		{
			if (i < 0 || i >= field.GetLength(0) || j < 0 || j >= field.GetLength(1))
				return false;
			if (mode)
				return (field[i, j] != 0);
			else
				return (field[i, j] == 1);
		}

		private static void strike_out_right(int i, int j0, int[,] field, Dictionary<int, int> ships)
		{
			int cnt = 0;
			for (int j = j0; j < field.GetLength(1); j++)
			{
				if (field[i, j] == 1)
				{
					field[i, j] = 2;
					cnt++;
				} else {
					break;
				}
			}
			ships[cnt]++;
			//echo "$cnt(r): " . implode(';', $arr) . "\n";
		}

		private static void strike_out_down(int i0, int j, int[,] field, Dictionary<int, int> ships)
		{
			int cnt = 0;
			for (int i = i0; i < field.GetLength(0); i++)
			{
				if (field[i, j] == 1)
				{
					field[i, j] = 2;
					cnt++;
				} else {
					break;
				}
			}
			ships[cnt]++;
			//echo "$cnt(d): " . implode(';', $arr) . "\n";
		}

		public static bool ValidateBattlefield(int[,] field) {
			bool res = true;
			Dictionary<int, int> pattern = new Dictionary<int, int> { {4, 1}, {3, 2}, {2, 3}, {1, 4} };
			Dictionary<int, int> ships = new Dictionary<int, int> ();
			foreach (KeyValuePair<int, int> pair in pattern)
				ships.Add(pair.Key, 0);

			for (int i = 0; i < field.GetLength(0); i++)
			{
				for (int j = 0; j < field.GetLength(1); j++)
				{
					if (field[i, j] == 0)
						continue;
					if (ck_cell(i - 1, j - 1, true, field))
					{
						Console.WriteLine("{0},{1}", i, j);
						res = false;
						break;
					}
					if (ck_cell(i - 1, j + 1, true, field))
					{
						Console.WriteLine("{0},{1}", i, j);
						res = false;
						break;
					}
					if (ck_cell(i + 1, j - 1, true, field))
					{
						Console.WriteLine("{0},{1}", i, j);
						res = false;
						break;
					}
					if (ck_cell(i + 1, j + 1, true, field)) {
						Console.WriteLine("{0},{1}", i, j);
						res = false;
						break;
					}
					if (field[i, j] == 1)
					{
						if (ck_cell(i, j + 1, false, field))
						{
							strike_out_right(i, j, field, ships);
						}
						else if (ck_cell(i + 1, j, false, field))
						{
							strike_out_down(i, j, field, ships);
						}
						else
						{
							ships[1]++;
							field[i, j] = 2;
						}
					}
				}
				if (!res) break;
			}
			//echo print_r($ships, true) . "\n";
			if (res)
			{
				res = false;
				if (pattern.Count == ships.Count)
				{
					res = true;
					foreach (var p in pattern)
					{
						int value;
						if (ships.TryGetValue(p.Key, out value))
						{
							if (value != p.Value) {
								res = false;
								break;
							}
						}
						else
						{
							res = false;
							break;
						}
					}
				}
			}
			//echo $res ? "true\n" : "false\n";
			return res;
		}
	}

	public class SolutionTest
	{
		public static void Main()
		{
			int[,] field1 = new int[10,10]
					{{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
					 {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
					 {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
					 {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
			Console.WriteLine(BattleshipField.ValidateBattlefield(field1));

			int[,] field3 = new int[10,10]
					{{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
					 {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
					 {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
					 {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
			Console.WriteLine(BattleshipField.ValidateBattlefield(field3));

			int[,] field4 = new int[10,10]
					{{0, 0, 0, 0, 0, 1, 1, 0, 0, 0},
					 {0, 0, 1, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 1, 0, 1, 1, 1, 0, 1, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
			Console.WriteLine(BattleshipField.ValidateBattlefield(field4));

			int[,] field5 = new int[10,10]
					{{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
					 {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
					 {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
					 {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
					 {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
					 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
			Console.WriteLine(BattleshipField.ValidateBattlefield(field5));
		
		}
	}
}
/*

<?php

function validate_battlefield(array $field): bool {
	$res = true;
	$pattern = array(4 => 1, 3 => 2, 2 => 3, 1 => 4);
	$ships = array();
	foreach ($pattern as $k => $v)
		$ships[$k] = 0;
	//for ($i = 0; $i < count($field); $i++) {
	//	echo "      [" . implode(', ', $field[$i]) . "],\n";
	//}

	$ck_cell = function($i, $j, $mode = 0) use (&$field): bool {
		if ($i < 0 || $i >= count($field) || $j < 0 || $j >= count($field[0]))
			return false;
		if ($mode == 0)
			return ($field[$i][$j] !== 0);
		else
			return ($field[$i][$j] == 1);
	};
	$strike_out_right = function($i, $j0) use (&$field, &$ships) {
		$cnt = 0;
		//$arr = array();
		for ($j = $j0; $j < count($field[$i]); $j++) {
			if ($field[$i][$j] == 1) {
				$field[$i][$j] = 2;
				//$arr[] = "($i,$j)";
				$cnt++;
			} else {
				break;
			}
		}
		$ships[$cnt]++;
		//echo "$cnt(r): " . implode(';', $arr) . "\n";
	};
	$strike_out_down = function($i0, $j) use (&$field, &$ships) {
		$cnt = 0;
		//$arr = array();
		for ($i = $i0; $i < count($field); $i++) {
			if ($field[$i][$j] == 1) {
				$field[$i][$j] = 2;
				//$arr[] = "($i,$j)";
				$cnt++;
			} else {
				break;
			}
		}
		$ships[$cnt]++;
		//echo "$cnt(d): " . implode(';', $arr) . "\n";
	};
	for ($i = 0; $i < count($field); $i++) {
		for ($j = 0; $j < count($field[$i]); $j++ ) {
			if ($field[$i][$j] === 0)
				continue;
			if ($ck_cell($i - 1, $j - 1)) {
				echo "$i,$j\n";
				$res = false;
				break;
			}
			if ($ck_cell($i - 1, $j + 1)) {
				echo "$i,$j\n";
				$res = false;
				break;
			}
			if ($ck_cell($i + 1, $j - 1)) {
				echo "$i,$j\n";
				$res = false;
				break;
			}
			if ($ck_cell($i + 1, $j + 1)) {
				echo "$i,$j\n";
				$res = false;
				break;
			}
			if ($field[$i][$j] === 1) {
				if ($ck_cell($i, $j + 1, 1)) {
					$strike_out_right($i, $j);
				} elseif ($ck_cell($i + 1, $j, 1)) {
					$strike_out_down($i, $j);
				} else {
					//echo "1: $i,$j\n";
					//echo print_r($field, true) . "\n";
					$ships[1]++;
					$field[$i][$j] = 2;
					//exit(0);
				}
			}
		}
		if (!$res) break;
	}
	echo print_r($ships, true) . "\n";
	if ($res)
		$res = (count(array_diff_assoc($pattern, $ships)) == 0);
	//echo $res ? "true\n" : "false\n";
	return $res;
}

echo validate_battlefield([
      [1, 0, 0, 0, 0, 1, 1, 0, 0, 0],
      [1, 0, 1, 0, 0, 0, 0, 0, 1, 0],
      [1, 0, 1, 0, 1, 1, 1, 0, 1, 0],
      [1, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 1, 0],
      [0, 0, 0, 0, 1, 1, 1, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 1, 0],
      [0, 0, 0, 1, 0, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    ]) ? "true\n" : "false\n";

echo validate_battlefield([
      [0, 0, 0, 0, 0, 1, 1, 0, 0, 0],
      [0, 0, 1, 0, 0, 0, 0, 0, 1, 0],
      [0, 0, 1, 0, 1, 1, 1, 0, 1, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 1, 0],
      [0, 0, 0, 0, 1, 1, 1, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 1, 0],
      [0, 0, 0, 1, 0, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    ]) ? "true\n" : "false\n";

echo validate_battlefield([
      [1, 0, 0, 0, 0, 1, 1, 0, 0, 0],
      [1, 0, 1, 0, 0, 0, 0, 0, 1, 0],
      [1, 0, 1, 0, 1, 1, 1, 0, 1, 0],
      [1, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 1, 0],
      [0, 0, 0, 1, 1, 1, 1, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 1, 0],
      [0, 0, 0, 1, 0, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    ]) ? "true\n" : "false\n";

?>
*/