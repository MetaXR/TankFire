using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnTween : MonoBehaviour,
    ISelectHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Text chineseTextTitle;
    public Text englishTextTextTitle;
    public string chineseTitleStr;
    public string englishTitleStr;
    public string sceneName=SceneName.Lab1;
    //private int sceneId;
    // Use this for initialization
    void Start()
    {
        //string[] strList = sceneName.Split('b');
        //sceneId = System.Convert.ToInt32(strList[1]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        chineseTextTitle.text = chineseTitleStr;
        englishTextTextTitle.text = englishTitleStr;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //GlobalDate.GetInstance().CurSceneId = sceneId;
        SceneLoading.GetInstance().ChangeToScene(sceneName);
    }

    public void OnSelect(BaseEventData eventData)
    {
    }
}
