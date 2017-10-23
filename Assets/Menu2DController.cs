using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu2DController {
	public GameObject View;
	public List<SolarSystem> menu;


	public Menu2DController() {
		View = new GameObject("2dMenu");
		menu = new List<SolarSystem>();
	}
}
