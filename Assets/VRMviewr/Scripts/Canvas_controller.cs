using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas_prefab;
    public void OnClickDo()
    {
        GameObject SelectUI =  Instantiate(canvas_prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void OnButtonYes()
    {
        Destroy(transform.root.gameObject);
    }

    public void OnButtonNo()
    {
        Destroy(transform.root.gameObject);
    }

}
