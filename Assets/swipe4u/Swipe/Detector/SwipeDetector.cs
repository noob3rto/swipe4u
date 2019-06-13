using System;
using UnityEngine;

public delegate void OnSwipe(SwipeDirection swipeDir);

public abstract class SwipeDetector
{
	public abstract bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1);


	public bool IsVerticalSwipe(SwipeDirection direction)
	{
		return SwipeDirection.Down == direction || SwipeDirection.Up == direction;
	}

	public bool IsHorizontalSwipe(SwipeDirection direction)
	{
		return SwipeDirection.Left == direction || SwipeDirection.Right == direction;
	}

	public bool IsVerticalSwipe(float oldY, float newY, float distance)
	{
		return Mathf.Abs(newY - oldY) > distance;
	}

	public bool IsHorizontalSwipe(float oldX, float newX, float distance)
	{
		return Mathf.Abs(newX - oldX) > distance;
	}

}