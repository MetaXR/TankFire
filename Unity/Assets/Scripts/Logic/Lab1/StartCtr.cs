using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartCtr : MonoBehaviour
{
    public Button comTankBtn;
    public Button topViewBtn;
    public Button thirdViewBtn;
    public GameObject thirdCharaterController;//第三人称
    public GameObject freeLookCamera;//自由漫游相机
    public GameObject freeLookGameObject;//自由漫游整体
    public GameObject overLookCamera;
    public bool isOverLooking=true;

    public string tagName = "CanSelected";
	// Use this for initialization
	void Start ()
	{
        comTankBtn.onClick.AddListener(delegate() { SceneManager.LoadSceneAsync(SceneName.Detail); });
	    OverlookRomingBtn();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (isOverLooking){CheckClick();}
	}

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
                        GlobalDate.GetInstance().ModelID = temp.modelID;
                        SceneLoading.GetInstance().ChangeToScene(SceneName.Detail);
                    }
                }
            }
        }
    }
}
