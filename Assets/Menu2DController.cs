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

		upPager = initButton("Page Up Button", new Vector3(-25, -25, 0), "↓");
		upPager.AddComponent<PageUp>();
		var upScript = upPager.GetComponent<PageUp>();
		upScript.menu = this;

		downPager = initButton("Page Down Button", new Vector3(-25, -20, 0), "↑");
		downPager.AddComponent<PageDown>();
		var downScript = downPager.GetComponent<PageDown>();
		downScript.menu = this;

		menu = new List<SolarSystem>();
		visible = null;
		ToggleOnTrigger.onEnter += ChangeVisible;
		page = 0;
	}

	private GameObject initButton(string name, Vector3 position, string text) {
		Vector3 buttonScale = new Vector3(3, 3, 0.1F);
		var buttonMaterial = new Material(Shader.Find("Standard"));
		buttonMaterial.mainTexture = Resources.Load("button") as Texture;

		var ret = GameObject.CreatePrimitive(PrimitiveType.Cube);
		ret.name = name;
		ret.transform.parent = View.transform;
		ret.transform.localScale = buttonScale;
		ret.transform.localPosition = position;
		ret.layer = 8;
		var collider = ret.GetComponent<BoxCollider>();
		collider.isTrigger = true;
		ret.GetComponent<MeshRenderer>().material = buttonMaterial;

		var label = new GameObject();
		label.transform.parent = ret.transform;	
		label.transform.localPosition = new Vector3(-0.25F, 0.7F, 0);
		label.name = name + " label";
		label.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
		var labelMesh = label.AddComponent<TextMesh>();
		labelMesh.text = text;
		labelMesh.fontSize = 100;
		labelMesh.color = Color.white;	
		return ret;
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

	public void resetView() {
		this.page = 0;
		if (visible != null) {
			visible.solarSystem.SetActive (false);
		}
		visible = null;
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
