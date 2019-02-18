using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenURL : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    public void Link()
    {
        var url = text.GetComponent<Text>().text;
        Debug.Log(url);
        Application.OpenURL(url);
    }
}
