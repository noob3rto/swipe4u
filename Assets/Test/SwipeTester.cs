using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		testSwipe();
    }

    // Update is called once per frame
    void Update()
    {

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
