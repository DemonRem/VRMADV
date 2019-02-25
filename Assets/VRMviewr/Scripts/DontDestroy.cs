using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string Path;
    void Start()
    {
        DontDestroyOnLoad(this);   
    }
}
