using System;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

// Fluent Calculator

// https://www.codewars.com/kata/fluent-calculator-1/train/csharp
// https://ru.wikibooks.org/wiki/%D0%A0%D0%B5%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D0%B8_%D0%B0%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC%D0%BE%D0%B2/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4_%D1%80%D0%B5%D0%BA%D1%83%D1%80%D1%81%D0%B8%D0%B2%D0%BD%D0%BE%D0%B3%D0%BE_%D1%81%D0%BF%D1%83%D1%81%D0%BA%D0%B0

// "C:\Program Files (x86)\MSBuild\14.0\Bin\csc.exe" /debug+ /langversion:6 /r:"C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\mstest.testframework\1.3.2\lib\net45\Microsoft.VisualStudio.TestPlatform.TestFramework.dll" /r:C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Runtime.dll "026 - Fluent Calculator.cs"
// /r:"System.Runtime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

namespace Kata
{
	public class FluentCalculator
	{
		int _pos = 0;
		StringBuilder _expr = new StringBuilder();
		bool _req_val = true;
		double _left = 0.0;
		int _number = 0;
		char _token = '$';

		public FluentCalculator() { }

		FluentCalculator addValue(string val)
		{
			//Console.WriteLine("addValue: req-val={0}; val={1}", _req_val, val);
			if (!_req_val)
				return null;
			_expr.Append(val);
			_req_val = false;
			return this;
		}

		public FluentCalculator Zero
		{ get { return addValue("0"); } }
		public FluentCalculator One
		{ get { return addValue("1"); } }
		public FluentCalculator Two
		{ get { return addValue("2"); } }
		public FluentCalculator Three
		{ get { return addValue("3"); } }
		public FluentCalculator Four
		{ get { return addValue("4"); } }
		public FluentCalculator Five
		{ get { return addValue("5"); } }
		public FluentCalculator Six
		{ get { return addValue("6"); } }
		public FluentCalculator Seven
		{ get { return addValue("7"); } }
		public FluentCalculator Eight
		{ get { return addValue("8"); } }
		public FluentCalculator Nine
		{ get { return addValue("9"); } }
		public FluentCalculator Ten
		{ get { return addValue("10"); } }

		FluentCalculator addOperation(string op)
		{
			//Console.WriteLine("addOperation: req-val={0}; op=\"{1}\"", _req_val, op);
			if (_req_val)
				return null;
			_expr.Append(op);
			_req_val = true;
			return this;
		}

		public FluentCalculator Plus
		{ get { return addOperation("+"); } }
		public FluentCalculator Minus
		{ get { return addOperation("-"); } }
		public FluentCalculator Times
		{ get { return addOperation("*"); } }
		public FluentCalculator DividedBy
		{ get { return addOperation("/"); } }

		char get_token()
		{
			if (_pos >= _expr.Length)
			{
				_token = '$';
			}
			else
			{
				_token = _expr[_pos++];
				if (char.IsNumber(_token))
				{
					StringBuilder buff = new StringBuilder();
					buff.Append(_token);
					_token = '#';
					while (_pos < _expr.Length && char.IsNumber(_expr[_pos]))
						buff.Append(_expr[_pos++]);
					_number = Convert.ToInt32(buff.ToString());
					//Console.WriteLine("number: [{0}]", _number);
				}
			}
			//Console.WriteLine("token: [{0}]", _token);
			return _token;
		}

		double prim(bool read)
		{
			if (read)
				get_token();

			//Console.WriteLine("prim: read={0}, [{1}]", read, _token);
			switch (_token)
			{
				case '#':
				{
					double v = _number;
					get_token();
					return v;
				}
				default:
					//Console.WriteLine("ERROR: Primary expected");
					throw new Exception("Primary expected");
			}
		}

		double term(bool read)
		{
			double left = prim(read);

			while (true)
			{
				//Console.WriteLine("term: [{0}]", _token);
				switch (_token)
				{
					case '*':
						left *= prim(true);
						break;
					case '/':
						left /= prim(true);
						break;
					default:
						return left;
				}
			}
		}

		double expr(bool read)
		{
			double left = term(read);

			while (true)
			{
				//Console.WriteLine("expr: [{0}], left={1}", _token, left);
				switch (_token)
				{
					case '+':
						left += term(true);
						//Console.WriteLine("expr: [{0}], left={1}", _token, left);
						break;
					case '-':
						left -= term(true);
						break;
					default:
						return left;
				}
			}
		}

		public double Result()
		{
			//Console.WriteLine("req-val: {0}; expr=\"{1}\"", _req_val, _expr.ToString());
			if (_req_val)
				return Double.NaN;
			try
			{
				_pos = 0;
				while (get_token() != '$')
				{
					_left = expr(false);
				}
			}
			//catch (Exception ex)
			//{
			//	Console.WriteLine("Exception result: {0}", ex.ToString());
			//	return Double.NaN;
			//}
			catch
			{
				return Double.NaN;
			}
			_expr = new StringBuilder();
			_req_val = true;
			return _left;
		}

		public static implicit operator double(FluentCalculator c)
		{
			return c.Result();
		}
	}

	public static class Test
	{
		static void Main()
		{
			try
			{
				BasicAddition();
				MultipleInstances();
				MultipleCalls();
				Bedmas();
				StaticCombinationCalls();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: {0}", ex.ToString());
				Environment.Exit(1);
			}
		}

		public static void BasicAddition()
		{
			var calculator = new FluentCalculator();
			Assert.AreEqual(3, calculator.One.Plus.Two.Result());
		}

		public static void MultipleInstances()
		{
			var calculatorOne = new FluentCalculator();
			var calculatorTwo = new FluentCalculator();
			Assert.AreNotEqual((double)calculatorOne.Five.Plus.Five, (double)calculatorTwo.Seven.Times.Three);
		}

		public static void MultipleCalls()
		{
			var calculator = new FluentCalculator();
			Assert.AreEqual(4, calculator.One.Plus.One.Result() + calculator.One.Plus.One.Result());
		}

		public static void Bedmas()
		{
			var calculator = new FluentCalculator();
			Assert.AreEqual(58, (double)calculator.Six.Times.Six.Plus.Eight.DividedBy.Two.Times.Two.Plus.Ten.Times.Four.DividedBy.Two.Minus.Six);
			Assert.AreEqual(-11.972, calculator.Zero.Minus.Four.Times.Three.Plus.Two.DividedBy.Eight.Times.One.DividedBy.Nine, 0.01);
		}

		public static void StaticCombinationCalls()
		{
			var calculator = new FluentCalculator();
			Assert.AreEqual(177.5, 10 * calculator.Six.Plus.Four.Times.Three.Minus.Two.DividedBy.Eight.Times.One.Minus.Five.Times.Zero);
		}
	}
}