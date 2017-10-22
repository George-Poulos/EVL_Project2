using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPosition : MonoBehaviour {

    public static float position = 1f;
    public static bool isScaling = false;
    public float originalPosition = 1f;
	
	// Update is called once per frame
	void Update () {
        if (isScaling)
        {
            //transform.position = new Vector3(transform.position.x, 0.0f, originalPosition*positionVariable);
            transform.localPosition = new Vector3(0.0f, 0.0f, (originalPosition * position));
        }	
	}
}
