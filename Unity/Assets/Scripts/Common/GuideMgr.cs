using ExcelParser;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine.Events;

public class GuideMgr:Singleton<GuideMgr>
{
    
    public  int CurGuideId { get; set; }
    public  int NextGuideId { get; set; }
    public  string CurFuncName { get; set; }   

    private FlowWDXFireMgr curWDXDB;
    public FlowWDXFireMgr CurWDXFireFlowDB
    {
        get
        {
            if (curWDXDB == null)
            {
                curWDXDB = new FlowWDXFireMgr();
            }
            return curWDXDB;
        }
        set { curWDXDB = value; }
    }

    private FlowWDXFireBean curWDXBean;
    public FlowWDXFireBean CurWDXFireFlowBean
    {
        get
        {
            if (curWDXBean == null)
            {
                curWDXBean = curWDXDB.GetDataById(CurGuideId);
            }
            return curWDXBean;
        }
        set { curWDXBean = value; }
    }

    //---------------------地面流淌火------
    private FlowDiMianFireMgr curDMDB;
    public FlowDiMianFireMgr CurDMFlowDB
    {
        get
        {
            if (curDMDB == null)
            {
                curDMDB = new FlowDiMianFireMgr();
            }
            return curDMDB;
        }
        set { curDMDB = value; }
    }

    private FlowDiMianFireBean curDMBean;
    public FlowDiMianFireBean CurDMFireFlowBean
    {
        get
        {
            if (curDMBean == null)
            {
                curDMBean = curDMDB.GetDataById(CurGuideId);
            }
            return curDMBean;
        }
        set { curDMBean = value; }
    }

    //---------------------敞开式------
    private FlowCKSFireMgr curCKSDB;
    public FlowCKSFireMgr CurCKSFlowDB
    {
        get
        {
            if (curCKSDB == null)
            {
                curCKSDB = new FlowCKSFireMgr();
            }
            return curCKSDB;
        }
        set { curCKSDB = value; }
    }

    private FlowCKSFireBean curCKSBean;
    public FlowCKSFireBean CurCKSFireFlowBean
    {
        get
        {
            if (curCKSBean == null)
            {
                curCKSBean = curCKSDB.GetDataById(CurGuideId);
            }
            return curCKSBean;
        }
        set { curCKSBean = value; }
    }

    //---------------------环形燃烧灭火实验------
    private FlowBaoZhaFireMgr curBZDB;
    public FlowBaoZhaFireMgr CurBZFlowDB
    {
        get
        {
            if (curBZDB == null)
            {
                curBZDB = new FlowBaoZhaFireMgr();
            }
            return curBZDB;
        }
        set { curBZDB = value; }
    }

    private FlowBaoZhaFireBean curBZBean;
    public FlowBaoZhaFireBean CurBZFireFlowBean
    {
        get
        {
            if (curBZBean == null)
            {
                curBZBean = curBZDB.GetDataById(CurGuideId);
            }
            return curBZBean;
        }
        set { curBZBean = value; }
    }

    public GeneralRuleMgr curGeneralRule;
    public GeneralRuleMgr CurGeneralRule
    {
        get
        {
            if (curGeneralRule == null)
            {
                curGeneralRule = new GeneralRuleMgr();
            }
            return curGeneralRule;
        }
        set { curGeneralRule = value; }
    }  
}
