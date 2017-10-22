using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour {
    public void ChangeScale(int type)
    {
        GameObject threeDView = GameObject.Find("3DView");
        if (type == 0)
        {
            threeDView.transform.localScale = new Vector3(threeDView.transform.localScale.x-0.1F, threeDView.transform.localScale.y - 0.1F, threeDView.transform.localScale.z - 0.1F);
        }
        else
        {
            threeDView.transform.localScale = new Vector3(threeDView.transform.localScale.x + 0.1F, threeDView.transform.localScale.y + 0.1F, threeDView.transform.localScale.z + 0.1F);
        }
    }

    public void ScalePlanets(float scale)
    {
        PlanetsScale.isScaling = true;
        PlanetsScale.scale = scale;
    }

    public void ScaleSystems(float scale)
    {
        SystemsScale.isScaling = true;
        SystemsScale.scale = scale;
        PlanetPosition.isScaling = true;
        PlanetPosition.position = scale;
    }
}
