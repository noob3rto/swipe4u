using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTester : MonoBehaviour
{

	private OnSwipe swipeRight;
	private OnSwipe swipeLeft;

	private SwipeDetector detector;


	// Start is called before the first frame update
	void Start()
    {
		detector = new ClassicSwipeDetector();

		swipeRight = delegate {
			Debug.Log("swipe right");
			transform.position += Vector3.right / 20;
		};
		swipeLeft = delegate {
			Debug.Log("swipe left");
			transform.position += Vector3.left / 20;
		};
	}

	// Update is called once per frame
	void Update()
	{

		Touch[] touches = Input.touches;

		detector.DetectSwipe(ref touches, SwipeDirection.Right, swipeRight, 50f);
		//detector.DetectSwipe(ref touches, SwipeDirection.Left, swipeLeft, 5f);


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
