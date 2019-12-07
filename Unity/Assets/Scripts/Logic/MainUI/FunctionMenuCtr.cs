using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FunctionMenuCtr : MonoBehaviour
{
    public GameObject panelMiddle;
    public Button helperBtn;
    public Button tipsBtn;


    void Start()
    {

        panelMiddle.SetActive(false);
        helperBtn.onClick.AddListener(delegate () { panelMiddle.SetActive(panelMiddle.activeInHierarchy ? false : true); });
        tipsBtn.onClick.AddListener(delegate () { tipsBtnClick(); });

    }

    public void tipsBtnClick()
    {
        Client.Instance.DoSetCameraTargetEvent();
    }



}
