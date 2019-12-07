using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TweenPanelCtrl : MonoBehaviour
{

    public Button setButton;
    public List<GameObject> buttonLists = new List<GameObject>();
    public float delay;
    public bool isActive = true;

    private int i = 0;
    private Hashtable argsActive = new Hashtable();
    private Hashtable argsunActive = new Hashtable();


    void Start()
    {
        //button
        setButton.onClick.AddListener(setButtonClick);

        //set itween arg      
        argsunActive.Add("x", 90);
        argsunActive.Add("time", 1f);
        argsunActive.Add("delay", 0.2f);
        argsunActive.Add("easetype", iTween.EaseType.linear);
        argsunActive.Add("oncomplete", "setButtonHide");
        argsunActive.Add("oncompletetarget", this.gameObject);
        //argsunActive.Add("oncompleteparams", i);
        //iTween.RotateTo(buttonLists[i], argsunActive);
        argsActive.Add("x", 0);
        argsActive.Add("time", 1f);
        argsActive.Add("delay", 0.2f);
        argsActive.Add("easetype", iTween.EaseType.linear);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setButtonClick()
    {
        //setButton.onClick.RemoveListener(setButtonClick);
        setButton.onClick.RemoveAllListeners();
        InvokeRepeating("ButtonContrl", 0f, delay);
        //ButtonContrl();
    }

    private void ButtonContrl()
    {

        //if (i < 6)
        //{
        //    if (isActive)
        //    {
        //        iTween.RotateTo(buttonLists[i], argsunActive);
        //    }
        //    else
        //    {
        //        for (int j = buttonLists.Count - 1; j > 0; j--)
        //        {
        //            Debug.Log(buttonLists[j].name);
        //            buttonLists[j].SetActive(true);
        //            iTween.RotateTo(buttonLists[j], Vector3.zero, 0.2f);
        //        }
        //        CancelInvoke("ButtonContrl");
        //        isActive = true;
        //    }
        //}
        //else
        //{
        //    CancelInvoke("ButtonContrl");
        //    i = 0;
        //    isActive = false;
        //}
        #region 版本一
        //if (isActive)
        //{
        //    for (int i = 0; i < buttonLists.Count; i++)
        //    {

        //        iTween.RotateTo(buttonLists[i], argsunActive);
        //    }
        //    isActive = false;
        //}
        //else
        //{
        //    for (int i = buttonLists.Count - 1; i > -1; i--)
        //    {
        //        iTween.RotateTo(buttonLists[i], argsActive);
        //    }
        //    isActive = true;
        //}

        #endregion
        //
        if (i < buttonLists.Count)
        {            
            if (isActive)
            {
                iTween.RotateTo(buttonLists[i], argsunActive);
                i++;
            }
            else
            {
                iTween.RotateTo(buttonLists[buttonLists.Count - i - 1], argsActive);
                i++;
            }

        }
        else
        {
            CancelInvoke("ButtonContrl");
            if (isActive)
            {
                Invoke("setisActiveFalse", 0.2f);
            }
            else
            {
                Invoke("setisActiveTrue", 0.2f);
            }          
            i = 0;
        }

        #region 版本2


        #endregion
    }
    private void setisActiveFalse()
    {
        setButton.onClick.AddListener(() => setButtonClick());

        isActive = false;
    }
    private void setisActiveTrue()
    {
        setButton.onClick.AddListener(() => setButtonClick());

        isActive = true;
    }
}
