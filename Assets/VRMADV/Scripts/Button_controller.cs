using System.Collections;
using UnityEngine;

public class Button_controller : MonoBehaviour
{
    public GameObject obj;
    public GameObject VRM;
    public void OnClick(){
        VRM.transform.position = new Vector3(0, -1, 0);
        VRM.transform.rotation = new Quaternion(0, 0, 0, 0);
        var camera = obj.GetComponent<Camera_controller>();
        camera.distance = 2.0f;
        camera.polarAngle = 90.0f;
        camera.azimuthalAngle = 90.0f;
        camera.offset = new Vector3(0, 0, 0);
    }
}