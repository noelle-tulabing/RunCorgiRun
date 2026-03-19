using UnityEngine;

public class MoonshinePlacer : TimedObjectPlacer
{
    public void Start()
    {
        minSecondsToWait = GameParameters.MoonshineMinSecondsToWait;
        maxSecondsToWait = GameParameters.MoonshineMaxSecondsToWait;
    }

    public override void Place()
    {
        Instantiate(ObjectPrefab, SpawnTools.RandomTopOfScreenLocationWorldSpace(), Quaternion.identity);
    }
}
