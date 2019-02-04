using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;

    // Update is called once per frame
    void Update()
    {
        Vector2 pt = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        if(pt.x > -0.5 && -0.5 < pt.y && pt.y < 0.5)
        {
            camera.transform.position += new Vector3(-0.1f * Time.deltaTime, 0, 0);
        }

        if(pt.x > 0.5 && -0.5 < pt.y && pt.y < 0.5)
        {
            camera.transform.position +=  new Vector3(0.1f * Time.deltaTime, 0, 0);
        }

        if(pt.y < -0.5 && -0.5 < pt.x && pt.x < 0.5)
        {
            camera.transform.position += new Vector3(0, 0, -0.1f * Time.deltaTime);
        }

        if(pt.y > 0.5 && -0.5 < pt.x && pt.x < 0.5)
        {
            camera.transform.position += new Vector3(0, 0, 0.1f * Time.deltaTime);
        }
    }
}
