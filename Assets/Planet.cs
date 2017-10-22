using System;

public class Planet
{
	public string radiusOfPlanet; 
	public string radiusOfOrbit;
	public string planetLetter;
	public string mass; 
	public string name;
	public string discovered;
	public Star star;
	public string timeToOrbit; 

	public string texture;

	public bool errorMassRadius;  

	private const double AU_TO_KM = 149597870.7;
	private const double JUPITER_RADIUS_TO_KM = 69911;
	private const double YEAR_TO_DAYS = 365.2422; 


	public Planet (string [] data)
	{
		this.radiusOfOrbit = string.IsNullOrEmpty(data[9]) ? "0.0" : (Double.Parse (data [9]) * AU_TO_KM).ToString();
		this.radiusOfPlanet = string.IsNullOrEmpty(data[26]) ? "0.0" : (Double.Parse (data [26]) * JUPITER_RADIUS_TO_KM).ToString();
		this.mass = string.IsNullOrEmpty(data[21]) ? "0" : data [21];
		this.name = data [70];
		this.discovered = data [3];
		this.planetLetter = data [2];
		this.star = new Star (data);
		this.timeToOrbit = string.IsNullOrEmpty(data[5]) ? "0" : (Double.Parse(data[5])/YEAR_TO_DAYS).ToString();
		this.errorMassRadius = setMassRadius();
		setTexture ();
	}

	private bool setMassRadius() {
		double massDouble = string.IsNullOrEmpty(this.mass) ? 0 : Double.Parse (this.mass);
		double radius = string.IsNullOrEmpty(this.radiusOfOrbit) ? 0 : Double.Parse (this.radiusOfOrbit);
		double radiusPlanet = string.IsNullOrEmpty(this.radiusOfPlanet) ? 0 : Double.Parse (this.radiusOfPlanet);
		if ((massDouble <= 0) && (radiusPlanet <= 0)) {
			return true;
		} else {
			if (massDouble <= 0) {
				massDouble = 0.00672 * Math.Exp(0.0000706 * (radiusPlanet));
			} else if (radius <= 0) {
				radius = 72483+(15496 * Math.Log(massDouble));
			}
		}
		return false;
	}

	private void setTexture() {
		// Set the texture based on the radius/mass
		double massDouble = string.IsNullOrEmpty(this.mass) ? 0 : Double.Parse (this.mass);

		if (massDouble <= 0) {
			texture = "";	
		} 
		else {
			if (massDouble < 0.05) {
				texture = "uranus";
			} else if (massDouble < 0.1) {
				texture = "neptune";
			} else if (massDouble < 0.65) {
				texture = "saturn";
			} else {
				texture = "jupiter";
			}
		}
	}
}

