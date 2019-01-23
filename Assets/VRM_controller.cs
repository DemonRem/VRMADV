using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRM;


public class VRM_controller : MonoBehaviour
{
    public GameObject obj;
    IEnumerator LoadVrmCoroutine(string path, Action<GameObject> onLoaded)
    {
        var www = new WWW(path);
        yield return www;
        VRMImporter.LoadVrmAsync(www.bytes, onLoaded);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var path = Application.streamingAssetsPath + "/" + "model.vrm";

        StartCoroutine(LoadVrmCoroutine(path, go =>
        {
            go.transform.position = new Vector3(0 ,0 ,0);
            GameObject VRM = GameObject.Find("VRM");
            obj.GetComponent<Camera_controller>().target = VRM;
        }));
    }
   
}