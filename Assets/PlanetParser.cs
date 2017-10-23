using System;
using System.Collections.Generic;
using UnityEngine; 

public class PlanetParser
{
	public Dictionary<string, SolarSystem> dict = new Dictionary<string, SolarSystem>();
	private float[] solOrbit = new float[8] {
		57910000, 108200000, 149600000, 227900000, 778500000, 1433000000, 2877000000, 4503000000
	};
	private float[] solRadius = new float[8] {
		2440, 6052, 6371, 3400, 69911, 58232, 25362, 24622
	};
	private float[] solTime = new float[8] {
		0.24F, 0.62F, 1.00F, 1.88F, 11.86F, 29.46F, 84.01F, 164.80F
	};
	private string[,] solData = new string[8, 2] {
		{"mercury", "mercury"},
		{"venus",   "venus"},
        {"earthmap", "earth"},
        {"mars",     "mars"},
        {"jupiter", "jupiter"},
        {"saturn",   "saturn"},
        {"neptune", "neptune"},
        {"uranus", "uranus"}
	};

	public PlanetParser (string fileName)
	{
		GameObject universe = new GameObject("Universe");
		var oneOffset = new Vector3 (0, -15, 0);
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
			system.setPosition(oneOffset);
		}
		var ourSystem = createSol(universe);
		dict.Add("Our Sun", ourSystem);
	}

	private SolarSystem createSol(GameObject universe) {
		var systemOffset = new Vector3 (0, 0, 0);
		SolarSystem ourSystem = new SolarSystem(
			"Our Sun",
			1.0F,
			"sol",
			"G2V",
			695700,
			8,
			universe.transform
		);
		for(int i = 0; i < 8; i++) {
			ourSystem.addPlanet(
				solOrbit[i], 
				solRadius[i], 
				solData[i, 1], 
				solData[i, 0], 
				solTime[i]
			);
		}
		ourSystem.setPosition(systemOffset);
		return ourSystem;
	}
}

