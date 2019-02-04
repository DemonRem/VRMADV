using System;
using UnityEngine;
using VRM;
using UnityEngine.Networking;
using System.Collections;

public class VRM_controller : MonoBehaviour
{
    public GameObject obj;
    
    private IEnumerator roadVRM(string path, Action<GameObject> onLoaded)
    {
        Debug.Log(path);
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.Send();
        if(!request.isNetworkError){
            VRMImporter.LoadVrmAsync(request.downloadHandler.data, onLoaded);
        }
    }

    void Start()
    {
        var path = Application.streamingAssetsPath + "/" + "model0.vrm";

        StartCoroutine(roadVRM(path, go =>
        {
            go.transform.position = new Vector3(0, 0, 0);
            obj.GetComponent<Camera_controller>().target = GameObject.Find("VRM");
        }));
    }
}