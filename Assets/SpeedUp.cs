using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour {
	public Menu menu;

	void OnTriggerEnter(Collider other) {
		menu.speedUp();
	}

	void Activate() {
		menu.speedUp ();
	}
}
