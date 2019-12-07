using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIItem : MonoBehaviour
{
    public int ItemID;
    public string imgPath;
    public Image ItemImg;
    public Text ItemName;
    public Toggle ItemToggle;

    public void InitUIItem(int id,string imgpath, string name, bool select,ToggleGroup group)
    {
        ItemID = id;
        ItemName.text = name;
        ItemToggle.isOn = select;
        imgPath = imgpath;
        StartCoroutine(GetImage());
        ItemToggle.group = group;
        if (ItemID == 1)
        {
            ItemToggle.isOn = true;
        }
    }

    IEnumerator GetImage()
    {
        WWW www = new WWW("file://" + Application.streamingAssetsPath + "/" + imgPath + ".png");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            Texture2D tex = www.texture;
            Sprite temp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
            ItemImg.sprite = temp;
        }
        if (www.isDone)
        {
            www.Dispose();
        }
    }  
}
