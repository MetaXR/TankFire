using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System;
public class NetEncryptManager {


    private static NetEncryptManager pInstance;
    // Use this for initialization

    public static NetEncryptManager GetInstance()
    {
        if (pInstance == null)
            pInstance = new NetEncryptManager();

        return pInstance;
    }


    //ksort
    public string ksort(Dictionary<string, string> m_dictionary)
    {
        string mResult = "";
        if (m_dictionary.Count > 0)
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>(m_dictionary);
            lst.Sort(delegate(KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
            {
                return s2.Key.CompareTo(s1.Key);
            });
            m_dictionary.Clear();
            lst.Reverse();
            int num = 0;
            foreach (KeyValuePair<string, string> kvp in lst)
            {
              //  Debug.Log(kvp.Key + "：" + kvp.Value);
                mResult += kvp.Key + "=" + kvp.Value;
                if (num < lst.Count - 1)
                {
                    mResult += "&";
                    num++;
                }

            }
        }

        return mResult;
    }

    //md5 加密
    public string GetMd5(string md5String)
    {
        md5String += "fa034ce350e72d8a3960ba560150cd62";
        string b = "";
        MD5 md5 = MD5.Create();
        byte[] targetData = md5.ComputeHash(Encoding.UTF8.GetBytes(md5String));
        for (int i = 0; i < targetData.Length; i++)
        {
            b = b + targetData[i].ToString("x2");
        }
        return b;
    }

    //Time stamp
    public string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
