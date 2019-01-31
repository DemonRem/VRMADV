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
    public GameObject ButtonPrefab;
    private GameObject ButtonNow;

    async UniTask Start(){
        Debug.Log("Start"); 
        Vector2 CanvasSize = Canvas.GetComponent<RectTransform>().sizeDelta;
        Debug.Log(CanvasSize.x + ":" + CanvasSize.y);

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
        int margin = 90;
        var ButtonSize =  (CanvasSize.x - margin * 3) / 2;
        Debug.Log(ButtonSize);
        

        foreach(FileInfo f in info)
        {
#if UNITY_EDITOR
            Sprite sprite = await GetMetaData(Application.persistentDataPath + "/ModelData/" + f.Name);

#elif UNITY_ANDROID
            Sprite sprite = await GetMetaData("file://" + Application.persistentDataPath + "/ModelData/" + f.Name);
#endif

            ButtonNow = Instantiate(ButtonPrefab) as GameObject;
            ButtonNow.transform.SetParent(Canvas.transform);
            ButtonNow.name = "Button" + count;
            ButtonNow.GetComponent<RectTransform>().sizeDelta = new Vector2(ButtonSize, ButtonSize);

            if(count % 2 == 0){
                ButtonNow.GetComponent<RectTransform>().anchoredPosition = new Vector2((margin + ButtonSize / 2), - margin - ((ButtonSize + margin) * (count / 2)) - (ButtonSize / 2));
                ButtonNow.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else{
                ButtonNow.GetComponent<RectTransform>().anchoredPosition = new Vector2((margin * 2 + ButtonSize * 1.5f), - margin - ((ButtonSize + margin) * ((count - 1) / 2)) - (ButtonSize / 2));
                ButtonNow.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            GameObject.Find("Button" + count).GetComponent<Image>().sprite = sprite;
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
        Debug.Log(path);
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
        int AvaterNum = 3;
        string[] AvaterPath = new string[AvaterNum];
        for(int num = 0; num < AvaterNum; num++)
        {
            AvaterPath[num] = "model" + num +".vrm";
        }
        Debug.Log("pathの出力に成功");
        return AvaterPath;

    }
}