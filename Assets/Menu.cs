using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
	GameObject myObject;
	Slider orbitSlider;
	Slider speedSlider;
	float orbitVal;
	float orbitPrevVal;
	float speedVal;
	float speedPrevVal;
	PlanetParser p;
	bool enabled = false;
	// Use this for initialization
	void Start ()
	{
		orbitSlider = GameObject.Find ("ScaleSlider").GetComponent<Slider> ();
		this.orbitVal = orbitSlider.value;
		this.orbitPrevVal = this.orbitVal;

		speedSlider = GameObject.Find ("SpeedSlider").GetComponent<Slider> ();
		this.speedVal = speedSlider.value;
		this.speedPrevVal = this.speedVal;

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

		if (this.orbitVal != this.orbitPrevVal) {
			this.p.setOrbitalScaleAll (this.orbitVal);
		}

		if (this.speedVal != this.speedPrevVal) {
			this.p.setSpeedValueAll (this.speedVal);
		}
		
		foreach (Transform child in this.transform)
		{
			if (Input.GetKey(KeyCode.Q))
			{
				child.gameObject.SetActive(false);

			}
			else if (Input.GetKey(KeyCode.E))
			{
				child.gameObject.SetActive(true);
			}
		}
	}
}