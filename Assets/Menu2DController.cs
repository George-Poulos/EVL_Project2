using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu2DController {
	public GameObject View;
	public List<SolarSystem> menu;


	public Menu2DController() {
		View = new GameObject("2dMenu");
		View.transform.position = new Vector3(0, 0, 0);
		menu = new List<SolarSystem>();
	}

	public void updateMenu() {
		Vector3 start = new Vector3(0, 0, 0);
		Vector3 offset = new Vector3(0, -5, 0);
		foreach(var system in menu) {
			system.set2dPosition(start);
			start += offset;
		}
	}

	public void setMenuPosition(Vector3 position) {
		View.transform.position = position;
	}

	public void logMenu() {
		foreach(var system in menu) {
			Debug.Log(system.star.name);
		}
	}
}
