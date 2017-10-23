using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnTrigger : MonoBehaviour {
	public SolarSystem target;
	public delegate void EnterAction(SolarSystem target);
    public static event EnterAction onEnter;


	void OnTriggerEnter(Collider other) {
		if(onEnter != null)
        {
            onEnter(target);
        }
    }
}
