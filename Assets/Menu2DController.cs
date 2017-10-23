using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu2DController {
	public GameObject View;
	public List<SolarSystem> menu;
	public SolarSystem visible;

	public Menu2DController() {
		View = new GameObject("2dMenu");
		View.transform.position = new Vector3(0, 0, 0);
		menu = new List<SolarSystem>();
		visible = null;
		ToggleOnTrigger.onEnter += ChangeVisible;
	}

	public void updateMenu() {
		Vector3 start = new Vector3(0, 0, 0);
		Vector3 offset = new Vector3(0, -5, 0);
		int i = 0;
		foreach(var system in menu) {
			if(i < 10) {
				system.set2dPosition(start);
				start += offset;
				system.sideSystem.SetActive(true);
			} else {
				system.sideSystem.SetActive(false);
			}
			i++;
		}
	}

	public void addSolarSystem(SolarSystem system) {
		menu.Add(system);
	}

	public void setMenuPosition(Vector3 position) {
		View.transform.position = position;
	}

	public void logMenu() {
		foreach(var system in menu) {
			Debug.Log(system.star.name);
		}
	}

	void ChangeVisible(SolarSystem target) {
		if(visible != null && visible.star.name != target.star.name) {
			visible.solarSystem.SetActive(false);
			target.solarSystem.SetActive(true);
			visible = target;
		} else {
			visible = target;
			visible.solarSystem.SetActive(true);
		}
	}
}
