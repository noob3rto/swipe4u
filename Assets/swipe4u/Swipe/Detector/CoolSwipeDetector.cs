using System.Collections.Generic;
using UnityEngine;

public class CoolSwipeDetector : SwipeDetector
{

	public CoolSwipeDetector()
	{
		oldLocalTouches = new Dictionary<int, Touch>();
	}


	public override bool DetectSwipe(ref Touch[] touches, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		Touch oldTouch, newTouch;
		foreach (Touch touch in touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Debug.Log("a");
				if (!oldLocalTouches.ContainsKey((int)direction))
				{
					Debug.Log("b");
					oldLocalTouches.Add((int)direction, touch);
					oldTouch = touch;
				}
				else
				{
					Debug.Log("c");
					oldLocalTouches[(int)direction] = touch;
					oldTouch = touch;
				}
			}

			if (!oldLocalTouches.ContainsKey((int)direction))
			{
				Debug.Log("d");
				oldLocalTouches.Add((int)direction, touch);
				oldTouch = touch;
			}
			else
			{
				Debug.Log("e");
				oldTouch = oldLocalTouches[(int)direction];
			}

			newTouch = touch;

			if (this.DetectSwipe(ref oldTouch, ref newTouch, direction, doSwipe, distance))
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>It detect the swipe even if you keep your finger stuck to the screen.
	/// It doesn't consider the movement speed of the finger
	///</summary>
	public override bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		if (isTouchGood(oldTouch, newTouch))
		{
			//I need to copy because I can't pass ref to delegates
			Touch newTouchCopy = newTouch;
			Touch oldTouchCopy = oldTouch;

			OnSwipe doUpdateLocalTouch = delegate (SwipeDirection dir) {
				oldTouchCopy.position = newTouchCopy.position;
				oldLocalTouches[(int)direction] = oldTouchCopy;
			};

			StupidSwipeDetector stupidDetector = new StupidSwipeDetector();

			/*The oldPosition is updated at step of 1f. In this way the swipe can be detected 
			 * many time without taking out the finger from the screen
			 * I update only the local copy, in this way it's possible to detect opposite swipes simultaneously
			 */
			stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, (SwipeDirection)(-(int)direction), doUpdateLocalTouch, 1f);
			

			if (oldTouchCopy.position != newTouch.position)
			{
				OnSwipe doUpdateRefTouch = delegate (SwipeDirection dir) {
					oldTouchCopy.position = newTouchCopy.position;
				};
				//if the swipe is detected, I update the real oldTouch
				if (stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, direction, doSwipe + doUpdateRefTouch + doUpdateLocalTouch, distance))
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
