using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem 
{
	public GameObject solarSystem;

	public Star star;
	public List<Planet> planets = new List<Planet>(); 
	public int numOfPlanets;

	public SolarSystem(String[] starItems, Transform parent)
	{
		solarSystem = new GameObject();
		this.star = new Star(starItems, solarSystem.transform);
		solarSystem.name = star.name;
		this.numOfPlanets = star.numberOfPlanets;
		solarSystem.transform.parent = parent;
	}

	public void addPlanet(String[] planetItems){
		planets.Add(new Planet(planetItems, this.star, solarSystem.transform));
	}

	public void setPosition(Vector3 position) {
		solarSystem.transform.position = position;
	}
}


