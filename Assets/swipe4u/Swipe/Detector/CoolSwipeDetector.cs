using System;
using UnityEngine;


public class CoolSwipeDetector : SwipeDetector
{

	/// <summary>It detect the swipe even if you keep your finger stuck to the screen. 
	///</summary>
	public override bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		if (isTouchGood(oldTouch, newTouch))
		{
			StupidSwipeDetector stupidDetector = new StupidSwipeDetector();

			//I need to copy because I can't pass ref to delegates
			Touch oldTouchCopy = oldTouch;
			Touch newTouchCopy = newTouch;

			OnSwipe doUpdateTouchPos = delegate (SwipeDirection dir) {
				oldTouchCopy.position = newTouchCopy.position;
			};

			/*The oldPosition is updated at step of 1f. In this way the swipe can be detected 
			 * many time without taking out the finger from the screen*/
			stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, (SwipeDirection)(-(int) direction), doUpdateTouchPos, 1f);

			oldTouch = oldTouchCopy;

			if (oldTouch.position != newTouch.position)
			{
				if (stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, direction, doSwipe + doUpdateTouchPos, distance))
				{
					oldTouch = oldTouchCopy;

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
