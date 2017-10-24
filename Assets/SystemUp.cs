using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUp : MonoBehaviour {
	public Menu menu;

	void OnTriggerEnter(Collider other) {
		menu.scaleSystemUp();
	}

	void Activate() {
		menu.scaleSystemUp();
	}
}
