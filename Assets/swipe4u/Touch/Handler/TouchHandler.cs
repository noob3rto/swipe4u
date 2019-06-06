using System;
using UnityEngine;

public class TouchHandler
{
	public Touch oldTouch, newTouch, firstTouch;

	public float x;
	public float xIncrement = 0f;
	public float xProportion;

	public static float zSpeed = 15f;

	//movement follow finger through x axis
	private bool FOLLOWX;
	//movement follow finger through y axis
	private bool FOLLOWY;

	private SwipeDetector swipeDetector;

	public TouchHandler(bool FOLLOWX, bool FOLLOWY)
	{
		this.FOLLOWX = FOLLOWX;
		this.FOLLOWY = FOLLOWY;
	}


	public void Handle(Touch[] touches, ref Transform transform)
	{
		foreach (Touch touch in touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				oldTouch = touch;
				firstTouch = touch;
			}
			else if (touch.phase == TouchPhase.Moved)
			{
				newTouch = touch;

				//doMovement

				//doSwipe
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				//doSwipe
			}

		}
	}

	public void setSwipeDetector(SwipeDetector swipeDetector)
	{
		this.swipeDetector = swipeDetector;
	}
}