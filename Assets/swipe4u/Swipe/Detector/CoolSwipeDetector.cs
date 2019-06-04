using System;
using UnityEngine;


public class CoolSwipeDetector
{

	/// <summary>It detect the swipe even if you keep your finger stuck to the screen. 
	/// In the next commits this feature will be customizable
	///</summary>
	public bool DetectSwipe(ref Vector2 oldPos, Vector2 newPos, SwipeDirection direction, OnSwipe doSwipe, float distance = -1)
	{
		Vector2 oldPosCopy = oldPos;

		/*The oldPosition is updated at step of 1f. In this way the swipe can be detected 
		 * many time without taking out the finger from the screen*/
		StupidSwipeDetector.DetectSwipe(oldPosCopy, newPos, delegate (SwipeDirection dir) {
			if ((int) dir == - (int) direction)
				oldPosCopy = newPos;
		}, 1f);

		oldPos = oldPosCopy;


		if (oldPos != newPos)
		{
			return StupidSwipeDetector.DetectSwipe(oldPosCopy, newPos, doSwipe, distance) == direction;
		}
		
		return false;
	}


}
