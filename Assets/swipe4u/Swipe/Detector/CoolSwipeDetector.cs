using System;
using UnityEngine;


public class CoolSwipeDetector : SwipeDetector
{

	/// <summary>It detect the swipe even if you keep your finger stuck to the screen. 
	/// In the next commits this feature will be customizable
	///</summary>
	public bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		if (isTouchGood(oldTouch, newTouch))
		{

			/*The oldPosition is updated at step of 1f. In this way the swipe can be detected 
			 * many time without taking out the finger from the screen*/
			StupidSwipeDetector.DetectSwipe(oldTouch, newTouch, delegate (SwipeDirection dir) {
				if ((int) dir == - (int) direction)
					oldTouch.position = newTouch.position;
			}, 1f);


			if (oldTouch.position != newTouch.position)
			{
				if (StupidSwipeDetector.DetectSwipe(oldTouch, newTouch, doSwipe, distance) == direction)
				{
					oldTouch.position = newTouch.position;

					return true;
				}
			}
		}
		return false;
	}

	private bool isTouchGood(Touch oldTouch, Touch newTouch)
	{
		return (oldTouch.phase == TouchPhase.Began || oldTouch.phase == TouchPhase.Moved)
			&& (newTouch.phase == TouchPhase.Moved || newTouch.phase == TouchPhase.Ended);
	}

}
