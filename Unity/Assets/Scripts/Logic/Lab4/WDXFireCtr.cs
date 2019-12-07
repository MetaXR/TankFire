using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using Model;
using UnityStandardAssets.Cameras;
using ExcelParser;
using MirzaBeig.VFX;

public class WDXFireCtr : MonoBehaviour
{
    public FireType fireType = FireType.WENDING;
    public Button menueBtn;
    public Button EquipSettingBtn;    
    private GameObject localPerson;
    public GameObject freeLookCamera;
    public GameObject overLookCamera;
    public bool haveGun = false;
    public GameObject tipWin;
    private FireTankMgr fireTankDB;
    private FireTankBean fireTankBean;
    private FireEquip fireEquip;
    public GameObject guidObj;
    public GameObject MarkPoint;
    public ParticleSystems FXFlow;

    void Start()
    {
        Client.Instance.onTankDead += GameOver;
        Client.Instance.onTankBeAttacking += GameStart;
        Client.Instance.setLocalPlayer += InitLocalPlayer;      
        Game.Scene.GetComponent<EventComponent>().Run(EventIdType.CreateLobbyUI);
        WDXFireMgr.Instance.InitData();
        WDXFireMgr.Instance.roomType = (RoomType)fireType;
        menueBtn.onClick.AddListener(delegate () { SceneLoading.GetInstance().ChangeToScene(SceneName.Main); });
        EquipSettingBtn.onClick.AddListener(EquipBtnClick);
        fireTankDB = new FireTankMgr();
        fireTankBean = fireTankDB.GetDataById((int)fireType);
        if (fireTankBean == null)
        {
            return;
        }
        tipWin = Instantiate(Resources.Load(FilePath.uiPath + "Canvas_TipWindow") as GameObject);
        tipWin.GetComponent<BgWindow>().InitData(StartFireLab, ClosedBtn, fireTankBean.Title, fireTankBean.Desc);
        MarkPoint.SetActive(false);
    }
    void OnDestroy()
    {
        Client.Instance.onTankDead -= GameOver;
        Client.Instance.onTankBeAttacking -= GameStart;
        Client.Instance.setLocalPlayer -= InitLocalPlayer;
    }

    public void InitLocalPlayer(Unit unit)
    {
        freeLookCamera.GetComponent<FreeLookCam>().SetTarget(unit.GameObject.transform);
        localPerson = unit.GameObject;
        PlayerComponent.Instance.MyPlayer.UnitId = unit.Id;
    }
    
