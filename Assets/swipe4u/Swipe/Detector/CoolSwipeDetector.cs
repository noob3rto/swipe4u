using System;
using System.Collections.Generic;
using UnityEngine;


public class CoolSwipeDetector : SwipeDetector
{

	private Dictionary<int, Touch> oldLocalTouches;
	

	public CoolSwipeDetector()
	{
		oldLocalTouches = new Dictionary<int, Touch>();
	}

	/// <summary>It detect the swipe even if you keep your finger stuck to the screen.
	/// It doesn't consider the movement speed of the finger
	///</summary>
	public override bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		if (isTouchGood(oldTouch, newTouch))
		{
			if (oldTouch.phase == TouchPhase.Began)
			{
				if (!oldLocalTouches.ContainsKey((int)direction))
				{
					oldLocalTouches.Add((int)direction, oldTouch);
				}
				else
				{
					oldLocalTouches[(int)direction] = oldTouch;
				}
			}

			Touch oldLocalTouch= oldLocalTouches[(int)direction];
			
			//I need to copy because I can't pass ref to delegates
			Touch newTouchCopy = newTouch;

			OnSwipe doUpdateLocalTouch = delegate (SwipeDirection dir) {
				oldLocalTouch.position = newTouchCopy.position;
				oldLocalTouches[(int)direction] = oldLocalTouch;
			};

			StupidSwipeDetector stupidDetector = new StupidSwipeDetector();

			/*The oldPosition is updated at step of 1f. In this way the swipe can be detected 
			 * many time without taking out the finger from the screen
			 * I update only the local copy, in this way it's possible to detect opposite swipes simultaneously
			 */
			stupidDetector.DetectSwipe(ref oldLocalTouch, ref newTouch, (SwipeDirection)(-(int)direction), doUpdateLocalTouch, 1f);
			

			if (oldLocalTouch.position != newTouch.position)
			{
				Touch oldTouchCopy = oldTouch;
				
				OnSwipe doUpdateRefTouch = delegate (SwipeDirection dir) {
					oldTouchCopy.position = newTouchCopy.position;
				};
				//if the swipe is detected, I update the real oldTouch
				if (stupidDetector.DetectSwipe(ref oldLocalTouch, ref newTouch, direction, doSwipe + doUpdateRefTouch + doUpdateLocalTouch, distance))
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
