using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
	GameObject myObject;


	Button speedUpBtn;
	Button speedDownBtn;


	SteamVR_Controller.Device Controller;

	float orbitVal = 2.0F;
	float orbitPrevVal;
	float speedVal = 0.2F;
	float speedPrevVal;
	float planetSizeVal = 1.0F;
	float planetSizePrevVal;

	PlanetParser p;

	// Use this for initialization
	void Start ()
	{
		
	}
		

	// Update is called once per frame
	void Update ()
	{
		

	}

	public void scaleSystemUp(){
		this.orbitVal += 0.5F;
		p.setOrbitalScaleAll (this.orbitVal);
	}

	public void scaleSystemDown(){
		this.orbitVal -= 0.5F;
		if (this.orbitVal < 0.1F) {
			this.orbitVal = 0.1F;
		}
		p.setOrbitalScaleAll (this.orbitVal);
	}

	public void speedUp(){
		this.speedVal += 0.2F;
		p.setSpeedValueAll (this.speedVal);
	}
	public void speedDown(){
		this.speedVal -= 0.2F;
		if (this.speedVal < 0.1F) {
			this.speedVal = 0.1F;
		}
		p.setSpeedValueAll (this.speedVal);
	}

	public void planetUp(){
		this.planetSizeVal += 1.0F;
		p.setSizeScaleAll (this.planetSizeVal);
	}

	public void planetDown(){
		this.planetSizeVal -= 1.0F;
		if (this.planetSizeVal < 1.0F) {
			this.planetSizeVal = 1.0F;
		}
		p.setSizeScaleAll (this.planetSizeVal);
	}







	void Activate(){
		Debug.Log ("Hit Activate");


	}
}