using System.Collections;
using System.Collections.Generic;
using ExcelParser;
using UnityEngine;
using Model;

public class Client : Singleton<Client>
{    
    public delegate void Tips( );
    public event Tips tipEvent;

    public delegate void ChangeLayerTips(Transform trans);
    public event ChangeLayerTips changeLayerEvent;

    public delegate void ShowConTechMenue();
    public event ShowConTechMenue showConTechMenue;

    public delegate void ClearCurLayerinfoWin(Transform parent);
    public event ClearCurLayerinfoWin clearCurInfo;

    public delegate void DeviceInfoLoadSuc(string text);
    public event DeviceInfoLoadSuc deviceInfoLoadEvent;

    //关闭小球
    public delegate void SetCameraTargetEvent();
    public event SetCameraTargetEvent setCameraTargetEvent;

	public delegate void SetCameraValue(CamData data);
	public event SetCameraValue setCamValueEvent;

    public delegate void SetTankCellInfo(int mid,int cid);
    public event SetTankCellInfo setTankCell;

    public delegate void ChangeCommEquip(int id);
    public event ChangeCommEquip changeCommEquip;

   /*------------------------稳定性火灾-推塔事件------------------------*/

    public delegate void OnTankNormal();

    public event OnTankNormal onTankNormal;

    public delegate void OnTankFiring();

    public event OnTankFiring onTankFiring;

    public delegate void OnTankBeAttacking();

    public event OnTankBeAttacking onTankBeAttacking;

    public delegate void OnTankDead();

    public event OnTankDead onTankDead;


    /*------------------------总体规律认知------------------------*/

    public delegate void Step1Start(IDataBean bean);

    public event Step1Start step1Start;

    /*--------------------------LocalPlayer---------------------*/
    public delegate void SetLocalPlayer(Unit unit);
    public event SetLocalPlayer setLocalPlayer;

    public void DoShow()
    {
        if (tipEvent != null)
        {
            tipEvent();
        }
    }

    public delegate bool ShowWindow(bool show,string strtitle, string strcontext,out GameObject self);
    public event ShowWindow showWinEvent;

    public bool DoshowWinEvent(bool show,string strtitle, string strcontext, out GameObject self)
    {
        if (showWinEvent != null)
        {
            showWinEvent(show, strtitle, strcontext, out self);
            return true;
        }
        self = null;
        return false;
    }

	/*
    public void DochangeLayerEvent(Transform trans)
    {
        if (changeLayerEvent != null)
        {
            changeLayerEvent(trans);
        }
    }
    */

    public void DoShowConTechMenue()
    {
        if(showConTechMenue !=null)
        {
            showConTechMenue(); 
        }
    }

    public void DoClearCurInfoWindow(Transform parent)
    {
        if (clearCurInfo != null)
        {
            clearCurInfo(parent);
        }
    }

    public void DodeviceInfoLoadEvent(string text)
    {
        if (deviceInfoLoadEvent != null)
        {
            deviceInfoLoadEvent(text);
        }
    }

    public void DoSetCameraTargetEvent()
    {
        if (setCameraTargetEvent != null)
        {
            setCameraTargetEvent();
        }
    }

	public void DoSetCameraValueEvent(CamData data)
	{
		if (setCamValueEvent != null)
		{
			setCamValueEvent(data);
		}
	}

    public void DoSetTankCellInfo(int mid,int cid)
    {
        if (setTankCell != null)
        {
            setTankCell(mid,cid);
        }
    }

    public void DoChangeCommEquip(int id)
    {
        if (changeCommEquip != null)
        {
            changeCommEquip(id);
        }
    }

    /*-----推塔事件-------------------------------------------------------*/

    public void DoTankNormal()
    {
        if (onTankNormal != null)
        {
            onTankNormal();
        }
    }

    public void DoTankFiring()
    {
        if (onTankFiring != null)
        {
            onTankFiring();
        }
    }

    public void DoTankBeAttacking()
    {
        if (onTankBeAttacking != null)
        {
            onTankBeAttacking();
        }
    }

    public void DoTankDead()
    {
        if (onTankDead != null)
        {
            onTankDead();
        }
    }

    /*---------------------------总体规律---------------------------------*/

    public void DoStep1Start(IDataBean bean)
    {
        if (step1Start != null)
        {
            step1Start(bean);
        }
    }

    public void DoSetLocalPlayer(Unit unit)
    {
        if (setLocalPlayer != null)
        {
            setLocalPlayer(unit);
        }
    }

    //---------------
    public delegate void SetifCollision(bool index, long PlayerID);
    public event SetifCollision setifCollision;

    public void DoSetifCollision(bool index,long PlayerID)
    {
        if (setifCollision != null)
        {
            setifCollision(index,PlayerID);
        }
    }

}
