using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
	GameObject myObject;
	Slider orbitSlider;
	Slider speedSlider;
	Slider planetSizeSlider;

	float orbitVal;
	float orbitPrevVal;
	float speedVal;
	float speedPrevVal;
	float planetSizeVal;
	float planetSizePrevVal;

	PlanetParser p;

	// Use this for initialization
	void Start ()
	{
		orbitSlider = GameObject.Find ("ScaleSlider").GetComponent<Slider> ();
		this.orbitVal = orbitSlider.value;
		this.orbitPrevVal = this.orbitVal;

		speedSlider = GameObject.Find ("SpeedSlider").GetComponent<Slider> ();
		this.speedVal = speedSlider.value;
		this.speedPrevVal = this.speedVal;

		planetSizeSlider = GameObject.Find ("PlanetSizeSlider").GetComponent<Slider> ();
		this.planetSizeVal = planetSizeSlider.value;
		this.planetSizePrevVal = this.planetSizeVal;


		myObject = GameObject.FindGameObjectWithTag ("GameController");
	 	this.p = myObject.GetComponent<Planets> ().p;

		foreach (Transform child in this.transform)
		{
			child.gameObject.SetActive(false);
		}
	}
		

	// Update is called once per frame
	void Update ()
	{
		this.orbitPrevVal = this.orbitVal;
		this.orbitVal = orbitSlider.value;

		this.speedPrevVal = this.speedVal;
		this.speedVal = speedSlider.value;

		this.planetSizePrevVal = this.planetSizeVal;
		this.planetSizeVal = this.planetSizeSlider.value;

		if (this.orbitVal != this.orbitPrevVal) {
			this.p.setOrbitalScaleAll (this.orbitVal);
		}

		if (this.planetSizeVal != this.planetSizePrevVal) {
			this.p.setSizeScaleAll (this.planetSizeVal);
		}

		if (this.speedVal != this.speedPrevVal) {
			this.p.setSpeedValueAll (this.speedVal);
		}

	}

	void Activate(){
		Debug.Log ("Hit Activate");
	}
}