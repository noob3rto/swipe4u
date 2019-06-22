using System;
using UnityEngine;

public class ClassicSwipeDetector : SwipeDetector
{
	private float minDistSpeedDetection = 1f;

	private float deltaTime = 0f;
	private float deltaPosition = 0f;

	private float speedLimit = 0.01f;


	public override bool DetectSwipe(ref Touch[] touches, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		//TODO

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
				OnSwipe resetDelta = delegate {
					deltaTime = 0f;
					deltaPosition = 0f;
				};

				StupidSwipeDetector stupidDetector = new StupidSwipeDetector();

				if (deltaTime/deltaPosition >= speedLimit)
				{
					return stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, direction, doSwipe + resetDelta, minDistSpeedDetection);
				}
				else
				{
					return stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, direction, doSwipe + resetDelta, distance);
				}
			}
		}
		else if (TouchPhase.Stationary == newTouch.phase)
		{
			deltaTime = 0f;
			deltaPosition = 0f;
		}
		//TODO: set deltas if the finger moves in the opposite direction

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