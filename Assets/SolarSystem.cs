using System;
using System.Collections.Generic;


public class SolarSystem
{
	public Star star;
	public List<Planet> planets = new List<Planet>(); 
	public int numOfPlanets;

	public SolarSystem (Star s)
	{
		this.star = s;
		this.numOfPlanets = Int32.Parse(s.numberOfPlanets);
	}

	public void addPlanet(Planet p){
		planets.Add (p);
	}
}


