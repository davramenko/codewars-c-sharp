using System;

// Bouncing Balls

public class BouncingBall
{
	public static int bouncingBall(double h, double bounce, double window)
	{
		if (h <= 0 || bounce <= 0 || bounce >= 1 || window >= h)
			return -1;
		int res = 1;
		for (double b = bounce * h; b > window; b *= bounce)
			res += 2;
		return res;
	}
}

static class Program
{
	static void Main()
	{
	}
}