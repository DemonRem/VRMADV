using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
using VRM;

public class Canvas_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas_prefab;
    public GameObject VRMPathObject;

    void Start()
    {
        VRMPathObject = GameObject.Find("VRMPath");
    }

    public void OnClickDo()
    {
        StartCoroutine("SetMetaUI");
        VRMPathObject.GetComponent<DontDestroy>().Path = gameObject.GetComponent<VRMPath>().Path;
    }

    public void OnButtonYes()
    {
        SceneManager.LoadScene ("Viewer");
    }

    public void OnButtonNo()
    {
        Destroy(transform.root.gameObject);
    }

    IEnumerator SetMetaUI()
    {
        GameObject SelectUI =  Instantiate(canvas_prefab, new Vector3(0, 0, 0), Quaternion.identity);
        VRMMeta VRM = SelectUI.GetComponent<VRMMeta>();
        VRM.Meta = gameObject.GetComponent<VRMMeta>().Meta;
        yield return null;
        SelectUI.gameObject.SetActive(true);
    }

}
