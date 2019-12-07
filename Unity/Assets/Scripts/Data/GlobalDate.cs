using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class GlobalDate : SingletonMonoBehavior<GlobalDate>
{
    private string lastSceneName;
    public string LastSceneName
    {
        get { return lastSceneName; }
        set { if (value != string.Empty && value != null) lastSceneName = value; }
    }
	public string curSceneName;
	public List<Material> commonMatList = new List<Material>();
    private int curSceneId;
    public int CurSceneId
    {
        get { return curSceneId; }
        set { curSceneId = value; }
    }
    private int id;
    public int ModelID
    {
        get { return id; }
        set { if (value != 0) id = value; }
    }

    private GlobalProto globalProto;
    public GlobalProto InitGlobalProto
    {
        get { return globalProto; }
        set { globalProto = value;}
    }

    const string path = "GlobalProto";

    private void Awake()
    {
        StartCoroutine(GetGlobalFile());
    }

    IEnumerator GetGlobalFile()
    {
        WWW www = new WWW("file://" + Application.streamingAssetsPath + "/" + path + ".txt");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {            
            globalProto = MongoHelper.FromJson<GlobalProto>(www.text);           
        }
        if (www.isDone)
        {
            www.Dispose();
        }
    }
}

public class CamData
{
	public Vector3 LookAt;          // Desired lookat position
	public float Distance;          // Desired distance (units, ie Meters)
	public float Rotation;          // Desired rotation (degrees)
	public float Tilt;              // Desired tilt (degrees)
	public bool Smoothing = true;
	public float LookAtHeightOffset = 0f;

	public void  Init(Vector3 mLookAt,float mDistance,float mRotation,float mTilt,bool mSmoothing,float mLookAtHeightOffset)
	{
		LookAt= mLookAt;          // Desired lookat position
		Distance= mDistance;          // Desired distance (units, ie Meters)
		Rotation= mRotation;          // Desired rotation (degrees)
		Tilt= mTilt;              // Desired tilt (degrees)
		Smoothing = mSmoothing;
		LookAtHeightOffset = mLookAtHeightOffset;
	}
}

public enum TankState
{
    NORMAL=1,
    FIRING,
    BE_ATTACKING,
    DEAD,
    WDXFIRE,
    BZXFIRE,
    FYXFIRE
}

public class FilePath
{
    public const string root = "";
    public const string uiPath = "UI/";
    public const string modelPath = "Model/";
    public const string fxPath = "FX/";
}

public enum FireType
{
    WENDING = 1,
    BAOZHA,
    FEIYI,
    DIMIAN,
    CHANGKAISHI
}

public enum EquipeType
{
    WATER=1,
    PAOMO
}


