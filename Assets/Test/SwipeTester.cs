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

		Vector2 oldPos = new Vector2(1, 5);
		Vector2 newPos = new Vector2(4, 60);

		coolDetector.DetectSwipe(ref oldPos, newPos, SwipeDirection.Up, delegate { }, 10f);
		Debug.Log(oldPos);
	}
}
