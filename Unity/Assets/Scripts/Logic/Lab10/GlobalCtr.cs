using System.Collections;
using System.Collections.Generic;
using ExcelParser;
using UnityEngine;
using UnityEngine.UI;
using MirzaBeig.VFX;

public class GlobalCtr : MonoBehaviour
{
    public Button Step2Button;
    public ParticleSystems FxStormFire;
    public ParticleSystems FxTankFire;
    public ParticleSystems FxTerrianFire;
    public ParticleSystems FXFixedWater;
    public ParticleSystems FXWaterStep81;
    public ParticleSystems FXWaterStep82;
    public ParticleSystems FXWaterStep9;
    private GameObject guidObj;
    public GameObject mStep6Model;
    public GameObject mStep81Model;
    public GameObject mStep82Model;
    public GameObject mStep9Model;
    public GameObject mGuanDing;
    public DetailCameraCtr CurCam;

    void Awake()
    {
        Reset(); 
    }
    void Start()
    {
        Step2Button.onClick.AddListener(delegate () { StartRescueFlow(); });
    }

    void Reset()
    {       
        FxStormFire.gameObject.SetActive(false);
        FxTankFire.gameObject.SetActive(false);
        FxTerrianFire.gameObject.SetActive(false);
        FXFixedWater.gameObject.SetActive(false);
        FXWaterStep81.gameObject.SetActive(false);
        FXWaterStep82.gameObject.SetActive(false);
        FXWaterStep9.gameObject.SetActive(false);
        mStep6Model.gameObject.SetActive(false);
        mStep81Model.gameObject.SetActive(false);
        mStep82Model.gameObject.SetActive(false);
        mStep9Model.gameObject.SetActive(false);
        mGuanDing.gameObject.SetActive(true);
        CurCam.targetObject = FxTankFire.transform;
    }

    void ResetFX()
    {
        FxStormFire.stopImediate();
        FxTankFire.stopImediate();
        FxTerrianFire.stopImediate();
        FXFixedWater.stopImediate();
        FXWaterStep81.stopImediate();
        FXWaterStep82.stopImediate();
        FXWaterStep9.stopImediate();

        mStep6Model.gameObject.SetActive(false);
        mStep81Model.gameObject.SetActive(false);
        mStep82Model.gameObject.SetActive(false);
        mStep9Model.gameObject.SetActive(false);
        mGuanDing.gameObject.SetActive(true);
    }

    void StartRescueFlow()
    {
        if (guidObj == null)
        {
            guidObj = Instantiate(Resources.Load(FilePath.uiPath + "Canvas_Guide")) as GameObject;
            IDataBean bean = GuideMgr.Instance.CurGeneralRule.GetDataById(1);
            guidObj.GetComponent<GuideCtr>().StartGuide(bean);
        }
        else
        {
            Client.Instance.DoStep1Start(GuideMgr.Instance.CurGeneralRule.GetDataById(1));
        }
    }

    public void OnMenueBtn()
    {
        SceneLoading.GetInstance().ChangeToScene(SceneName.Main);
    }

    public void Step11()
    {
        Debug.Log("----------Step11----初始状态------");
    }

    public void Step12()
    {
        Debug.Log("----------Step12----火灾开始------");
        FxStormFire.gameObject.SetActive(true);
        FxStormFire.play();
        mGuanDing.gameObject.SetActive(false);
    }

    public void Step13()
    {
        Debug.Log("----------Step13----火灾报警---报警人员-----");
        FxStormFire.stop();
        FxTankFire.gameObject.SetActive(true);
        FxTankFire.play();
    }

    public void Step14()
    {
        Debug.Log("----------Step14----火灾报警----接警人员----");
    }

    public void Step15()
    {
        Debug.Log("----------Step15------固定消防系统------");
        FXFixedWater.gameObject.SetActive(true);
        FXFixedWater.play();
    }

    public void Step16()
    {
        Debug.Log("----------Step16------地面流淌火持续燃烧中------");
        FxTerrianFire.gameObject.SetActive(true);
        FxTerrianFire.play();
    }
    public void Step17()
    {
        Debug.Log("----------Step17-----消防队到场！-------");
        mStep6Model.gameObject.SetActive(true);
        CurCam.targetObject = mStep6Model.transform;
    }

    public void Step18()
    {
        Debug.Log("----------Step18-----火情侦查-------");
        CurCam.targetObject = FxTankFire.transform;
    }

    public void Step19()
    {
        Debug.Log("----------Step18----部署灭火力量，1灭地面火--------");
        mStep6Model.gameObject.SetActive(false);
        mStep81Model.gameObject.SetActive(true);
        FXWaterStep81.gameObject.SetActive(true);
        FXWaterStep81.play();
        StartCoroutine(Timer(15, FxTerrianFire));
        StartCoroutine(Timer(18, FXWaterStep81));
        CurCam.targetObject = mStep81Model.transform;
    }

    public void Step110()
    {
        Debug.Log("----------Step19----部署灭火力量，1灭油罐火--------");
        mStep81Model.gameObject.SetActive(false);
        FXWaterStep81.stop();
        mStep82Model.gameObject.SetActive(true);
        FXWaterStep82.gameObject.SetActive(true);
        FXWaterStep82.play();
        StartCoroutine(Timer(15, FxTankFire));
        StartCoroutine(Timer(18, FXWaterStep82));
        CurCam.targetObject = mStep82Model.transform;

    }

    public void Step111()
    {
        Debug.Log("----------Step111------灭火后冷却------");
     
        mStep82Model.gameObject.SetActive(false);
        mStep9Model.gameObject.SetActive(true);
        FXWaterStep9.gameObject.SetActive(true);
        FXWaterStep9.play();
        StartCoroutine(Timer(15, FXWaterStep9));
        CurCam.targetObject = mStep9Model.transform;
    }

    public void Step112()
    {
        Debug.Log("----------Step112----灭火完成--------");
      
        Reset();
    }

    public void Step113()
    {
        Debug.Log("----------Step113----灭火成功--------");
        Reset();
    }

    public void Step114()
    {
        Debug.Log("----------Step114------灭火失败，固定灭火系统损坏，出现地面流淌火------");
        FXFixedWater.stop();
    }

    IEnumerator Timer(float time, ParticleSystems fx)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            //btn.GetComponentInChildren<Text>().text = string.Format("等" + "{0}" + "秒", time);
            time--;
        }
        fx.stop();
    }   
}
