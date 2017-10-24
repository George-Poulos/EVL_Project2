using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : MonoBehaviour {
	public Menu menu;

	void OnTriggerEnter(Collider other) {
		menu.speedDown();
	}

	void Activate() {
		menu.speedDown();
	}
}
