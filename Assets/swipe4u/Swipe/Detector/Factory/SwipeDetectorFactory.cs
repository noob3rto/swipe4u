using System;
using System.Collections.Generic;

struct DetectableDirection
{
	SwipeDirection dir;
	bool detect;
};

public class SwipeDetectorFactory
{	
	//detect swipe when taking out the finger from the screen
	private bool WHEN_TOUCH_ENDED;
	//detect swipe while moving the finger on the screen
	private bool WHEN_MOVING;

	List<DetectableDirection> directions;
	
	SwipeDetectorFactory(String configuration)
	{

	}


	/// <summary>return a type of SwipeDetector based on the configuration
	/// of the factory
	/// </summary>
	public SwipeDetector GetSwipeDetector()
	{
		if (WHEN_MOVING)
		{
			if (WHEN_TOUCH_ENDED)
			{
				//return a swipeDetector
			}

			return new CoolSwipeDetector();
		}


		return new StupidSwipeDetector();
	}

}
