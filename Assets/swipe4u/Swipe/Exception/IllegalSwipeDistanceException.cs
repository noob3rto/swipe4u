using System;

public class IllegalSwipeDistanceException : Exception
{
	public IllegalSwipeDistanceException()
	{
	}

	public IllegalSwipeDistanceException(string message)
		: base(message)
	{
	}

	public IllegalSwipeDistanceException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
