using System;
using System.Collections.Generic;
using UnityEngine;

public class ClassicSwipeDetector : SwipeDetector
{
	private float minDistSpeedDetection = 1f;

	private float deltaTime = 0f;
	private float deltaPosition = 0f;

	private float speedLimit = 0.3f;

	private OnSwipe resetDeltaPos;
	private OnSwipe resetDeltaTime;

	public ClassicSwipeDetector()
	{
		oldLocalTouches = new Dictionary<int, Touch>();

		resetDeltaPos = delegate {
			deltaPosition = 0;
		};

		resetDeltaTime = delegate {
			deltaTime = 0;
		};
	}


	public override bool DetectSwipe(ref Touch[] touches, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		Touch oldTouch, newTouch;
		foreach (Touch touch in touches)
		{
			Debug.Log(touch.position);
			if (touch.phase == TouchPhase.Began)
			{
				deltaTime = 0;

				if (!oldLocalTouches.ContainsKey((int)direction))
				{
					oldLocalTouches.Add((int)direction, touch);
					oldTouch = touch;
				}
				else
				{
					oldLocalTouches[(int)direction] = touch;
					oldTouch = touch;
				}
			}
			else
			{
				if (touch.phase == TouchPhase.Stationary)
				{
					deltaTime = 0;
				}

				if (!oldLocalTouches.ContainsKey((int)direction))
				{
					oldLocalTouches.Add((int)direction, touch);
					oldTouch = touch;
				}
				else
				{
					oldTouch = oldLocalTouches[(int)direction];
				}
			}
			newTouch = touch;

			if (this.DetectSwipe(ref oldTouch, ref newTouch, direction, doSwipe, distance))
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>It detect the swipe only when the finger is lifted from the screen.
	/// It detect swipe if the distance between position is big enough or if the finger moves 
	/// faster then the speed limit
	///</summary>
	public override bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		deltaTime += newTouch.deltaTime;
		if (IsHorizontalSwipe(direction))
		{
			deltaPosition += Math.Abs(newTouch.deltaPosition.x);
		} 
		else if (IsHorizontalSwipe(direction))
		{
			deltaPosition += Math.Abs(newTouch.deltaPosition.y);
		}


		if (isTouchGood(oldTouch, newTouch))
		{
			if (oldTouch.position != newTouch.position)
			{
				StupidSwipeDetector stupidDetector = new StupidSwipeDetector();
				
				//if the finger go in the opposite direction, i reset the delta time
				stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, (SwipeDirection)(-(int)direction), resetDeltaTime, 1f);
				OnSwipe swipeAction = doSwipe + resetDeltaTime + resetDeltaPos;
				if (deltaTime/deltaPosition >= speedLimit)
				{
					Debug.Log("aaa");
					return stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, direction, swipeAction, minDistSpeedDetection);
				}
				else
				{
					Debug.Log("bbb");
					return stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, direction, swipeAction, distance);
				}
			}
		}
		return false;
	}

	private bool isTouchGood(Touch oldTouch, Touch newTouch)
	{
		return (oldTouch.phase == TouchPhase.Began || oldTouch.phase == TouchPhase.Moved)
			&& newTouch.phase == TouchPhase.Ended;
	}

	private void IncrementsDelta(SwipeDirection direction)
	{

	}

}