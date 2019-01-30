using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UniRx.Async;
using VRM;
public class VRM_reading : MonoBehaviour
{
    public GameObject Thumbnail;
    async UniTask Start(){
        Debug.Log("Start"); 
        DirectoryCheck();
        string[] AvaterPath = DefaultAvaterPath();
        foreach (string check in AvaterPath)
        {
            Debug.Log(check);
            if (System.IO.File.Exists (Application.persistentDataPath + "/" + "ModelData" + "/" + check) == false)
            {
                await CopyAvater(Application.streamingAssetsPath + "/" + check, check);
            }
        }
        
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/" + "ModelData");
        FileInfo[] info = dir.GetFiles("*.vrm");
        int count = 0;
        foreach(FileInfo f in info)
        {
            Sprite sprite = await GetMetaData(Application.persistentDataPath + "/" + "ModelData" + f.Name);
            GameObject.Find("Image" + count).GetComponent<Image>().sprite = sprite;
            count++;
        }

    }

    public void DirectoryCheck()
    {
        if (!(Directory.Exists(Application.persistentDataPath + "/" + "ModelData")))
        {
            Debug.Log("ディレクトリなし");
            Directory.CreateDirectory(Application.persistentDataPath + "/" + "ModelData");
            Debug.Log("ディレクトリ作成");
        }
    }

    public async UniTask<Sprite> GetMetaData(string path)
    {
        Debug.Log("メタデータ取得開始");
        var data = await LoadAvater(path);
        Debug.Log("Loaded");
        var tex = data.Thumbnail;
        Sprite sprite = Sprite.Create(
           texture : tex,
            rect : new Rect(0, 0, tex.width, tex.height),
            pivot : new Vector2(0.5f, 0.5f) 
        );
        return sprite; 
    }

    public async UniTask<VRMMetaObject> LoadAvater(string path)
    {
        var uwr = UnityWebRequest.Get(path);
        await uwr.SendWebRequest();
        Debug.Log("Bytes:" + uwr.downloadedBytes);

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            throw new Exception("Cannnot local file:" + path);
        }

        byte[] bytes = uwr.downloadHandler.data;
        var context = new VRMImporterContext();
        context.ParseGlb(bytes);
        return context.ReadMeta(true); 
    }

    public async UniTask CopyAvater(string path,string model)
    {
        Debug.Log("モデル読み込み開始");
        var uwr = UnityWebRequest.Get(path);
        await uwr.SendWebRequest();
        Debug.Log("Bytes:" + uwr.downloadedBytes);

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            throw new Exception("Cannnot local file:" + path);
        }

        byte[] bytes = uwr.downloadHandler.data;
        string topath =  Application.persistentDataPath + "/" + "ModelData" + "/" + model;
        File.WriteAllBytes(topath, bytes);
        Debug.Log("コピー成功");
    }

    public string[] DefaultAvaterPath()
    {
        int AvaterNum = 2;
        string[] AvaterPath = new string[AvaterNum];
        for(int num = 0; num < AvaterNum; num++)
        {
            AvaterPath[num] = "model" + num +".vrm";
        }
        Debug.Log("pathの出力に成功");
        return AvaterPath;

    }
}