using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SceneLoading : SingletonMonoBehavior<SceneLoading>
{
    AsyncOperation async;
    public string m_strNextSceneName;
    bool m_bChanging;
    public  Animation m_LoadingAni;
    public  bool openLoadingAnim ; //loading动画开关
	public Image m_pBlackImage;
    public Dictionary<string, string> SceneNameDic = new Dictionary<string, string>();
    

    // Use this for initialization
    void Start () 
	{
        //m_pBlackImage = transform.FindChild("Image_Black").GetComponent<Image>();
        //Invoke("Test", 10); 
        SceneNameDic.Add(SceneName.Lab1, SceneNameStr.Lab1);
        SceneNameDic.Add(SceneName.Lab2, SceneNameStr.Lab2);
        SceneNameDic.Add(SceneName.Lab3, SceneNameStr.Lab3);
        SceneNameDic.Add(SceneName.Lab4, SceneNameStr.Lab4);
        SceneNameDic.Add(SceneName.Lab5, SceneNameStr.Lab5);
        SceneNameDic.Add(SceneName.Lab6, SceneNameStr.Lab6);
        SceneNameDic.Add(SceneName.Lab7, SceneNameStr.Lab7);
        SceneNameDic.Add(SceneName.Lab8, SceneNameStr.Lab8);
        SceneNameDic.Add(SceneName.Lab9, SceneNameStr.Lab9);
        SceneNameDic.Add(SceneName.Lab10, SceneNameStr.Lab10);
    }

 
	// Update is called once per frame
	void Update1 () {
       
//        if (async != null && async.progress == 1)
//        {
//            // 变亮动画
//            m_pBlackImage.color = new Color(m_pBlackImage.color.r, m_pBlackImage.color.g, m_pBlackImage.color.b, m_pBlackImage.color.a);
//            DOTween.ToAlpha(() => m_pBlackImage.color, x => m_pBlackImage.color = x, 0, _duration).OnComplete(() => BlackToAlpha());
//
//            m_bChanging = false;
//
//            async = null;
//            
//            GameObject pCenter = GameObject.Find("CardboardMain/Head/HeadControl").gameObject;
//            if (pCenter)
//            {
//                pCenter.SetActive(true);
//                GC.Collect();
//            }
//            StopLoadAnimation();
//
//
//        }
    }

//    public void ChangeToScene(string strSceneName, LeVRVideoTransitionType transitionType = LeVRVideoTransitionType.toDark)
//    {
//        if (m_bChanging)
//            return;
//
//        m_bChanging = true;
//
//        //GlobleData.GetInstance().m_strLastSceneName = m_strNextSceneName;
//
//        m_strNextSceneName = strSceneName;
//
//        // 变暗动画        
//
//        switch (transitionType)
//        {
//            case LeVRVideoTransitionType.toDark:
//                m_pBlackImage.color = new Color(0, 0, 0, m_pBlackImage.color.a);
//                DOTween.ToAlpha(() => m_pBlackImage.color, x => m_pBlackImage.color = x, 1, _duration).OnComplete(AlphaToBlack);
//                break;
//            case LeVRVideoTransitionType.toLight:
//                m_pBlackImage.color = new Color(1, 1, 1, m_pBlackImage.color.a);
//                DOTween.ToAlpha(() => m_pBlackImage.color, x => m_pBlackImage.color = x, 1, _duration).OnComplete(AlphaToBlack);
//                break;
//            case LeVRVideoTransitionType.none:
//                AlphaToBlack();
//                break;
//            default:
//                throw new ArgumentOutOfRangeException();
//        }
//
//        //Loading动画
//        if (openLoadingAnim) { PlayLoadAnimation(); }
//        CenterCross.Instance.SetDelayEnable(4.0f);
//       
//    }



    void BlackToAlpha()
    {
        m_pBlackImage.color = new Color(0, 0, 0, 0);
    }

    public void PlayLoadAnimation()
    {
        if (!m_LoadingAni.isPlaying)
        {
            m_LoadingAni.gameObject.SetActive(true);
            m_LoadingAni.Play("Yan");
        }
    }

    public void StopLoadAnimation()
    {
        if (m_LoadingAni.isPlaying)
        {          
            m_LoadingAni.Stop("Yan");
            m_LoadingAni.gameObject.SetActive(false);
        }
    }

    private void AlphaToBlack()
    {
//        switch (m_strNextSceneName)
//        {
//            case "playScene":
//                string strId = GlobleData.GetInstance().m_pDetailItemData.contentId;
//                string strType = GlobleData.GetInstance().m_pDetailItemData.type;
//                JsonDataManager.GetInstance().GetPlayPageData(strId, strType, JsonDataType.PlayPage, JsonDataCallBack, JsonDataCallBackError);
//                break;
//            case "localScene":
//                InitLocalActionCommonData();
//                StartCoroutine(LoadScene());
//                break;
//            default:
//                StartCoroutine(LoadScene());
//                break;
//        }
    }
   

    void JsonDataCallBackError()
    {
//        if (GlobleData.GetInstance().m_pInfo != null)
//        {
//            GlobleData.GetInstance().m_pInfo = null;
//        }
//        StartCoroutine(LoadScene());
//        return;
    }   

	public void ChangeToScene(string sceneName)
	{
		ResetSceneInfo();
		GlobalDate.GetInstance().LastSceneName = SceneManager.GetActiveScene().name;
        m_strNextSceneName = sceneName;
        if (SceneManager.GetActiveScene().name !=sceneName)
	    {
            StartCoroutine("LoadScene");
	    }

		NetMgr.GetInstance ().SendNetMsg ("ChangeToScene", sceneName);
    }

    IEnumerator LoadScene()
    { 
        async = SceneManager.LoadSceneAsync(m_strNextSceneName);           
        yield return async;
		GlobalDate.GetInstance().curSceneName = m_strNextSceneName;
    }

	void ResetSceneInfo()
	{
		for (int i = 0; i < GlobalDate.GetInstance().commonMatList.Count; i++)
		{ 
			Material temp = GlobalDate.GetInstance().commonMatList [i];
			Utility.SetMaterialRenderingMode(temp, RenderingMode.Opaque);
			DOTween.ToAlpha(() => temp.color, x => temp.color = x, 1f, 0.001f).OnComplete(null);
		}
	}
}
