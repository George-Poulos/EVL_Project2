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
            transform.localScale = new Vector3(originalSize * scale, transform.localScale.y, originalSize * scale);
<<<<<<< HEAD
            ///transform.GetComponent<Circle>().xradius = originalSize * scaleVariable;
           // transform.GetComponent<Circle>().yradius = originalSize * scaleVariable;
            //Debug.Log("Scaliiing");
=======
>>>>>>> 03f51dfc45577a0d65ece78e2ad9b4c915992484
        }
    }
}
