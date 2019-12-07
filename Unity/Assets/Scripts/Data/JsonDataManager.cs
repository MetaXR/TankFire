using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class JsonDataManager : UnitySingletonDestory<JsonDataManager>
{
    public void GetDeviceInfo(JsonDataType type, Action<DeviceInfoList> over_CallBack, Action error_CallBack)
    {
        DeviceInfoList pTemp = new DeviceInfoList();      
        pTemp.m_nType = type;
        pTemp.over_CallBack = over_CallBack;
        pTemp.error_CallBack = error_CallBack;
        string text =((TextAsset)Resources.Load("Data/DeviceInfo")).text;
        Debug.Log(text);
        Client.Instance.DodeviceInfoLoadEvent(text);
    }    
}
