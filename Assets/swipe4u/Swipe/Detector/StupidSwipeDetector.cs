using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidSwipeDetector : SwipeDetector
{
	private static float minDistanceForSwipe = 1f;


	public override bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		Vector2 oldPos = oldTouch.position;
		Vector2 newPos = newTouch.position;

		if (distance < 0)
		{
			distance = minDistanceForSwipe;
		}

		if (direction == SwipeDirection.Down || direction == SwipeDirection.Up)
		{
			if (IsVerticalSwipe(oldPos.y, newPos.y, distance))
			{
				SwipeDirection detectedDir = newPos.y - oldPos.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
				if (detectedDir == direction)
				{
					doSwipe(direction);
					return true;
				}
			}
		}
		else if (direction == SwipeDirection.Left || direction == SwipeDirection.Right)
		{
			if (IsHorizontalSwipe(oldPos.x, newPos.x, distance))
			{
				SwipeDirection dirDetected = newPos.x - oldPos.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
				doSwipe(direction);
				return true;
			}
		}

		return false;
	}

	private static void setMinDistanceForSwipe(float minDistanceForSwipe)
	{
		if (minDistanceForSwipe <= 0)
		{
			throw new IllegalSwipeDistanceException("Distanza per swipe negativa o nulla");
		}

		StupidSwipeDetector.minDistanceForSwipe = minDistanceForSwipe;	
	}

	private static float getMinDistanceForSwipe()
	{
		return minDistanceForSwipe;
	}

}