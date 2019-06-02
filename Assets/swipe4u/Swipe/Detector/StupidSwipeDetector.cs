using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnSwipe(SwipeDirection swipeDir);

public class StupidSwipeDetector : MonoBehaviour
{
	private static float minDistanceForSwipe = 1f;

	public static SwipeDirection? DetectSwipe(Vector2 oldPos, Vector2 newPos, OnSwipe swipe, float distance=-1)
	{
		if (distance < 0)
		{
			distance = minDistanceForSwipe;
		}
		if (IsVerticalSwipe(oldPos.y, newPos.y, distance))
		{
			SwipeDirection direction = newPos.y - oldPos.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
			swipe(direction);
			return direction;
		}
		else if (IsHorizontalSwipe(oldPos.x, newPos.x, distance))
		{
			SwipeDirection direction = newPos.x - oldPos.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
			swipe(direction);
			return direction;
		}
		return null;
	}

	private static bool IsVerticalSwipe(float oldY, float newY, float distance)
	{
		return Mathf.Abs(newY - oldY) > distance;
	}

	private static bool IsHorizontalSwipe(float oldX, float newX, float distance)
	{
		return Mathf.Abs(newX - oldX) > distance;
	}

	private static void setMinDistanceForSwipe(float minDistanceForSwipe)
	{
		if (minDistanceForSwipe <= 0)
		{
			throw new UnityException();
		}

		StupidSwipeDetector.minDistanceForSwipe = minDistanceForSwipe;
	}

	private static float getMinDistanceForSwipe()
	{
		return minDistanceForSwipe;
	}

}