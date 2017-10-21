using System;
using System.Collections.Generic;

public class PlanetParser
{
	public Dictionary<string, Dictionary<string, string[]>> dict = new Dictionary<string, Dictionary<string,string[]>>();

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
				Dictionary<string, string[]> tmp = new Dictionary<string,string[]> ();
				tmp.Add (items [2], items);
				if (dict.ContainsKey (items [1])) {
					dict [items [1]].Add (items[2],items);
				} else {
					
					dict.Add (items [1], tmp);
				}
			}
			i++;
		}
	}
}

