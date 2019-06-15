using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTester : MonoBehaviour
{

	private OnSwipe swipeRight;
	private OnSwipe swipeLeft;


	Touch oldTouch = new Touch();
	Touch newTouch = new Touch();


	// Start is called before the first frame update
	void Start()
    {
		swipeRight = delegate {
			Debug.Log("swipe detected");
			transform.position += Vector3.right / 20;
		};
		swipeLeft = delegate {
			Debug.Log("swipe detected");
			transform.position += Vector3.left / 20;
		};
	}

	// Update is called once per frame
	void Update()
	{

		SwipeDetector detector = new CoolSwipeDetector();

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				oldTouch = touch;
			}
			newTouch = touch;

			Debug.Log("oldTouch " + oldTouch.position);
			Debug.Log("newTouch " + newTouch.position);

			detector.DetectSwipe(ref oldTouch, ref newTouch, SwipeDirection.Right, swipeRight, 5f);
			detector.DetectSwipe(ref oldTouch, ref newTouch, SwipeDirection.Left, swipeLeft, 5f);


			/*if (touch.phase == TouchPhase.Began)
			{
				oldTouch = touch;
			}
			else if (touch.phase == TouchPhase.Moved)
			{
				newTouch = touch;

				detector.DetectSwipe(ref oldTouch);
			}*/
		}
	}


	void testSwipe()
	{
		CoolSwipeDetector coolDetector = new CoolSwipeDetector();

		Touch oldTouch = new Touch();
		Touch newTouch = new Touch();

		Vector2 oldPos = new Vector2(1, 5);
		Vector2 newPos = new Vector2(4, 60);

		oldTouch.phase = TouchPhase.Moved;
		newTouch.phase = TouchPhase.Moved;
		oldTouch.position = oldPos;
		newTouch.position = newPos;

		coolDetector.DetectSwipe(ref oldTouch, ref newTouch, SwipeDirection.Up, delegate { }, 10f);

		Debug.Log(oldTouch.position);


	}
}
