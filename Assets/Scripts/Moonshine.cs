using UnityEngine;

public class Moonshine : TimedObject
{
    void Start()
    {
        secondsOnScreen = GameParameters.MoonshineSecondsOnScreen;
        base.Start();
    }

}
