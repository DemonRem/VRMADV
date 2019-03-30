using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UniRx.Async;
using VRM;
public class VRM_reading : MonoBehaviour
{  
    public GameObject Canvas;
    public GameObject Content;
    public GameObject ScrollView;
    public GameObject ButtonPrefab;
    private GameObject ButtonNow;
    private int count = 0;
    private int margin = 90;
    private int AvaterNum = 3;

    async UniTask Start(){
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

        ButtonLayout();
        var converter =  new ByteToVRMConverter();       

        foreach(FileInfo f in info)
        {   string path = Application.persistentDataPath + "/ModelData/" + f.Name;
            VRMMetaObject data = await converter.GetMetaData(path);
            Debug.Log(path);
            if(data.Thumbnail != null){
                Sprite sprite = Sprite.Create(
                    texture : data.Thumbnail,
                    rect : new Rect(0, 0, data.Thumbnail.width, data.Thumbnail.height),
                    pivot : new Vector2(0.5f, 0.5f) 
                );
                ButtonCreate(count,f.Name);
                GameObject obj  = GameObject.Find("Button" + count);
                obj.GetComponent<Image>().sprite = sprite;
                obj.GetComponent<VRMMeta>().Meta = data;
                count++;
            } else {
                Sprite sprite = null;
                ButtonCreate(count,f.Name);
                GameObject obj  = GameObject.Find("Button" + count);
                obj.GetComponent<Image>().sprite = sprite;
                obj.GetComponent<VRMMeta>().Meta = data;
                count++;
            }
        }
    }

    public void ButtonLayout()
    {
        Vector2 CanvasSize = new Vector2(Screen.width, Screen.height);
        Canvas.GetComponent<RectTransform>().sizeDelta = CanvasSize;
        var ButtonSize =  (CanvasSize.x - margin * 3) / 2;
        ScrollView.GetComponent<RectTransform>().sizeDelta = CanvasSize;
        GridLayoutGroup Layout = Content.GetComponent<GridLayoutGroup>();
        Layout.padding.left = margin;
        Layout.padding.top = margin;
        Layout.padding.bottom = margin;
        Layout.cellSize = new Vector2(ButtonSize, ButtonSize);
        Layout.spacing = new Vector2(margin, margin); 
    }

    public void ButtonCreate(int count, string path)
    {
        ButtonNow = Instantiate(ButtonPrefab) as GameObject;
        ButtonNow.transform.SetParent(Content.transform);
        ButtonNow.name = "Button" + count;
        ButtonNow.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        ButtonNow.GetComponent<VRMPath>().Path = path;
    }

    public void DirectoryCheck()
    {
        if (!(Directory.Exists(Application.persistentDataPath + "/" + "ModelData")))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + "ModelData");
        }
    }


    public async UniTask CopyAvater(string path,string model)
    {
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
    }

    public string[] DefaultAvaterPath()
    {
        string[] AvaterPath = new string[AvaterNum];
        for(int num = 0; num < AvaterNum; num++)
        {
            AvaterPath[num] = "model" + num +".vrm";
        }
        return AvaterPath;

    }
}
public class ByteToVRMConverter :IDisposable
{
    public async UniTask<VRMMetaObject> GetMetaData(string path)
    {
        Debug.Log("LoadStart");
        var data = await VRMMetaImporter.ImportVRMMeta(path, true);
        Debug.Log("Loaded");
        return data; 
    }

    public void Dispose()
    {

    }
}