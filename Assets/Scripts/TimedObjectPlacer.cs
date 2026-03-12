using System.Collections;
using UnityEngine;

public class TimedObjectPlacer : MonoBehaviour
{
    public GameObject ObjectPrefab;
    
    public float minSecondsToWait;
    public float maxSecondsToWait;
    
    private bool canCreateObject = true;
    void Update()
    {
        if (canCreateObject)
        {
            StartCoroutine(CountdownUntilCreation());
        }
    }
    
    IEnumerator CountdownUntilCreation()
    {
        canCreateObject = false;
        float secondsToWait = Random.Range(minSecondsToWait, maxSecondsToWait);
        yield return new WaitForSeconds(secondsToWait);
        Place();
        canCreateObject = true;
    }

    private void Place()
    {
        Instantiate(ObjectPrefab, SpawnTools.RandomLocationWorldSpace(), Quaternion.identity);
    }
}
