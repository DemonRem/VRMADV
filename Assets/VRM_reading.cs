using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UniRx.Async;
using VRM;
public class VRM_reading : MonoBehaviour
{
    async UniTask Start(){
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] info = dir.GetFiles("*.vrm");
        foreach (FileInfo f in info)
        {
            var path = Application.streamingAssetsPath + "/" + f.Name;
            var data = await LoadAvater(path);
            Debug.Log(data.AllowedUser);
        }   
    }

    private async UniTask<VRMMetaObject> LoadAvater(string path)
    {
        var uwr = UnityWebRequest.Get(path);
        await uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            throw new Exception("Cannnot local file:" + path);
        }

        byte[] bytes = uwr.downloadHandler.data;
        var context = new VRMImporterContext();
        context.ParseGlb(bytes);
        var meta =  context.ReadMeta(true);
        return meta;
    }
}