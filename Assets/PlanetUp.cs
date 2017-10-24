using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUp : MonoBehaviour {
	public Menu menu;

	void OnTriggerEnter(Collider other) {
		menu.planetUp();
	}

	void Activate() {
		menu.planetUp();
	}
}
