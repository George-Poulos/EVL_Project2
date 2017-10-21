using System;
using System.Collections.Generic;

public class PlanetParser
{
	public Dictionary<string, Dictionary<string, Planet>> dict = new Dictionary<string, Dictionary<string,Planet>>();

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
				Dictionary<string, Planet> tmp = new Dictionary<string,Planet> ();
				tmp.Add (items [2], new Planet(items));
				if (dict.ContainsKey (items [1])) {
					dict [items [1]].Add (items[2],new Planet(items));
				} else {
					dict.Add (items [1], tmp);
				}
			}
			i++;
		}
	}
}

