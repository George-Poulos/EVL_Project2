using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu2DController {
	public GameObject View, upPager, downPager;
	public List<SolarSystem> menu;
	public SolarSystem visible;

	private int page;
	public Menu2DController() {
		View = new GameObject("2dMenu");
		View.transform.position = new Vector3(0, 0, 0);

		Vector3 buttonScale = new Vector3(3, 3, 0.1F);

		upPager = GameObject.CreatePrimitive(PrimitiveType.Cube);
		upPager.name = "Page Up Button";
		upPager.transform.parent = View.transform;
		upPager.transform.localScale = buttonScale;
		upPager.transform.localPosition = new Vector3(-25, -25, 0);
		upPager.AddComponent<PageUp>();
		var collider = upPager.GetComponent<BoxCollider>();
		collider.isTrigger = true;
		var upScript = upPager.GetComponent<PageUp>();
		upScript.menu = this;

		downPager = GameObject.CreatePrimitive(PrimitiveType.Cube);
		downPager.name = "Page Down Button";
		downPager.transform.parent = View.transform;
		downPager.transform.localScale = buttonScale;
		downPager.transform.localPosition = new Vector3(-25, -20, 0);
		downPager.AddComponent<PageDown>();
		collider = downPager.GetComponent<BoxCollider>();
		collider.isTrigger = true;
		var downScript = downPager.GetComponent<PageDown>();
		downScript.menu = this;

		menu = new List<SolarSystem>();
		visible = null;
		ToggleOnTrigger.onEnter += ChangeVisible;
		page = 0;
	}

	public void updateMenu() {
		Vector3 start = new Vector3(0, 0, 0);
		Vector3 offset = new Vector3(0, -5, 0);
		int i = 0;
		int begin = 10 * page;
		int end = begin + 10;
		foreach(var system in menu) {
			if(i >= begin && i < end) {
				system.set2dPosition(start);
				start += offset;
				system.sideSystem.SetActive(true);
			} else {
				system.sideSystem.SetActive(false);
			}
			i++;
		}
	}

	public void pgUp() {
		this.page = (10 * page > menu.Count) ? page : page + 1;
		updateMenu();
	}

	public void pgDown() {
		this.page = (page == 0) ? 0 : page - 1;
		updateMenu();
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