    public void StartFireLab()
    {
        if (tipWin != null)
        {
            Destroy(tipWin.gameObject);
        }
        //FlowWDXFireMgr db = new FlowWDXFireMgr();
        //FlowWDXFireBean bean = db.GetDataById(1);
        guidObj = Instantiate(Resources.Load(FilePath.uiPath + "Canvas_Guide")) as GameObject;
        GuideCtr guidCtr = guidObj.GetComponent<GuideCtr>();
        StartCoroutine(Timer(1f, guidCtr,1));

    }
    public IEnumerator Timer(float time, GuideCtr guidCtr,int id)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            //btn.GetComponentInChildren<Text>().text = string.Format("等" + "{0}" + "秒", time);
            time--;
        }
        switch(fireType)
        {
            //------冷却实验
            case FireType.WENDING:
                FlowWDXFireMgr db = new FlowWDXFireMgr();               
                guidCtr.StartGuideWDX(db.GetDataById(id));
                break;
            case FireType.BAOZHA:
                FlowBaoZhaFireMgr dbbz = new FlowBaoZhaFireMgr();
                guidCtr.StartGuideBZ(dbbz.GetDataById(1));
                break;
            case FireType.CHANGKAISHI:
                FlowCKSFireMgr dbcks = new FlowCKSFireMgr();
                guidCtr.StartGuideCKS(dbcks.GetDataById(id));
                break;             
            case FireType.DIMIAN:
                FlowDiMianFireMgr dbdm = new FlowDiMianFireMgr();                
                guidCtr.StartGuideDM(dbdm.GetDataById(id));
                break;
            case FireType.FEIYI:
                //guidCtr.StartGuideWDX((FlowWDXFireBean)bean);
                break;
            default:
                break;
        }
    }
    public void ThirdView()
    {
        overLookCamera.gameObject.SetActive(false);
        freeLookCamera.SetActive(true);
    }
    public void OverLookView()
    {
        freeLookCamera.SetActive(false);
        overLookCamera.SetActive(true);
        overLookCamera.GetComponent<RtsCamera>().ResetToInitialValues(true, false);
    }

    public void EquipBtnClick()
    {
        if (tipWin == null)
        {
            tipWin = Instantiate(Resources.Load(FilePath.uiPath + "Canvas_TipWindow") as GameObject);
        }

        FireItemMgr db = new FireItemMgr();
        tipWin.GetComponent<BgWindow>().InitDataDouble(delegate ()
        {
            SendFireEquipStartRequest();
        }, ClosedBtn, db, false);
    }

    public void SendFireEquipStartRequest()
    {
        EquipInfo equipInfo = new EquipInfo();
        equipInfo.unitID = PlayerComponent.Instance.MyPlayer.UnitId;
        equipInfo.equipType = (int)WDXFireMgr.Instance.CurEquipeType;
        equipInfo.volume = WDXFireMgr.Instance.CurFieItem.LiquidSetting["Value"];
        equipInfo.vectory = WDXFireMgr.Instance.CurFieItem.LiquidSetting["Vectory"];
        SessionComponent.Instance.Session.Send(new Actor_FrieEquipStart()
        {
            MyEquipInfo = equipInfo
        });
    }

    public void WaterBtn(int id)
    {
        for (int i = 0; i < localPerson.transform.childCount; i++)
        {
            GameObject child = localPerson.transform.GetChild(i).gameObject;
            if (child.GetComponent<FireEquip>() != null)
            {
                fireEquip = child.GetComponent<FireEquip>();
                fireEquip.waterOrPaoMo = true;
                fireEquip.StartParticle();
                haveGun = true;
            }
        }

        if (!haveGun)
        {
            GameObject gun = Instantiate(Resources.Load(FilePath.fxPath + "equipWaterGun") as GameObject) as GameObject;
            gun.transform.parent = localPerson.transform;
            gun.transform.localPosition = new Vector3(0.3f, 0, 0.6f);
            if (gun.GetComponent<FireEquip>() != null)
            {
                fireEquip = gun.GetComponent<FireEquip>();
                fireEquip.PlayerID = PlayerComponent.Instance.MyPlayer.UnitId;
                fireEquip.waterOrPaoMo = true;
                fireEquip.StartParticle();
                haveGun = true;
            }
            else
            {
                Debug.Log("the gun form person is error!!!");
                haveGun = false;
            }
        }
        else
        {
            for (int i = 0; i < localPerson.transform.childCount; i++)
            {
                GameObject child = localPerson.transform.GetChild(i).gameObject;
                if (child.GetComponent<FireEquip>() != null)
                {
                    fireEquip = child.GetComponent<FireEquip>();
                    fireEquip.waterOrPaoMo = true;
                    fireEquip.StartParticle();
                }
            }
        }
    }
    public void GameOver()
    {
        FireTankComponent.Instance.MyFireTank.EndData();
        WDXFireFlowMgr db = new WDXFireFlowMgr();
        WDXFireFlowBean bean = db.GetDataById(8);
        if (guidObj == null)
        {
            guidObj = Instantiate(Resources.Load(FilePath.uiPath + "Canvas_Guide")) as GameObject;
        }

        GuideCtr guidCtr = guidObj.GetComponent<GuideCtr>();
        StartCoroutine(Timer(1, guidCtr, 8));
    }
    public void GameStart()
    {
        WDXFireFlowMgr db = new WDXFireFlowMgr();
        WDXFireFlowBean bean = db.GetDataById(4);
        if (guidObj != null)
        {
            Destroy(guidObj);
        }
        guidObj = Instantiate(Resources.Load(FilePath.uiPath + "Canvas_Guide")) as GameObject;
        GuideCtr guidCtr = guidObj.GetComponent<GuideCtr>();
        StartCoroutine(Timer(1, guidCtr, 4));
    }
    public void ClosedBtn()
    {
        if (tipWin != null)
        {
            Destroy(tipWin.gameObject);
        }
        GameObject menueObj = Resources.Load(FilePath.uiPath + "Canvas_Menue") as GameObject;
        Instantiate(menueObj);
    }

    /*------------------------------------------GuidMgr_functions-------------------------------------*/
    public void StepAnim2()
    {
        Debug.Log("冷却实验1--火情侦查---");
        OverLookView();
        MarkPoint.SetActive(true);
    }
    public void StepAnim3()
    {
        Debug.Log("冷却实验2--冷却水枪设置---");
        ThirdView();
        EquipBtnClick();
    }
    public void StepAnim4()
    {
        Debug.Log("冷却实验3--开始油罐冷却---");       
    }
    public void StepAnim5()
    {
        Debug.Log("冷却实验4--油罐冷却完成---");
        FXFlow.stop();
    }
   
    public void StepAnim6()
    {

    }
    public void StepAnim7()
    {

    }
    public void StepAnim8()
    {

    }

    public void StepAnim9()
    {

    }
    public void StepAnim10()
    {

    }
    public void StepAnim0()
    {

    }

}
