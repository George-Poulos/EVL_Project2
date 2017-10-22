using System;
using System.Collections.Generic;

public class PlanetParser
{
	public Dictionary<string, SolarSystem> dict = new Dictionary<string, SolarSystem>();

	public PlanetParser (string fileName)
	{
		string text = System.IO.File.ReadAllText(fileName);
		string[] csvLines = text.Split ('\n');
		string[] keys;
		int i = 0;
		foreach (string line in csvLines) {
			string[] items = line.Split (',');
			if (i == 0) {
				keys = items;
			}
			else{
				if (dict.ContainsKey (items [1])) {
					dict [items [1]].addPlanet(new Planet(items));
				} else {
					dict.Add (items [1], new SolarSystem(new Star(items)));
					dict [items [1]].addPlanet (new Planet (items));
				}
			}
			i++;
		}
	}
}

