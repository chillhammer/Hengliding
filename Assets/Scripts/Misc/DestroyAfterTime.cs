using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float secondsFromStartToDestroy;
    
    // Update is called once per frame
    void Update()
    {
        if ((secondsFromStartToDestroy -= Time.deltaTime) <= 0) {
            Destroy(gameObject);
        }
    }
}
