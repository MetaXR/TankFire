using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUICtr : MonoBehaviour
{
    public GameObject panelRight;
    public Button menueBtn;
    public Button helperBtn;
    public Button tipsBtn;

    public GameObject panelBottom;
    public Button overviewBtn;
    public Button roamingBtn;
    public Button structBtn;
    public Button processTechBtn;
    public Button tongFengBtn;
    public Button jinDuBtn;

	private GameObject panelLeft;
	public GameObject panelMiddle;
    public Button tweenRight;
    public Button tweenBottom;

    public Dictionary<string, Vector3> OldPosList = new Dictionary<string, Vector3>();
    public Dictionary<string, Vector3> DesPosList = new Dictionary<string, Vector3>();

    
 



    void Start()
    {
		panelLeft = GameObject.Find ("Panel_Left");
		panelMiddle.SetActive (false);
        tweenRight.onClick.AddListener(delegate() { tweenRightClick(); });
		tweenBottom.onClick.AddListener(delegate() { tweenBottomClick(); tweenLeftClick();});
        menueBtn.onClick.AddListener(delegate() { SceneLoading.GetInstance().ChangeToScene(SceneName.Main); });      
		helperBtn.onClick.AddListener (delegate() {panelMiddle.SetActive(panelMiddle.activeInHierarchy ? false : true);});
        tipsBtn.onClick.AddListener(delegate() {tipsBtnClick();});

        Vector3 rightPos = panelRight.transform.localPosition;
        Vector3 bottomPos = panelBottom.transform.localPosition;
       
        
        if (panelLeft!=null)
        {
            Vector3 leftPos = panelLeft.transform.localPosition;
            OldPosList.Add(panelLeft.gameObject.name, leftPos);
            leftPos.y -= 130f;
            DesPosList.Add(panelLeft.gameObject.name, leftPos);
            tweenLeftClick();
        }
		
 
        OldPosList.Add(panelRight.gameObject.name, rightPos);
        OldPosList.Add(panelBottom.gameObject.name, bottomPos);
		
        rightPos.x += 100f;
        bottomPos.y -= 130f;
		
        
        DesPosList.Add(panelRight.gameObject.name, rightPos);
        DesPosList.Add(panelBottom.gameObject.name, bottomPos);
		

		tweenBottomClick ();
		//tweenRightClick ();		
    }


    public void tweenRightClick()
    {
        BtnMove btnMove = panelRight.GetComponent<BtnMove>();
        if (btnMove == null)
            return;
        if (!btnMove.init)
        {
            Dictionary<string, Vector3> temp = new Dictionary<string, Vector3>();
            foreach (var item in OldPosList)
            {
                if (item.Key == panelRight.gameObject.name)
                {
                    temp.Add("orign", item.Value);
                }
            }
            foreach (var item in DesPosList)
            {
                if (item.Key == panelRight.gameObject.name)
                {
                    temp.Add("des", item.Value);
                }
            }
            btnMove.Init(temp);
			btnMove.MoveBtnX();
        }
        else
        {
            btnMove.MoveBtnX();
        }
    }

    public void tweenBottomClick()
    {
        BtnMove btnMove = panelBottom.GetComponent<BtnMove>();
        if (btnMove == null)
            return;
        if (!btnMove.init)
        {
            Dictionary<string, Vector3> temp = new Dictionary<string, Vector3>();
            foreach (var item in OldPosList)
            {
                if (item.Key == panelBottom.gameObject.name)
                {
                    temp.Add("orign", item.Value);
                }
            }
            foreach (var item in DesPosList)
            {
                if (item.Key == panelBottom.gameObject.name)
                {
                    temp.Add("des", item.Value);
                }
            }
            btnMove.Init(temp);
			btnMove.MoveBtnY();
        }
        else
        {
            btnMove.MoveBtnY();
        }
    }

	private void tweenLeftClick()
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
			btnMove.MoveBtnY();
		}
		else
		{
			btnMove.MoveBtnY();
		}
	}

    public void tipsBtnClick()
    {
        Client.Instance.DoSetCameraTargetEvent();
    }



}
