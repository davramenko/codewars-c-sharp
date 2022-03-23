//using System;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Linq;
using NUnit.Framework;

// Street Fighter 2 - Character Selection

// https://www.codewars.com/kata/5853213063adbd1b9b0000be/train/csharp

// "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\csc.exe" /debug+ /langversion:7 /r:C:\Temp\NUnit.Console-3.15.0\bin\net40\nunit.framework.dll /r:C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Runtime.dll "028 - Street Fighter 2 - Character Selection.cs"

public class Kata
{
    public string[] StreetFighterSelection(string[][] fighters, int[] position, string[] moves)
    {
        List<string> result = new List<string>();
        foreach (var move in moves)
		{
            Console.WriteLine("Move: {0}; posRow: {1}; posCol: {2}; rows: {3}; cols: {4}", move, position[0], position[1], fighters.Length, fighters[0].Length);
            switch (move.ToLower())
			{
                case "up":
                    if (position[0] > 0) position[0]--;
                    break;
                case "down":
                    if (position[0] < (fighters.Length - 1)) position[0]++;
                    break;
                case "left":
                    if (position[1] <= 0) position[1] = fighters[0].Length - 1;
                    else position[1]--;
                    break;
                case "right":
                    position[1]++;
                    if (position[1] >= fighters[0].Length) position[1] = 0;
                    break;
			}
            result.Add(fighters[position[0]][position[1]]);
		}
        var output = result.ToArray();
        Console.WriteLine("Rsult: {0}", string.Join(",", output));
        return output;
    }
}

[TestFixture]
public class FrontTests
{
    private Kata kata = new Kata();
    private string[][] fighters;

    public FrontTests()
    {
        this.fighters = new string[][]
        {
              new string[] { "Ryu", "E.Honda", "Blanka", "Guile", "Balrog", "Vega" },
              new string[] { "Ken", "Chun Li", "Zangief", "Dhalsim", "Sagat", "M.Bison" },
        };
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public string GetCurrentMethod()
    {
        var st = new StackTrace();
        var sf = st.GetFrame(1);

        return sf.GetMethod().Name;
    }

    [Test]
    public void _01_ShouldWorkWithFewMoves()
    {
        var moves = new string[] { "up", "left", "right", "left", "left" };
        var expected = new string[] { "Ryu", "Vega", "Ryu", "Vega", "Balrog" };
        Console.WriteLine(GetCurrentMethod());
        CollectionAssert.AreEqual(expected, kata.StreetFighterSelection(fighters, new int[] { 0, 0 }, moves));
    }

    [Test]
    public void _02_ShouldWorkWithNoSelectionCursorMoves()
    {
        var moves = new string[] { };
        var expected = new string[] { };
        Console.WriteLine(GetCurrentMethod());
        CollectionAssert.AreEqual(expected, kata.StreetFighterSelection(fighters, new int[] { 0, 0 }, moves));
    }

    [Test]
    public void _03_ShouldWorkWhenAlwaysMovingLeft()
    {
        var moves = new string[] { "left", "left", "left", "left", "left", "left", "left", "left" };
        var expected = new string[] { "Vega", "Balrog", "Guile", "Blanka", "E.Honda", "Ryu", "Vega", "Balrog" };
        Console.WriteLine(GetCurrentMethod());
        CollectionAssert.AreEqual(expected, kata.StreetFighterSelection(fighters, new int[] { 0, 0 }, moves));
    }

    [Test]
    public void _04_ShouldWorkWhenAlwaysMovingRight()
    {
        var moves = new string[] { "right", "right", "right", "right", "right", "right", "right", "right" };
        var expected = new string[] { "E.Honda", "Blanka", "Guile", "Balrog", "Vega", "Ryu", "E.Honda", "Blanka" };
        Console.WriteLine(GetCurrentMethod());
        CollectionAssert.AreEqual(expected, kata.StreetFighterSelection(fighters, new int[] { 0, 0 }, moves));
    }

    [Test]
    public void _05_ShouldUseAll4DirectionsClockwiseTwice()
    {
        var moves = new string[] { "up", "left", "down", "right", "up", "left", "down", "right" };
        var expected = new string[] { "Ryu", "Vega", "M.Bison", "Ken", "Ryu", "Vega", "M.Bison", "Ken" };
        Console.WriteLine(GetCurrentMethod());
        CollectionAssert.AreEqual(expected, kata.StreetFighterSelection(fighters, new int[] { 0, 0 }, moves));
    }

    [Test]
    public void _06_ShouldWorkWhenAlwaysMovingDown()
    {
        var moves = new string[] { "down", "down", "down", "down" };
        var expected = new string[] { "Ken", "Ken", "Ken", "Ken" };
        Console.WriteLine(GetCurrentMethod());
        CollectionAssert.AreEqual(expected, kata.StreetFighterSelection(fighters, new int[] { 0, 0 }, moves));
    }

    [Test]
    public void _07_ShouldWorkWhenAlwaysMovingUp()
    {
        var moves = new string[] { "up", "up", "up", "up" };
        var expected = new string[] { "Ryu", "Ryu", "Ryu", "Ryu" };
        Console.WriteLine(GetCurrentMethod());
        CollectionAssert.AreEqual(expected, kata.StreetFighterSelection(fighters, new int[] { 0, 0 }, moves));
    }

    public static void Main()
    {
        try
        {
            var test = new FrontTests();
            test._01_ShouldWorkWithFewMoves();
            test._02_ShouldWorkWithNoSelectionCursorMoves();
            test._03_ShouldWorkWhenAlwaysMovingLeft();
            test._04_ShouldWorkWhenAlwaysMovingRight();
            test._05_ShouldUseAll4DirectionsClockwiseTwice();
            test._06_ShouldWorkWhenAlwaysMovingDown();
            test._07_ShouldWorkWhenAlwaysMovingUp();
        }
        catch (Exception ex)
		{
            Console.WriteLine("Exception: {0}", ex.ToString());
		}
    }
}
