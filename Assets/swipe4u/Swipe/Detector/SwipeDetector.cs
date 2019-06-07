using System;

public abstract class SwipeDetector
{
	public abstract bool DetectSwipe(ref Touch oldTouch, ref newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1);
}