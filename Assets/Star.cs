using System;
using UnityEngine;

public class Star 
{
	public GameObject root, sun, upperSun, sunText, sunSupport, innerHab, outerHab;
	public GameObject sideRoot, sideSun, sideSunText, habZone, offScreenArrow;
	public float radiusScale = 2.0F;

	public float brightness;
	public float mass;
	public string name;
	public string distanceAwayFromUs;
	public string type;
	public string discovered;
	public float radius;
	public float scaledRadius;
	public int numberOfPlanets;
	public string texture;
	public char spectralType;

	private const float STAR_RADIUS_CONVERT = 695700F;
	private const float SUN_SCALE_CONSTANT = 100000F;
	private const float HAB_WIDTH = 0.03F;
	private const float PANEL_Z = 0F;
	private const float PANEL_WIDTH = 40.0F;
	private const float PANEL_HEIGHT = 0.1F;
	private const float PANEL_DEPTH = 0.1F;

	/**
	 * Constructor for screating a Star 
	 */
	public Star (string[] starData, Transform parent, Transform flatParent)
	{
		this.name = starData[1];
		this.numberOfPlanets = string.IsNullOrEmpty(starData[4]) ? 0 : Int32.Parse(starData[4]);
		this.distanceAwayFromUs = string.IsNullOrEmpty(starData[42]) ? "Unknown" : starData [42];
		this.mass = string.IsNullOrEmpty(starData[59]) ? 1.0F : float.Parse(starData[59]);
		this.radius = string.IsNullOrEmpty(starData[64]) ? this.mass * STAR_RADIUS_CONVERT : (float.Parse(starData[64]) * STAR_RADIUS_CONVERT);
		this.type = string.IsNullOrEmpty(starData[216]) ? "gstar" : starData [216];
		this.brightness = string.IsNullOrEmpty(starData[225]) ? 1.2F : (float)Math.Pow(10, float.Parse(starData[225]));
		this.scaledRadius = this.radius / SUN_SCALE_CONSTANT;
		this.discovered = starData[3];

		setTexture();
		setupGameStuff(parent);
		setup2dStuff(flatParent);
	}

	public Star(string name, string discovered, float brightness, string texture, string type, float radius, int numberOfPlanets, string distanceAwayFromUs, Transform parent, Transform flatParent) {
		this.name = name;
		this.discovered = discovered;
		this.brightness = brightness;
		this.type = type;
		this.radius = radius;
		this.numberOfPlanets = numberOfPlanets;
		this.scaledRadius = this.radius / SUN_SCALE_CONSTANT;
		this.distanceAwayFromUs = distanceAwayFromUs;
		this.texture = texture;

		setupGameStuff(parent);
		setup2dStuff(flatParent);
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

		innerHab = new GameObject(this.name + " innerHab");
		outerHab = new GameObject(this.name + " outerHab");
		initHab(innerHab, true);
		initHab(outerHab, false);
		innerHab.transform.parent = root.transform;
		outerHab.transform.parent = root.transform;
	}

	private void initHab(GameObject hab, bool inner) {
		float rad = inner ? this.brightness * 9.5F : this.brightness * 14F;
		rad *= this.radiusScale;

		hab.AddComponent<Circle>();
		hab.AddComponent<LineRenderer>();
		hab.GetComponent<Circle>().xradius = rad;
		hab.GetComponent<Circle>().yradius = rad;

		var line = hab.GetComponent<LineRenderer>();
		line.startWidth = HAB_WIDTH;
		line.endWidth = HAB_WIDTH;
		line.useWorldSpace = false;

		hab.GetComponent<LineRenderer>().material.color = Color.green;
	}

