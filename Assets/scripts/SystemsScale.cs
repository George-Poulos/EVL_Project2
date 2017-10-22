using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemsScale : MonoBehaviour
{

	public static float scale = 1f;
    public static bool isScaling = false;
    public float originalSize = 1f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isScaling)
        {

            ///transform.GetComponent<Circle>().xradius = originalSize * scaleVariable;
           // transform.GetComponent<Circle>().yradius = originalSize * scaleVariable;
            //Debug.Log("Scaliiing");

        }
    }
}
