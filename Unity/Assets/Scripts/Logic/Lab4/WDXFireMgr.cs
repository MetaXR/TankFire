using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using ExcelParser;

public class FireItem
{
    private FireItemBean fireItemBean;
    public FireItemBean FireItemBean
    {
        get
        {
            if (fireItemBean != null)
                return fireItemBean;
            else
            {
                FireItemMgr db = new FireItemMgr();
                fireItemBean = db.GetDataById(1);
                return fireItemBean;
            }
        }
        set
        {
            fireItemBean = value;
        }
    }    
    public Dictionary<string, int> LiquidSetting = new Dictionary<string, int>();
    public FireItem()
    { }

    public FireItem(FireItemBean bean, Dictionary<string, int> dic)
    {
        fireItemBean = bean;
        LiquidSetting = dic;
    }

    public void InitData(FireItemBean bean, Dictionary<string, int> dic)
    {
        fireItemBean = bean;
        LiquidSetting = dic;
    }
}

public class WDXFireMgr : Singleton<WDXFireMgr>
{
    //灭火液体
    private FireItem curFireItem;
    public FireItem CurFieItem
    {
        get
        {
            if (curFireItem == null)
            {
                curFireItem = new FireItem();
            }
            return curFireItem;
        }
        set
        {
            curFireItem = value;
        }
    }
    public EquipeType  CurEquipeType=EquipeType.WATER;
    //房间类型
    public RoomType    roomType=RoomType.WENDING;
    //所用液体的流量，射速
    public Dictionary<string, int> LiquidSetting = new Dictionary<string, int>();   

    public void InitData()
    {
        FireItemMgr db = new FireItemMgr();   
        if(LiquidSetting.ContainsKey("Value") ==false)
        {
            LiquidSetting.Add("Value", 50);
            LiquidSetting.Add("Vectory", 8);
        }
        CurFieItem.InitData(db.GetDataById(1), LiquidSetting);       
    }    

    public void SetRoomType(int type)
    {
        roomType = (RoomType)type;
    }

    public void SetSettingDic(Dictionary<string, int> dic)
    {
        CurFieItem.LiquidSetting = dic;
    }
}
