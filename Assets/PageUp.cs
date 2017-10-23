using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageUp : MonoBehaviour {
	public Menu2DController menu;

	void OnTriggerEnter(Collider other) {
		menu.pgUp();
    }
}
