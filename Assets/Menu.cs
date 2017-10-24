using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
	GameObject myObject;


	GameObject speedUpBtn;
	GameObject speedDownBtn;
	GameObject systemUpBtn;
	GameObject systemDownBtn;
	GameObject planetUpBtn;
	GameObject planetDownBtn;
	GameObject resetViewBtn;

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
		this.myObject = GameObject.FindGameObjectWithTag ("GameController");
		this.p = myObject.GetComponent<Planets> ().p;
		resetViewBtn = GameObject.FindGameObjectWithTag("Reset");
		speedUpBtn = GameObject.FindGameObjectWithTag("speedUp");
		speedDownBtn = GameObject.FindGameObjectWithTag("speedDown");
		systemUpBtn = GameObject.FindGameObjectWithTag("systemUp");
		systemDownBtn = GameObject.FindGameObjectWithTag("systemDown");
		planetUpBtn = GameObject.FindGameObjectWithTag("planetUp");
		planetDownBtn = GameObject.FindGameObjectWithTag("planetDown");
		var speedUpScript = speedUpBtn.GetComponent<SpeedUp>();
		var speedDownScript = speedDownBtn.GetComponent<SpeedDown>();
		var systemUpScript = systemUpBtn.GetComponent<SystemUp>();
		var systemDownScript = systemDownBtn.GetComponent<SystemDown>();
		var planetUpScript = planetUpBtn.GetComponent<PlanetUp>();
		var planetDownScript = planetDownBtn.GetComponent<PlanetDown>();
		var resetScript = resetViewBtn.GetComponent<ResetView>();
		speedUpScript.menu = this;
		speedDownScript.menu = this;
		systemUpScript.menu = this;
		systemDownScript.menu = this;
		planetUpScript.menu = this;
		planetDownScript.menu = this;
		resetScript.menu = this;
	}
		

	// Update is called once per frame
	void Update ()
	{
		

	}

	public void scaleSystemUp(){
		this.orbitVal += 1.0F;
		p.setOrbitalScaleAll (this.orbitVal);
	}

	public void scaleSystemDown(){
		this.orbitVal -= 1.0F;
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
		this.planetSizeVal += 0.1F;
		p.setSizeScaleAll (this.planetSizeVal);
	}

	public void planetDown(){
		this.planetSizeVal -= 0.1F;
		if (this.planetSizeVal < 0.1F) {
			this.planetSizeVal = 0.1F;
		}
		p.setSizeScaleAll (this.planetSizeVal);
	}

	public void resetView(){
		this.p.sideMenu.resetView ();
		p.setSizeScaleAll (1.0F);
		p.setOrbitalScaleAll (2.0F);
		p.setSpeedValueAll (0.2F);
	}

		
	void Activate(){
		Debug.Log ("Hit Activate");


	}
}