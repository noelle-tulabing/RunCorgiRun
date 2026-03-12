using System.Collections;
using UnityEngine;

public class BeerPlacer : TimedObjectPlacer
{
   public void Start()
   {
      minSecondsToWait = GameParameters.BeerMinSecondsToWait;
      maxSecondsToWait = GameParameters.BeerMaxSecondsToWait;
   }
}
