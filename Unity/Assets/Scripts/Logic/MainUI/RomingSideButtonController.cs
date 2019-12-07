using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RomingSideButtonController : MonoBehaviour
{
    //panel
    public GameObject panelLeft;
    public GameObject mainUi;
    //可以被操作的tag
    public Tags tagName = Tags.CanSelected;
    //控制位置
    public Dictionary<string, Vector3> OldPosList = new Dictionary<string, Vector3>();
    public Dictionary<string, Vector3> DesPosList = new Dictionary<string, Vector3>();
    //LeftPanel
    public Button freeRomingBtn;
    public Button overLookRomingBtn;
    //detailShowPanel
    public GameObject detailShowPanel;
    public Image[] layerTiltles;
    public Text detailTitle;
    public Text tipsRightTopTitleText;
    public Text tipsRightTopDescText;
    public Text tipsLeftTopTitleText;
    public Text tipsLeftTopDescText;
    public Button dtailAutoRotationBtn;
    public Button detailBackBtn;

    //漫游GameObject
    public GameObject thirdCharaterController;//第三人称
    public GameObject freeLookCamera;//自由漫游相机
    public GameObject freeLookGameObject;//自由漫游整体
    public GameObject detailShowCamera;//细节展示摄像机
    //模型
    public List<GameObject> ModelList;
    private Dictionary<string, GameObject> ModelManagerDic = new Dictionary<string, GameObject>();
    //俯视漫游
    public GameObject overLookCamera;
    //是否正在俯视
    private bool isOverLooking=true;

    void Start()
    {
        freeRomingBtn.onClick.AddListener(delegate() { freeRomingBtnClick(); });
        overLookRomingBtn.onClick.AddListener(delegate() { OverlookRomingBtn(); });
        //Modelg管理
        ModelList[0].SetActive(true);//整体模型
        ModelList[1].SetActive(false);//
        foreach (var modle in ModelList)
        {
            ModelManagerDic.Add(modle.name, modle);
            //Debug.Log(modle.name);
        }

        //动画Button
        Vector3 leftPos = panelLeft.transform.localPosition;
        OldPosList.Add(panelLeft.gameObject.name, leftPos);
        leftPos.x -= 100f;
        DesPosList.Add(panelLeft.gameObject.name, leftPos);

        //Camera
        overLookCamera.SetActive(true);
        freeLookCamera.SetActive(false);
        
        //DetailShowPanel
        detailShowPanel.SetActive(false);
    }

    void Update()
    {
        if (isOverLooking)
        {
            CheckClick();
        }
        
    }
    #region ButtonClick

    //自由漫游按钮
    public void freeRomingBtnClick()
    {
        //是否俯视漫游
        isOverLooking = false;
       
        overLookCamera.gameObject.SetActive(false);
        freeLookGameObject.SetActive(true);
        freeLookCamera.SetActive(true);
        thirdCharaterController.transform.localPosition = new Vector3(-139.05f, -71.61039f, 61.41f);//重置人物位置
        thirdCharaterController.transform.eulerAngles = new Vector3(0, 180.395f, 0);
        freeLookCamera.transform.localPosition = new Vector3(-139.05f, -71.61f, 61.41f);
        freeLookCamera.transform.localEulerAngles = new Vector3(0f, 0f, 0f);//重置摄像机角度        
        thirdCharaterController.SetActive(true);
    }
    #endregion

    //俯视图
    public void OverlookRomingBtn()
    {
        isOverLooking = true;
        freeLookGameObject.SetActive(false);
        overLookCamera.SetActive(true);
        //RomingCameraManager.Instance.SetCameraActive(CameraNames.OverLookCamera);
        if (overLookCamera != null)
        {
            //overLookCamera.GetComponent<RtsCamera>().Follow(ModelList[0]);
        }
        overLookCamera.GetComponent<RtsCamera>().ResetToInitialValues(true, false);
    }


    //detailPanel  
    //返回button
    public void DetailBackBtn()
    {
        ModelList[0].SetActive(true);
        ModelList[1].SetActive(false);
        ModelList[2].SetActive(true);
        ModelList[3].SetActive(false);
        ModelList[4].SetActive(false);
        ModelList[5].SetActive(false);
        ModelList[7].SetActive(true);
        
        detailShowCamera.SetActive(false);
        detailShowPanel.SetActive(false);
        mainUi.SetActive(true);
        panelLeft.SetActive(true);
        overLookCamera.GetComponent<RtsCamera>().CameraTarget.gameObject.SetActive(true);
        detailShowCamera.GetComponent<DetailCameraCtr>().isAutoSpin = false;
        detailShowCamera.GetComponent<DetailCameraCtr>().Start();
        overLookCamera.GetComponent<RtsCamera>().ResetToInitialValues(true, true);
        if (overLookCamera != null)
        {
            overLookCamera.GetComponent<RtsCamera>().Follow(ModelList[0]);
        }

    }
   
    //自动旋转button
    public void DetailPlayBtn()
    {
        detailShowCamera.GetComponent<DetailCameraCtr>().AutoSpin();
    }
    
    public void tweenLeftClick()
    {
        BtnMove btnMove = panelLeft.GetComponent<BtnMove>();
        if (btnMove == null)
            return;
        if (!btnMove.init)
        {
            Dictionary<string, Vector3> temp = new Dictionary<string, Vector3>();
            foreach (var item in OldPosList)
            {
                if (item.Key == panelLeft.gameObject.name)
                {
                    temp.Add("orign", item.Value);
                }
            }
            foreach (var item in DesPosList)
            {
                if (item.Key == panelLeft.gameObject.name)
                {
                    temp.Add("des", item.Value);
                }
            }
            btnMove.Init(temp);
        }
        else
        {
            btnMove.MoveLeftBtnX();
        }
    }

    //点击模型显示
    private void CheckClick()
    {
        //防止穿过UI点击场景
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //选择物体
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.collider.gameObject != null)
                {
                    #region 备用选中
                    //EventTrigger tigger = hit.collider.gameObject.GetComponent<EventTrigger>();
                    //if (tigger != null)
                    //{
                    //    ExecuteEvents.Execute(hit.collider.gameObject, null, ExecuteEvents.updateSelectedHandler);
                    //}
                    //Debug.Log(hit.collider.gameObject.tag);
                    //Debug.Log(hit.collider.gameObject.name);

                    //if (hit.collider.gameObject != null && hit.collider.gameObject.GetComponent<SelectableObj>() != null)
                    //{
                    //    GameObject obj = hit.collider.gameObject;
                    //    SelectableObj sObj = hit.collider.gameObject.GetComponent<SelectableObj>();
                    //    sObj.DoThing();
                    //    overLookBackBtn.gameObject.SetActive(true);//button显示
                    //    //_rtsCamera.ResetToInitialValues(false, false);//重置位置
                    //    _rtsCamera.SetCameraValue(obj);
                    //    _rtsCamera.Follow(obj);
                    //} 
                    #endregion
                    var tag = hit.collider.gameObject.tag;
                    if (tag == tagName.ToString())
                    {
                        SelectedObj temp = hit.collider.gameObject.GetComponent<SelectedObj>();
                        if (temp != null)
                        {
                            temp.SetDetilData(hit.collider.gameObject.name);
                           // SetLayerTitleImage(temp.layerIndex);
                        }
                        foreach (var model in ModelManagerDic)
                        {
                            if (model.Key == hit.collider.gameObject.name)
                            {
                                model.Value.SetActive(true);
                                SetDetailPanel(model.Key);
                                detailShowPanel.SetActive(true);
                                panelLeft.SetActive(false);//左边panel隐藏
                                mainUi.SetActive(false);//主UI隐藏
                                overLookCamera.GetComponent<RtsCamera>().CameraTarget.gameObject.SetActive(false);
                                detailShowCamera.SetActive(true);//细节展示摄像机
                                detailShowCamera.GetComponent<DetailCameraCtr>().Start();
                            }
                            else
                            {
                                model.Value.SetActive(false);
                            }

                        }
                    }
                }
            }
        }
    }

    //更改LayerTitleImage
    public void SetLayerTitleImage(int index)
    {
        for (int i = 0; i < layerTiltles.Length; i++)
        {
            if (i==index)
            {
                layerTiltles[index].gameObject.SetActive(true);
            }
            else
            {
                layerTiltles[i].gameObject.SetActive(false);
            }
        }
    }

    //设置详细信息展示Panel
    public void SetDetailPanel(string componentName)
    {
		tipsRightTopTitleText.text = CommonTextMgr.instance.GetDataByKey ("data");
		tipsLeftTopTitleText.text = CommonTextMgr.instance.GetDataByKey ("introduce");
        switch (componentName)
        {
		    case "dakua":
			    detailTitle.text = CommonTextMgr.instance.GetDataByKey ("dakuaLayer");
			    tipsRightTopDescText.text = CommonTextMgr.instance.GetDataByKey ("dakuaData");
			    tipsLeftTopDescText.text = CommonTextMgr.instance.GetDataByKey ("dakuamiaoshu");
                break;
		    case "jinchukou":
			    detailTitle.text = CommonTextMgr.instance.GetDataByKey ("jinchukouLayer");//CommonText.jinchukouLayer;
			    tipsRightTopDescText.text = CommonTextMgr.instance.GetDataByKey ("jinchukouData");//CommonText.jinchukouData;
			    tipsLeftTopDescText.text = CommonTextMgr.instance.GetDataByKey ("jinchukouDesc");//CommonText.jinchukouDesc;
                break;
            case "sandong":
			    detailTitle.text = CommonTextMgr.instance.GetDataByKey ("sandongLayer");//CommonText.sandongLayer;
			    tipsRightTopDescText.text = CommonTextMgr.instance.GetDataByKey ("sandongData");//CommonText.sandongData;
			    tipsLeftTopDescText.text = CommonTextMgr.instance.GetDataByKey ("sandongDesc");//CommonText.sandongDesc;
                break;
            case "guanlifang":
			    detailTitle.text = CommonTextMgr.instance.GetDataByKey ("guanlifangLayer");//CommonText.guanlifangLayer;
			    tipsRightTopTitleText.text = CommonTextMgr.instance.GetDataByKey ("guanlifang");//CommonText.guanlifang;
			    tipsRightTopDescText.text = CommonTextMgr.instance.GetDataByKey ("guanlifangData");//CommonText.guanlifangData;
			    tipsLeftTopDescText.text = CommonTextMgr.instance.GetDataByKey ("guanlifangDesc");//CommonText.guanlifangDesc;
                break;
            case "sanliangong":
			    detailTitle.text = CommonTextMgr.instance.GetDataByKey ("sanGongLayer");
			    tipsRightTopDescText.text = CommonTextMgr.instance.GetDataByKey ("sanGongData");
			    tipsLeftTopDescText.text = CommonTextMgr.instance.GetDataByKey ("sanGongDesc");
                break;
            default:
                Debug.Log("Not Find EnumUIType! type: " + componentName.ToString());
                break;
        }
    }

}
