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

	public SolarSystem(string name, float brightness, string texture, string type, float radius, int numberOfPlanets, Transform parent) {
		solarSystem = new GameObject();
		this.star = new Star(name, brightness, texture, type, radius, 
			numberOfPlanets, solarSystem.transform);
		solarSystem.name = star.name;
		this.numOfPlanets = star.numberOfPlanets;
		solarSystem.transform.parent = parent;
	}

	public void addPlanet(String[] planetItems){
		planets.Add(new Planet(planetItems, this.star, solarSystem.transform));
	}

	public void addPlanet(float radiusOfOrbit, float radiusOfPlanet, string planetLetter, string texture, float timeToOrbit) {
		planets.Add(new Planet(radiusOfOrbit, radiusOfPlanet, planetLetter,
			texture, timeToOrbit, this.star, solarSystem.transform));
	}

	public void setPosition(Vector3 position) {
		solarSystem.transform.position = position;
	}

	public void setOrbitScale(float newScale) {
		foreach(var planet in planets) {
			planet.setOrbitScale(newScale);
		}
		star.setOrbitScale(newScale);
	}

	public void setRevoScale(float newScale) {
		foreach(var planet in planets) {
			planet.setRevoScale(newScale);
		}
	}
}


