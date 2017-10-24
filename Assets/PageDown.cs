using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageDown : MonoBehaviour {
	public Menu2DController menu;

	void OnTriggerEnter(Collider other) {
		menu.pgDown();
    }

    void Activate() {
    	menu.pgDown();
    }
}
