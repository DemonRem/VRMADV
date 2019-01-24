using System.Collections;
using UnityEngine;

public class Button_controller : MonoBehaviour
{
    public GameObject obj;
    public void OnClick(){
        obj.GetComponent<Camera_controller>().distance = 2.0f;
        obj.GetComponent<Camera_controller>().polarAngle = 90.0f;
        obj.GetComponent<Camera_controller>().azimuthalAngle = 90.0f;
    }
}