using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem 
{
	public GameObject solarSystem, sideSystem;

	public Star star;
	public List<Planet> planets = new List<Planet>(); 
	public int numOfPlanets;

	public SolarSystem(String[] starItems, Transform parent, Transform sideView)
	{
		solarSystem = new GameObject();
		sideSystem = new GameObject();
		sideSystem.layer = 8;
		this.star = new Star(starItems, solarSystem.transform, sideSystem.transform);
		
		sideSystem.AddComponent<BoxCollider>();
		var collider = sideSystem.GetComponent<BoxCollider>();
		collider.isTrigger = true;
		collider.size = this.star.get2dSize();

		sideSystem.AddComponent<ToggleOnTrigger>();
		var script = sideSystem.GetComponent<ToggleOnTrigger>();
		script.target = this;

		solarSystem.name = star.name;
		sideSystem.name = star.name + " side view";
		this.numOfPlanets = star.numberOfPlanets;
		solarSystem.transform.parent = parent;
		sideSystem.transform.parent = sideView;
	}

	public SolarSystem(string name, string discovered, float brightness, string texture, string type, float radius, int numberOfPlanets, string distanceAwayFromUs, Transform parent, Transform sideView) {
		solarSystem = new GameObject();
		sideSystem = new GameObject();
		sideSystem.layer = 8;
		this.star = new Star(name, discovered, brightness, texture, type, 
			radius, numberOfPlanets, distanceAwayFromUs, solarSystem.transform, 
			sideSystem.transform);
		solarSystem.name = star.name;
		sideSystem.name = star.name + " side view";
		this.numOfPlanets = star.numberOfPlanets;
		solarSystem.transform.parent = parent;
		sideSystem.transform.parent = sideView;
	}

	public void addPlanet(String[] planetItems){
		var planet = new Planet(planetItems, this.star, solarSystem.transform, sideSystem.transform);
		if(planet.outOfBounds) this.star.setIndicator(true);
		planets.Add(planet);
	}

	public void addPlanet(float radiusOfOrbit, float radiusOfPlanet, string planetLetter, string texture, float timeToOrbit) {
		var planet = new Planet(radiusOfOrbit, radiusOfPlanet, planetLetter,
			texture, timeToOrbit, this.star, solarSystem.transform, sideSystem.transform);
		if(planet.outOfBounds) this.star.setIndicator(true);
		planets.Add(planet);
	}

	public void set3dPosition(Vector3 position) {
		solarSystem.transform.position = position;
	}

	public void set2dPosition(Vector3 position) {
		sideSystem.transform.localPosition = position;
	}

	public void setOrbitScale(float newScale) {
		bool off = false;
		foreach(var planet in planets) {
			planet.setOrbitScale(newScale);
			if(planet.outOfBounds) {
				off = true;
			}
		}
		star.setIndicator(off);
		star.setOrbitScale(newScale);
	}

	public void setRevoScale(float newScale) {
		foreach(var planet in planets) {
			planet.setRevoScale(newScale);
		}
	}

	public void setSizeScale(float newScale) {
		foreach(var planet in planets) {
			planet.setSizeScale(newScale);
		}
	}
}
