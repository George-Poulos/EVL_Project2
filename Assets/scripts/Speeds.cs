using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeds : MonoBehaviour {

    //public int type;

    public void ChangeSpeed(int type)
    {
        if (type == 0)
        {
            rotate.speed -= 1F;
        }
        else
        {
            rotate.speed += 1F;
        }
    }

    public void ChangeOrbitSpeed(float value)
    {
        rotate.speed = value;
    }
}
