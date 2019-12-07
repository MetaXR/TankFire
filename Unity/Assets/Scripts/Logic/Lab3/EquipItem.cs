using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour
{
    public int iD;
    public Image iImage;
    public Text iName;
    public string iDesc;
    public string imgPath;
	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void SetData(int id,string name,string imgpath,string desc)
    {
        iD = id;
        iName.text = name;
        imgPath = imgpath;
        StartCoroutine(GetImage());
    }

    public void OnClick()
    {
        Client.Instance.DoChangeCommEquip(iD);
    }

    IEnumerator GetImage()
    {
         WWW www = new WWW("file://" + Application.streamingAssetsPath + "/" + imgPath+".png");
         yield return www;
         if (string.IsNullOrEmpty(www.error))
        {
            Texture2D tex = www.texture;
            Sprite temp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
            iImage.sprite = temp;
        }
        if (www.isDone)
        {
            www.Dispose();
        }
        
    }  
}
