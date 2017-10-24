using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetView : MonoBehaviour {
	public Menu menu;

	void OnTriggerEnter(Collider other) {
		menu.resetView();
	}

	void Activate() {
		menu.resetView();
	}
}
