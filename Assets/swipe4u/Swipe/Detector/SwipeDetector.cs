using System;
using UnityEngine;

public delegate void OnSwipe(SwipeDirection swipeDir);

public abstract class SwipeDetector
{
	public abstract bool DetectSwipe(ref Touch oldTouch, ref Touch newTouch, SwipeDirection direction, OnSwipe doSwipe, float distance = -1);
}