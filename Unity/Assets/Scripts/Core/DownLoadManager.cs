using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class DownLoadManager :SingletonMonoBehavior<DownLoadManager>
{
    public Texture2D m_DefaultTex;  
    private static ArrayList pWWWJsonList = new ArrayList();
    private static ArrayList pWWWTextureList = new ArrayList();
    private class DownLoadData
    {
        public string m_strUrl;
        public WWW www;
        public Action<JsonDataType, WWW> download_CallBack;
        public Action<JsonDataType, WWW> download_Error_CallBack;
    }

    public void GetJosnByUrl(string strUrl, JsonDataType nType, Action<JsonDataType, WWW> callback, Action<JsonDataType, WWW> CallbackError)
    {
        StartCoroutine(QueryJosn(strUrl, nType, callback, CallbackError));
    }

    IEnumerator QueryJosn(string strUrl, JsonDataType nType, Action<JsonDataType, WWW> callback, Action<JsonDataType, WWW> CallbackError)
    {
        DownLoadData pTemp = new DownLoadData();

        WWW www = new WWW(strUrl);
        pTemp.www = www;
        pTemp.m_strUrl = strUrl;
        pTemp.download_CallBack = callback;
        pTemp.download_Error_CallBack = CallbackError;
        pWWWJsonList.Add(pTemp);

        yield return www;

        if (www.error != null)
        {

            pTemp.download_Error_CallBack(nType, pTemp.www);
        }
        else
        {
            // ok
            Debug.Log("ok");
            pTemp.download_CallBack(nType, pTemp.www);
        }

    }

    //public void Download_Texture2D(string strUrl, Image pImage)
    //{
    //    string path = PlayerData.cachefilepath + strUrl.GetHashCode();
    //    if (File.Exists(path))
    //    {
    //        StartCoroutine(GetTexturtLocal(path, pImage));
    //    }
    //    else
    //    {
    //        StartCoroutine(GetTexture(strUrl, pImage));
    //    }        
    //}

    //public void DownLoad_Texture2DLocal(string strUrl, Image pImage)
    //{       
    //    StartCoroutine(GetTexturtLocal(strUrl, pImage));
    //}


    //IEnumerator GetTexture(string strUrl, Image pImage)
    //{
    //    WWW wwwTexture = new WWW(strUrl);
    //    yield return wwwTexture;
    //    Texture2D texture = wwwTexture.texture;
    //    if (wwwTexture.error != null)
    //    {
    //        Debug.Log("GetPicError: " + wwwTexture.error);
    //    }
    //    else
    //    {
    //        pImage.overrideSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    //        string write_path = PlayerData.cachefilepath+strUrl.GetHashCode();           
    //        File.WriteAllBytes(write_path, texture.EncodeToJPG());          
    //    }
    //}

    //IEnumerator GetTexturtLocal(string path, Image pImage)
    //{       
    //    string androidPath = "file:///" + path;
    //    if (!File.Exists(path))
    //    {
    //        pImage.overrideSprite = Sprite.Create(m_DefaultTex, new Rect(0, 0, m_DefaultTex.width, m_DefaultTex.height), new Vector2(0.5f, 0.5f)); 
    //        yield break;
    //    }            
    //    WWW wwwTexture = new WWW(androidPath);
    //    yield return wwwTexture;
    //    Texture2D texture = wwwTexture.texture;
    //    if (wwwTexture.error != null)
    //    {
    //        Debug.Log("GetPicError: " + wwwTexture.error);
    //    }
    //    else
    //    {           
    //        pImage.overrideSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    //    }
    //}

    //public void DownLoadText(string path)
    //{       
    //    StartCoroutine(GetTextLocal(path));
    //}

    //IEnumerator GetTextLocal(string path)
    //{
    //    WWW txtfile = new WWW(path);
    //    yield return txtfile;        
    //    StreamReader reader = new StreamReader(txtfile.text, Encoding.Default);
    //    string line;
    //    var dic = new Dictionary<int, string>();
    //    while ((line = reader.ReadLine()) != null)
    //    {
    //        var li = line.Split('=');
    //        dic.Add(System.Convert.ToInt32(li[0]), li[1]);
    //    }
    //    ClientText.TextDic = dic;      
    //}  
}
