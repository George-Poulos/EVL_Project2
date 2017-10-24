using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDown : MonoBehaviour {
	public Menu menu;

	void OnTriggerEnter(Collider other) {
		menu.planetDown();
	}

	void Activate() {
		menu.planetDown();
	}
}
