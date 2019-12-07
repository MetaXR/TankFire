using System.Collections;
using ExcelParser;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GuideCtr : MonoBehaviour
{
    public GameObject PanelBottom;
    public Text BTextTitle;
    public Text BTextDesc;
    public Button BBtnNext;
    public Button BBtnNextLeft;
    public GameObject PanelTop;
    public Text OTextTitle;
    public Text OTextDesc;
    public Button OBtnNext;
    void Awake()
    {      
        GuideMgr.Instance.CurWDXFireFlowDB=new FlowWDXFireMgr();
        GuideMgr.Instance.CurCKSFlowDB = new FlowCKSFireMgr();
        GuideMgr.Instance.CurDMFlowDB = new FlowDiMianFireMgr();
        GuideMgr.Instance.CurBZFlowDB = new FlowBaoZhaFireMgr();

        GuideMgr.Instance.CurGeneralRule = new GeneralRuleMgr();
    }
    void Start ()
	{
        Client.Instance.step1Start += this.StartGuide;
	}
    void OnDestory()
    {
        Client.Instance.step1Start -= this.StartGuide;
    }
   
    public void StartGuideWDX(FlowWDXFireBean bean)
    {
        if (bean == null)
        {
            Debug.Log("Exception : tipBean is null!!!");
            return;
        }        
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        GuideMgr.Instance.CurGuideId = bean.Id;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.AddListener(delegate() { Closed(); });
            return;
        }
        BBtnNext.onClick.AddListener(delegate() { NextGuideBottomWDX(GuideMgr.Instance.NextGuideId); });
    }

    void NextGuideBottomWDX(int nextid)
    {
        FlowWDXFireBean bean = GuideMgr.Instance.CurWDXFireFlowDB.GetDataById(nextid);
        if (bean == null)
            return;
        GuideMgr.Instance.CurGuideId = nextid;
        GuideMgr.Instance.NextGuideId = bean.Nextid;        
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.RemoveListener(delegate() { NextGuideBottom(GuideMgr.Instance.NextGuideId); });
            BBtnNext.onClick.AddListener(delegate() { Closed(); });
        }
    }

    //---------------------地面流淌火-------------------
    public void StartGuideDM(FlowDiMianFireBean bean)
    {
        if (bean == null)
        {
            Debug.Log("Exception : tipBean is null!!!");
            return;
        }
       
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        GuideMgr.Instance.CurGuideId = bean.Id;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.AddListener(delegate () { Closed(); });
            return;
        }
        BBtnNext.onClick.AddListener(delegate () { NextGuideBottomDM(GuideMgr.Instance.NextGuideId); });
    }

    void NextGuideBottomDM(int nextid)
    {
        FlowDiMianFireBean bean = GuideMgr.Instance.CurDMFlowDB.GetDataById(nextid);
        if (bean == null)
            return;
        GuideMgr.Instance.CurGuideId = nextid;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
        
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.RemoveListener(delegate () { NextGuideBottom(GuideMgr.Instance.NextGuideId); });
            BBtnNext.onClick.AddListener(delegate () { Closed(); });
        }
    }

    //-----------敞开式燃烧--------   
    public void StartGuideCKS(FlowCKSFireBean bean)
    {
        if (bean == null)
        {
            Debug.Log("Exception : tipBean is null!!!");
            return;
        }
       
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        GuideMgr.Instance.CurGuideId = bean.Id;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.AddListener(delegate () { Closed(); });
            return;
        }
        BBtnNext.onClick.AddListener(delegate () { NextGuideBottomCKS(GuideMgr.Instance.NextGuideId); });
    }

    void NextGuideBottomCKS(int nextid)
    {
        FlowCKSFireBean bean = GuideMgr.Instance.CurCKSFlowDB.GetDataById(nextid);
        if (bean == null)
            return;
        GuideMgr.Instance.CurGuideId = nextid;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
       
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.RemoveListener(delegate () { NextGuideBottom(GuideMgr.Instance.NextGuideId); });
            BBtnNext.onClick.AddListener(delegate () { Closed(); });
        }
    }


    //-----------爆炸燃烧实验--------   
    public void StartGuideBZ(FlowBaoZhaFireBean bean)
    {
        if (bean == null)
        {
            Debug.Log("Exception : tipBean is null!!!");
            return;
        }
       
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        GuideMgr.Instance.CurGuideId = bean.Id;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.AddListener(delegate () { Closed(); });
            return;
        }
        BBtnNext.onClick.AddListener(delegate () { NextGuideBottomBZ(GuideMgr.Instance.NextGuideId); });
    }

    void NextGuideBottomBZ(int nextid)
    {
        FlowBaoZhaFireBean bean = GuideMgr.Instance.CurBZFlowDB.GetDataById(nextid);
        if (bean == null)
            return;
        GuideMgr.Instance.CurGuideId = nextid;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
        
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.RemoveListener(delegate () { NextGuideBottom(GuideMgr.Instance.NextGuideId); });
            BBtnNext.onClick.AddListener(delegate () { Closed(); });
        }
    }

    /// <summary>
    /// 用于总体规律实验提示导航
    /// </summary>
    /// <param name="tmpbean">GlobalRule bean 数据块</param>
    public void StartGuide(IDataBean tmpbean)
    {
        if (tmpbean == null)
        {
            Debug.Log("Exception : tipBean is null!!!");
            return;
        }
        GeneralRuleBean bean = (GeneralRuleBean)tmpbean;
       
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        GuideMgr.Instance.CurGuideId = bean.Id;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
        BBtnNext.onClick.AddListener(delegate() { NextGuideBottom(GuideMgr.Instance.NextGuideId); });
    }

    void NextGuideBottom(int nextid)
    {
        GeneralRuleBean bean =GuideMgr.Instance.CurGeneralRule.GetDataById(nextid);
        if (bean == null)
            return;
        GuideMgr.Instance.CurGuideId = nextid;
        GuideMgr.Instance.NextGuideId = bean.Nextid;
      
        BTextTitle.text = bean.Title;
        BTextDesc.text = bean.Desc;
        PanelBottom.SetActive(true);
        BBtnNext.gameObject.SetActive(false);
        StartCoroutine(Timer(bean.WaitTime, BBtnNext.gameObject));
        BBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj=GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        if (bean.Nextid == 0)
        {
            BBtnNext.onClick.RemoveListener(delegate() { NextGuideBottom(GuideMgr.Instance.NextGuideId); });
            BBtnNext.onClick.AddListener(delegate() { Closed(); });
        }
    }
    public void StartGuideTop(IDataBean tipBean)
    {
        if (tipBean == null)
        {
            Debug.Log("Exception : tipBean is null!!!");
            return;
        }
        GeneralRuleBean bean = (GeneralRuleBean)tipBean;
        PanelTop.SetActive(true);
        OTextTitle.text = bean.Title;
        OTextDesc.text = bean.Desc;
        OBtnNext.onClick.AddListener(delegate() { NextGuideTop(bean.Nextid); });
    }
    void NextGuideTop(int nextid)
    {
        GeneralRuleBean bean = GuideMgr.Instance.CurGeneralRule.GetDataById(nextid);
        PanelTop.SetActive(true);
        OTextTitle.text = bean.Title;
        OTextDesc.text = bean.Desc;
        OBtnNext.GetComponentInChildren<Text>().text = bean.FunctionsBtnName;
        string str = bean.Functions;
        string[] strList = str.Split(',');
        GameObject obj = GameObject.Find(strList[0]);
        obj.SendMessage(strList[1]);
        if (bean.Nextid == 0)
        {
            OBtnNext.onClick.AddListener(delegate() { Closed(); });
            return;
        }
        OBtnNext.onClick.AddListener(delegate() { NextGuideTop(bean.Nextid); });
    }
    IEnumerator Timer(float time,GameObject btn)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
           //btn.GetComponentInChildren<Text>().text = string.Format("等" + "{0}" + "秒", time);
            time--;
        }
        btn.gameObject.SetActive(true);
    }
    void Closed()
    {
        Destroy(gameObject);
    }
}
