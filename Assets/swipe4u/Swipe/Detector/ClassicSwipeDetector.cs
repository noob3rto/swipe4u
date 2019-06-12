using UnityEngine;

public class ClassicSwipeDetector : SwipeDetector
{

	/// <summary>It detect the swipe only when the touch end. 
	///</summary>
	public override bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		if (isTouchGood(oldTouch, newTouch))
		{
			StupidSwipeDetector stupidDetector = new StupidSwipeDetector();
			
			if (oldTouch.position != newTouch.position)
			{
				if (stupidDetector.DetectSwipe(ref oldTouch, ref newTouch, direction, doSwipe, distance))
				{
					return true;
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

}