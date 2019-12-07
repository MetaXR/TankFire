using System.Collections;
using System.Collections.Generic;
using System;

public class QueryData
{
    public JsonDataType m_nType;
}

public enum JsonDataType
{
    FirstPage,      //  首页
    ChannelData,   //  精选列表
    ChannelPage,   //  精选
    DetailPage,     //  详情页
    PlayPage,     //  播放页
    RecommendPage,//推荐页
    LiveHallPage,
    LiveLogin,
    DeviceInfo
}

[System.Serializable]
public class DeviceInfo
{
    public string id;
    public string layer;
    public string name;
    public string title;
    public string desc;   
}

public class DeviceInfoList:QueryData
{
    public int status;
    public DeviceInfo[] rukouList;
    public DeviceInfo[] zhantai1List;
    public DeviceInfo[] zhantai2List;
    public DeviceInfo[] suidaoList;
    public int errno;
    public string errmsg;
    public Action<DeviceInfoList> over_CallBack;
    public Action error_CallBack;
}
