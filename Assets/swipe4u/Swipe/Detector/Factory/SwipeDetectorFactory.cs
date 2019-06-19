using System;
using System.Collections.Generic;

struct DetectableDirection
{
	SwipeDirection dir;
	bool detect;
};

public class SwipeDetectorFactory
{
	private SwipeSetting[] settings;

	private	List<DetectableDirection> directions;
	
	SwipeDetectorFactory(SwipeSetting[] settings)
	{
		this.settings = settings;
	}


	/// <summary>return a type of SwipeDetector based on the configuration
	/// of the factory
	/// </summary>
	public SwipeDetector GetSwipeDetector()
	{

		foreach (SwipeSetting setting1 in settings)
		{
			if (SwipeSetting.InAStupidWay == setting1)
			{
				return new StupidSwipeDetector();
			}
			else if (SwipeSetting.WhenTouchMove == setting1)
			{
				foreach (SwipeSetting setting2 in settings)
				{
					if (SwipeSetting.WhenTouchEnd == setting2)
					{
						return new ClassicSwipeDetector();
					}
				}

				return new CoolSwipeDetector();
			}
		}

		return null;
	}

}
