using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FireInfoWinCtr : MonoBehaviour
{
    public Text titleText;
    public Text descText;
    public Button btnMove;

    public void SetData(FireStyleInfoBean bean)
    {
        titleText.text = bean.Title;
        descText.text = bean.Desc;
    }
	// Use this for initialization
	void Start () {
        btnMove.onClick.AddListener(delegate () { GetComponent<BtnMove>().MoveBtnY(); });

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
