using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRM;

public class CanvasMeta : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Thumnail;
    public GameObject Title;
    public GameObject Author;
    public GameObject Version;
    public GameObject ExporterVersion;
    public GameObject Personality_LICENCE;
    public GameObject Violent_LICENCE;
    public GameObject Sexual_LICENCE;
    public GameObject Commercial_LICENCE;
    public GameObject LICENCE_URL;
    void Awake()
    {

        VRMMetaObject Meta = gameObject.GetComponent<VRMMeta>().Meta;
        Sprite sprite = Sprite.Create(
            texture : Meta.Thumbnail,
            rect : new Rect(0, 0, Meta.Thumbnail.width, Meta.Thumbnail.height),
            pivot : new Vector2(0.5f, 0.5f) 
        );
        Thumnail.GetComponent<Image>().sprite = sprite;
        Title.GetComponent<Text>().text = Meta.Title;
        Author.GetComponent<Text>().text = Meta.Author;
        Version.GetComponent<Text>().text = Meta.Version;
        ExporterVersion.GetComponent<Text>().text = Meta.ExporterVersion;
        Personality_LICENCE.GetComponent<Text>().text = Meta.AllowedUser.ToString();
        Violent_LICENCE.GetComponent<Text>().text = Meta.ViolentUssage.ToString();
        Sexual_LICENCE.GetComponent<Text>().text = Meta.SexualUssage.ToString();
        Commercial_LICENCE.GetComponent<Text>().text = Meta.CommercialUssage.ToString();
        LICENCE_URL.GetComponent<Text>().text = Meta.OtherPermissionUrl;
    }
}
