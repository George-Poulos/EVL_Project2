using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDown : MonoBehaviour {
	public Menu menu;

	void OnTriggerEnter(Collider other) {
		menu.scaleSystemDown();
	}

	void Activate() {
		menu.scaleSystemDown();
	}
}
