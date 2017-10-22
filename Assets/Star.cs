using System;
using UnityEngine;

public class Star 
{
	public GameObject root, sun, upperSun, sunText, sunSupport, innerHab, outerHab;
	public float radiusScale = 2.0F;

	public float brightness;
	public float mass;
	public string name;
	public string distanceAwayFromUs;
	public string type;
	public float radius;
	public float scaledRadius;
	public int numberOfPlanets;
	public string texture;
	public char spectralType;

	private const float STAR_RADIUS_CONVERT = 695700F;
	private const float SUN_SCALE_CONSTANT = 100000F;
	private const float HAB_WIDTH = 0.03F;
	/**
	 * Constructor for screating a Star 
	 */
	public Star (string[] starData, Transform parent)
	{
		this.name = starData[1];
		this.numberOfPlanets = string.IsNullOrEmpty(starData[4]) ? 0 : Int32.Parse(starData[4]);
		this.distanceAwayFromUs = string.IsNullOrEmpty(starData[42]) ? "0" : starData [42];
		this.mass = string.IsNullOrEmpty(starData[59]) ? 1.0F : float.Parse(starData[59]);
		this.radius = string.IsNullOrEmpty(starData[64]) ? this.mass * STAR_RADIUS_CONVERT : (float.Parse(starData[64]) * STAR_RADIUS_CONVERT);
		this.type = string.IsNullOrEmpty(starData[216]) ? "gstar" : starData [216];
		this.brightness = string.IsNullOrEmpty(starData[225]) ? 1.2F : float.Parse(starData[225]);
		this.scaledRadius = this.radius / SUN_SCALE_CONSTANT;

		setTexture();
		setupGameStuff(parent);
	}

	private void setupGameStuff(Transform parent) {
		float sunSize = 0.25F;
		root = new GameObject(this.name);
		root.transform.parent = parent;

		sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sun.AddComponent<rotate>();
		sun.name = this.name;
		sun.transform.position = new Vector3(0, 0, 0);
		sun.transform.localScale = new Vector3(sunSize, sunSize, sunSize);
		sun.transform.parent = root.transform;
		sun.GetComponent<rotate>().rotateSpeed = -0.25F;

		Material sunMaterial = new Material(Shader.Find("Unlit/Texture"));
		sun.GetComponent<MeshRenderer>().material = sunMaterial;
		sunMaterial.mainTexture = Resources.Load(texture) as Texture;

		upperSun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		upperSun.AddComponent<rotate>();
		upperSun.GetComponent<rotate>().rotateSpeed = -0.25F;
		upperSun.GetComponent<MeshRenderer>().material = sunMaterial;
		upperSun.name = this.name + " upper";
		upperSun.transform.localScale = new Vector3(scaledRadius, scaledRadius, scaledRadius);
		upperSun.transform.position = new Vector3(0, 10, 0);
		upperSun.transform.parent = root.transform;

		sunSupport = GameObject.CreatePrimitive(PrimitiveType.Cube);
		sunSupport.transform.localScale = new Vector3 (0.1F, 10.0F, 0.1F);
		sunSupport.transform.position = new Vector3 (0, 5, 0);
		sunSupport.name = "Sun Support";
		sunSupport.transform.parent = root.transform;

		sunText = new GameObject();
		sunText.name = "Star Name";
		sunText.transform.position = new Vector3 (0, 5, 0);
		sunText.transform.localScale = new Vector3 (0.1F, 0.1F, 0.1F);
		var sunTextMesh = sunText.AddComponent<TextMesh>();
		sunTextMesh.text = this.name;
		sunTextMesh.fontSize = 200;
		sunText.transform.parent = root.transform;

		float innerRad = this.brightness * 9.5F;
		float outerRad = this.brightness * 14F;
		innerHab = new GameObject(this.name + " innerHab");
		outerHab = new GameObject(this.name + " outerHab");
		initHab(innerHab, innerRad * radiusScale, Color.green, root.transform);
		initHab(outerHab, outerRad * radiusScale, Color.green, root.transform);
	}

	private void initHab(GameObject hab, float radius, Color orbitColor, Transform parent) {
		hab.AddComponent<Circle>();
		hab.AddComponent<LineRenderer>();
		hab.GetComponent<Circle>().xradius = radius;
		hab.GetComponent<Circle>().yradius = radius;

		var line = hab.GetComponent<LineRenderer>();
		line.startWidth = HAB_WIDTH;
		line.endWidth = HAB_WIDTH;
		line.useWorldSpace = false;

		hab.GetComponent<LineRenderer>().material.color = orbitColor;
		hab.transform.parent = parent;
	}

	/**
	 * Set Texture based on spectralType of the Star
	 */
	private void setTexture() {
		if (!string.IsNullOrEmpty(type)) {
			spectralType = type[0];
			spectralType = Char.ToLower(spectralType);

			switch (spectralType)
			{
			case 'a':
				texture = "astar";
				break;
			case 'b':
				texture = "bstar";
				break;
			case 'f':
				texture = "fstar";
				break;
			case 'g':
				texture = "gstar";
				break;
			case 'k':
				texture = "kstar";
				break;
			case 'm':
				texture = "mstar";
				break;
			case 'l':
				texture = "mstar";
				break;
			case 't':
				texture = "mstar";
				break;
			case 'o':
				texture = "ostar";
				break;
			case 'w':
				texture = "bstar";
				break;
			case 's':
				texture = "bstar";
				break;
			default:
				//Debug.LogError("Invalid Spectral Type: " + spectralType);
				texture = "bstar";
				break;
			}

		} else {
		}
	}
}

