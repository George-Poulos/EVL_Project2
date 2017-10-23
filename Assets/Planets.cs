/// Sample Code for CS 491 Virtual And Augmented Reality Course - Fall 2017
/// written by Andy Johnson
/// 
/// makes use of various textures from the celestia motherlode - http://www.celestiamotherlode.net/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planets : MonoBehaviour {

	float panelHeight = 0.1F;
	float panelWidth = 30.0F;
	float panelDepth = 0.1F;

	float panelZ = 20;

	float orbitWidth = 0.01F;
	float habWidth = 0.03F;

	float revolutionSpeed = 0.2F;

	float panelXScale = 2.0F;
	float orbitXScale = 2.0F;

	public PlanetParser p;
	private bool x = false;
	//------------------------------------------------------------------------------------//

	void sideDealWithPlanets (string [,] planets, GameObject thisSide, GameObject theseOrbits){
		GameObject newPlanet;

		GameObject sunRelated;

		Material planetMaterial;

		int planetCounter;

		for (planetCounter = 0; planetCounter < planets.GetLength(0); planetCounter++) {

			float planetDistance = float.Parse (planets [planetCounter, 0]) / 149600000.0F * 10.0F;
			float planetSize = float.Parse (planets [planetCounter, 1]) * 1.0F / 10000.0F;
			string textureName = planets [planetCounter, 3];
			string planetName = planets [planetCounter, 4];

			// limit the planets to the width of the side view
			if ((panelXScale * planetDistance) < panelWidth) {

				newPlanet = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				newPlanet.name = planetName;
				newPlanet.transform.position = new Vector3 (-0.5F * panelWidth + planetDistance * panelXScale, 0, panelZ);
				newPlanet.transform.localScale = new Vector3 (planetSize, planetSize, 5.0F * panelDepth);

				planetMaterial = new Material (Shader.Find ("Standard"));
				newPlanet.GetComponent<MeshRenderer> ().material = planetMaterial;
				planetMaterial.mainTexture = Resources.Load (textureName) as Texture;

				sunRelated = thisSide;
				newPlanet.transform.parent = sunRelated.transform;
			}
		}
	}

	//------------------------------------------------------------------------------------//

	void sideDealWithStar (string [] star, GameObject thisSide, GameObject theseOrbits){
		GameObject newSidePanel;
		GameObject newSideSun;
		GameObject sideSunText;

		GameObject habZone;

		Material sideSunMaterial, habMaterial;

		newSidePanel = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newSidePanel.name = "Side " + star[1] + " Panel";
		newSidePanel.transform.position = new Vector3 (0, 0, panelZ);
		newSidePanel.transform.localScale = new Vector3 (panelWidth, panelHeight, panelDepth);
		newSidePanel.transform.parent = thisSide.transform;

		newSideSun = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newSideSun.name = "Side " + star[1] + " Star";
		newSideSun.transform.position = new Vector3 (-0.5F * panelWidth - 0.5F, 0, panelZ);
		newSideSun.transform.localScale = new Vector3 (1.0F, panelHeight*40.0F, 2.0F * panelDepth);
		newSideSun.transform.parent = thisSide.transform;

		sideSunMaterial = new Material (Shader.Find ("Unlit/Texture"));
		newSideSun.GetComponent<MeshRenderer> ().material = sideSunMaterial;
		sideSunMaterial.mainTexture = Resources.Load (star[2]) as Texture;


		sideSunText = new GameObject();
		sideSunText.name = "Side Star Name";
		sideSunText.transform.position = new Vector3 (-0.47F * panelWidth, 22.0F * panelHeight, panelZ);
		sideSunText.transform.localScale = new Vector3 (0.1F, 0.1F, 0.1F);
		var sunTextMesh = sideSunText.AddComponent<TextMesh>();
		sunTextMesh.text = star[1];
		sunTextMesh.fontSize = 150;
		sideSunText.transform.parent = thisSide.transform;

		float innerHab = Mathf.Abs(float.Parse (star[4]) * 9.5F);
		float outerHab = Mathf.Abs(float.Parse (star[4]) * 14F);


		// need to take panelXScale into account for the hab zone

		habZone = GameObject.CreatePrimitive (PrimitiveType.Cube);
		habZone.name = "Hab";
		habZone.transform.position = new Vector3 ((-0.5F * panelWidth) + ((innerHab+outerHab) * 0.5F * panelXScale), 0, panelZ);
		habZone.transform.localScale = new Vector3 ((outerHab - innerHab)* panelXScale, 40.0F * panelHeight, 2.0F * panelDepth);
		habZone.transform.parent = thisSide.transform;

		habMaterial = new Material (Shader.Find ("Standard"));
		habZone.GetComponent<MeshRenderer> ().material = habMaterial;
		habMaterial.mainTexture = Resources.Load ("habitable") as Texture;

	}

	//------------------------------------------------------------------------------------//

	void dealWithSystem(string[] starInfo, string[,] planetInfo, Vector3 offset, GameObject allThings){

		GameObject SolarCenter;
		GameObject AllOrbits;
		GameObject SunStuff;
		GameObject Planets;

		SolarCenter = new GameObject();
		AllOrbits = new GameObject();
		SunStuff = new GameObject();
		Planets = new GameObject();

		// add in second 'flat' representation
		GameObject SolarSide;
		SolarSide = new GameObject();
		SolarSide.name = "Side View of" + starInfo[1];


		sideDealWithStar (starInfo, SolarSide, AllOrbits);
		sideDealWithPlanets (planetInfo, SolarSide, AllOrbits);

		SolarSide.transform.position = new Vector3 (0, 8, 10.0F);
		SolarSide.transform.position += (offset * 0.15F);

	}

	//------------------------------------------------------------------------------------//

	void Start () {
		p = new PlanetParser ("./Assets/Resources/planets.csv");
		foreach(var system in p.dict.Values) {
			system.solarSystem.SetActive(false);
		}
		p.dict["Our Sun"].solarSystem.SetActive(true);
		p.dict["KOI-351"].solarSystem.SetActive(true);
	}

	// Update is called once per frame
	void Update () {

	}
}
