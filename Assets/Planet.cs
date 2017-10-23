using System;
using UnityEngine;

public class Planet
{
	public GameObject root, planet, orbit;
	public GameObject sideRoot, sidePlanetText;

	private float rotateScale = 0.2F;
	private float distanceScale = 2.0F;
	private float sizeScale = 1.0F;
	public float radiusOfPlanet; 
	public float radiusOfOrbit;
	public string planetLetter;
	public float mass; 
	public string name;
	public Star star;
	public float timeToOrbit; 
	public float volume;
	public bool outOfBounds;

	public string texture;

	public bool errorMassRadius;  

	private const float AU_TO_KM = 149597870.7F;
	private const float JUPITER_RADIUS_TO_KM = 69911F;
	private const float YEAR_TO_DAYS = 365.2422F; 
	private const float ORBIT_WIDTH = 0.01F;
	private const float AVG_DENSITY = 1.33F;
	private const float PANEL_Z = 0F;
	private const float PANEL_WIDTH = 40.0F;
	private const float PANEL_HEIGHT = 0.1F;
	private const float PANEL_DEPTH = 0.1F;
	private const float BASE_PLANET_SIZE = 10000F;

	public Planet (string [] data, Star star, Transform parent, Transform flatParent)
	{
		this.radiusOfOrbit = string.IsNullOrEmpty(data[9]) ? 0.0F : float.Parse (data [9]);
		this.mass = string.IsNullOrEmpty(data[21]) ? 1.0F : float.Parse(data [21]);
		this.volume = this.mass/AVG_DENSITY;
		this.radiusOfPlanet = string.IsNullOrEmpty(data[26]) ?  (float)Math.Pow((this.volume*3)/(4*3.14), (1/3)) * JUPITER_RADIUS_TO_KM: float.Parse (data [26]) * JUPITER_RADIUS_TO_KM;
		this.name = data [70];
		this.planetLetter = data [2];
		this.star = star;
		this.timeToOrbit = string.IsNullOrEmpty(data[5]) ? 0.0F : float.Parse(data[5])/YEAR_TO_DAYS;
		this.errorMassRadius = setMassRadius();
		this.outOfBounds = false;

		setTexture ();
		setupGameStuff(parent);
		setup2dStuff(flatParent);
		updateSize();
		updateRadius();
	}

	public Planet(float radiusOfOrbit, float radiusOfPlanet, string planetLetter, string texture, float timeToOrbit, Star star, Transform parent, Transform flatParent) {
		this.radiusOfOrbit = radiusOfOrbit / AU_TO_KM;
		this.radiusOfPlanet = radiusOfPlanet;
		this.planetLetter = planetLetter;
		this.timeToOrbit = timeToOrbit;
		this.star = star;
		this.texture = texture;
		setupGameStuff(parent);
		setup2dStuff(flatParent);
		updateSize();
		updateRadius();
	}

	private void setupGameStuff(Transform parent) {
		root = new GameObject(this.planetLetter + "Center");
		root.AddComponent<rotate>();
		updateRevoSpeed();

		planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		planet.name = this.planetLetter;
		planet.transform.position = new Vector3(0, 0, 0);
		planet.transform.parent = root.transform;

		Material planetMaterial = new Material(Shader.Find("Standard"));
		planet.GetComponent<MeshRenderer>().material = planetMaterial;
		planetMaterial.mainTexture = Resources.Load(this.texture) as Texture;

		orbit = new GameObject(this.planetLetter + " orbit");
		orbit.transform.parent = root.transform;

		root.transform.parent = parent;
	}

