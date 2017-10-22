using System;

public class Star
{
	public string brightness;
	public string name;
	public string distanceAwayFromUs;
	public string type;
	public string radius;
	public string numberOfPlanets;
	public string texture;
	public char spectralType;

	private const double STAR_RADIUS_CONVERT = 695700 ;

	public Star (string[] starData)
	{
		this.brightness = string.IsNullOrEmpty(starData[225]) ? "0" : starData [225];
		this.name = starData[1];
		this.distanceAwayFromUs = string.IsNullOrEmpty(starData[42]) ? "0" : starData [42];
		this.type = starData[216];
		this.radius = string.IsNullOrEmpty(starData[64]) ? "0" : (Double.Parse(starData[64]) * STAR_RADIUS_CONVERT).ToString();
		numberOfPlanets = string.IsNullOrEmpty(starData[4]) ? "0" : starData[4];

		setTexture();
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
				break;
			}

		} else {
		}
	}
}

