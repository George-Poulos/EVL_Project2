using System;
using System.Collections.Generic;
using UnityEngine; 

public class PlanetParser
{
	public Dictionary<string, SolarSystem> dict = new Dictionary<string, SolarSystem>();

	public PlanetParser (string fileName)
	{
		GameObject universe = new GameObject("Universe");
		var systemOffset = new Vector3 (0, 0, 0);
		var oneOffset = new Vector3 (0, -30, 0);
		string text = System.IO.File.ReadAllText(fileName);
		string[] csvLines = text.Split ('\n');
		bool first = false;
		foreach (string line in csvLines) {
			string[] items = line.Split (',');
			if (!first) {
				first = !first;
			}
			else{
				if (dict.ContainsKey (items [1])) {
					dict[items[1]].addPlanet(items);
				} else {
					SolarSystem system = new SolarSystem(items, universe.transform);
					dict.Add(items [1], system);
					system.addPlanet(items);
				}
			}
		}
		foreach(var system in dict.Values) {
			system.setPosition(systemOffset);
			systemOffset += oneOffset;
		}
	}
}