	private void setup2dStuff(Transform parentSide) {
		sideRoot = GameObject.CreatePrimitive(PrimitiveType.Cube);
		sideRoot.name = "Side " + this.name + " Panel";
		sideRoot.transform.position = new Vector3(0, 0, PANEL_Z);
		sideRoot.transform.localScale = new Vector3(PANEL_WIDTH, PANEL_HEIGHT, PANEL_DEPTH);
		sideRoot.transform.parent = parentSide.transform;

		sideSun = GameObject.CreatePrimitive(PrimitiveType.Cube);
		sideSun.name = "Side " + this.name + " Star";
		sideSun.transform.position = new Vector3(-0.5F * PANEL_WIDTH - 0.5F, 0, PANEL_Z);
		sideSun.transform.localScale = new Vector3(1.0F, PANEL_HEIGHT*40.0F, 2.0F * PANEL_DEPTH);
		sideSun.transform.parent = parentSide.transform;

		var material = new Material(Shader.Find("Unlit/Texture"));
		sideSun.GetComponent<MeshRenderer>().material = material;
		material.mainTexture = Resources.Load(texture) as Texture;

		sideSunText = new GameObject("Side Star Name");
		sideSunText.transform.position = new Vector3(-0.47F * PANEL_WIDTH, 22.0F * PANEL_HEIGHT, PANEL_Z);
		sideSunText.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
		var sunTextMesh = sideSunText.AddComponent<TextMesh>();
		sunTextMesh.text = this.name + ": " + this.type + " - " + this.distanceAwayFromUs + "ly away (via " + this.discovered + ")";
		sunTextMesh.fontSize = 150;
		sideSunText.transform.parent = parentSide.transform;

		habZone = GameObject.CreatePrimitive(PrimitiveType.Cube);
		habZone.name = "Hab";
		habZone.transform.position = new Vector3(0, 0, 0);
		habZone.transform.parent = parentSide.transform;
		init2dHab();

		offScreenArrow = new GameObject();
		offScreenArrow.name = "size indicator";
		offScreenArrow.transform.position = new Vector3(0.5F * PANEL_WIDTH + 0.5F, 32.5F * PANEL_HEIGHT, PANEL_Z);
		offScreenArrow.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
		var arrowMesh = offScreenArrow.AddComponent<TextMesh>();
		arrowMesh.text = "→";
		arrowMesh.fontSize = 500;
		arrowMesh.color = Color.yellow;
		offScreenArrow.transform.parent = parentSide.transform;		
		setIndicator(false);

		var habMaterial = new Material(Shader.Find("Standard"));
		habZone.GetComponent<MeshRenderer>().material = habMaterial;
		habMaterial.mainTexture = Resources.Load("habitable") as Texture;
	}

	public void setOrbitScale(float newScale) {
		this.radiusScale = newScale;
		UnityEngine.Object.DestroyImmediate(innerHab.GetComponent<LineRenderer>());			
		UnityEngine.Object.DestroyImmediate(innerHab.GetComponent<Circle>());
		UnityEngine.Object.DestroyImmediate(outerHab.GetComponent<LineRenderer>());			
		UnityEngine.Object.DestroyImmediate(outerHab.GetComponent<Circle>());
		initHab(innerHab, true);
		initHab(outerHab, false);
		init2dHab();
	}

	private void init2dHab() {
		float beginEdge = Mathf.Abs(this.brightness * 9.5F);
		float endEdge = Mathf.Abs(this.brightness * 14F);
		Vector3 oldPosition = habZone.transform.position;
		oldPosition[0] = (-0.5F * PANEL_WIDTH) + ((beginEdge+endEdge) * 0.5F * this.radiusScale);
		habZone.transform.position = oldPosition;
		habZone.transform.localScale = new Vector3 ((endEdge - beginEdge)* this.radiusScale, 40.0F * PANEL_HEIGHT, 2.0F * PANEL_DEPTH);
	}

	public Vector3 get2dSize() {
		return new Vector3(30, 4, 0.1F);
	}

	public void setIndicator(bool active) {
		offScreenArrow.SetActive(active);
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