	private void setup2dStuff(Transform flatParent) {
		sideRoot = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sideRoot.name = this.planetLetter;
		sideRoot.transform.position = new Vector3 (0, 0, 0);

		var planetMaterial = new Material (Shader.Find ("Standard"));
		sideRoot.GetComponent<MeshRenderer>().material = planetMaterial;
		planetMaterial.mainTexture = Resources.Load (texture) as Texture;

		sidePlanetText = new GameObject("Planet Name");
		sidePlanetText.transform.position = new Vector3(0, 0, 0);
		sidePlanetText.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
		var planetTextMesh = sidePlanetText.AddComponent<TextMesh>();
		planetTextMesh.text = this.planetLetter;
		planetTextMesh.fontSize = 25;
		sidePlanetText.transform.parent = sideRoot.transform;

		sideRoot.transform.parent = flatParent.transform;
	}

	public void setSizeScale(float newScale) {
		this.sizeScale = newScale;
		updateSize();
	}

	private void updateSize() {
		float size = this.radiusOfPlanet / BASE_PLANET_SIZE * this.sizeScale;
		this.sideRoot.transform.localScale = new Vector3(size, size, 5.0F * PANEL_DEPTH);
		this.planet.transform.localScale = new Vector3(size, size, size);
	}

	public void setOrbitScale(float newScale) {
		this.distanceScale = newScale;
		UnityEngine.Object.DestroyImmediate(orbit.GetComponent<LineRenderer>());			
		UnityEngine.Object.DestroyImmediate(orbit.GetComponent<Circle>());
		updateRadius();
	}

	public void setRevoScale(float newScale) {
		this.rotateScale = newScale;
		updateRevoSpeed();
	}

	private void updateRevoSpeed() {
		float scaledSpeed = -1.0F / this.timeToOrbit * this.rotateScale;
		root.GetComponent<rotate>().rotateSpeed = scaledSpeed;
	}

	private void updateRadius() {
		float scaledRadius = this.radiusOfOrbit * this.distanceScale * 10.0F;

		Vector3 position = planet.transform.localPosition;
 		position[2] = scaledRadius; // the Z value
 		planet.transform.localPosition = position;
		
		orbit.AddComponent<Circle>();
		orbit.AddComponent<LineRenderer>();
		orbit.GetComponent<Circle>().xradius = scaledRadius;
		orbit.GetComponent<Circle>().yradius = scaledRadius;

		var line = orbit.GetComponent<LineRenderer>();
		line.startWidth = ORBIT_WIDTH;
		line.endWidth = ORBIT_WIDTH;
		line.useWorldSpace = false;

		orbit.GetComponent<LineRenderer>().material.color = Color.white;

		float newPos = -0.5F * PANEL_WIDTH + scaledRadius;
		if(newPos < (PANEL_WIDTH/2)) {
			sideRoot.SetActive(true);
			sideRoot.transform.localPosition = new Vector3 (newPos, 0, PANEL_Z);
			this.outOfBounds= false;
		} else {
			sideRoot.SetActive(false);
			this.outOfBounds = true;
		}
	}

	private bool setMassRadius() {
		float massDouble = this.mass;
		float radius = this.radiusOfOrbit;
		float radiusPlanet = this.radiusOfPlanet;
		if ((massDouble <= 0.0F) && (radiusPlanet <= 0.0F)) {
			return true;
		} else {
			if (massDouble <= 0.0F) {
				massDouble = (float)(0.00672F * Math.Exp(0.0000706F * (radiusPlanet)));
			} else if (radius <= 0.0F) {
				radius = (float)(72483+(15496 * Math.Log(massDouble)));
			}
		}
		return false;
	}

	private void setTexture() {
		// Set the texture based on the radius/mass
		float massDouble = this.mass;

		if (massDouble <= 0) {
			if (this.radiusOfPlanet < 10000)
				this.texture = "uranus";
			else if (this.radiusOfPlanet < 20000)
				this.texture = "neptune";
			else if (this.radiusOfPlanet < 30000)
				this.texture = "saturn";
			else
				texture = "jupiter";
		} 
		else {
			if (massDouble < 0.05F) {
				texture = "uranus";
			} else if (massDouble < 0.1F) {
				texture = "neptune";
			} else if (massDouble < 0.65F) {
				texture = "saturn";
			} else {
				texture = "jupiter";
			}
		}
	}
}

