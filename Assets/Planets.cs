/// Sample Code for CS 491 Virtual And Augmented Reality Course - Fall 2017
/// written by Andy Johnson
/// 
/// makes use of various textures from the celestia motherlode - http://www.celestiamotherlode.net/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planets : MonoBehaviour {
	
	public PlanetParser p;
	
	void Start () {
		p = new PlanetParser ("./Assets/Resources/planets.csv");
		p.sideMenu.setMenuPosition(new Vector3(0, 50, 30));
		p.sideMenu.updateMenu();
	}

	// Update is called once per frame
	void Update () {

	}
}
