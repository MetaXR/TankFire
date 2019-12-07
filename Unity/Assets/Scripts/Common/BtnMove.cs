using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class BtnMove : MonoBehaviour 
{
	public bool show = true;
	public bool init = false;
	private Dictionary<string,Vector3> PosList = new Dictionary<string, Vector3>();
	public float speedTime = 0.5f;
	//public bool X = true;
	//public bool Y = true;
	//public bool Z = true;
	// Use this for initialization
	void Start()
	{
		
	}

	public void Init(Dictionary<string,Vector3> list)
	{
		if (!init)
		{			
			PosList = list;
		}
		init = true;
	}

	public void MoveBtnX()
	{
		if (!show) {
			Vector3 temp;
			PosList.TryGetValue ("orign",out temp);
			MoveX(gameObject,temp,speedTime);
			show = true;
		} else {
			Vector3 temp;
			PosList.TryGetValue ("des",out temp);
			MoveX(gameObject,temp,speedTime);
			show = false;
		}
	}

	public void MoveBtnY()
	{
		if (!show) {
			Vector3 temp;
			PosList.TryGetValue ("orign",out temp);
			MoveY(gameObject,temp,speedTime);
			show = true;
		} else {
			Vector3 temp;
			PosList.TryGetValue ("des",out temp);
			MoveY(gameObject,temp,speedTime);
			show = false;
		}
	}

    public void MoveBtnY_Show()
    {
        Vector3 temp;
        PosList.TryGetValue("orign", out temp);
        MoveY(gameObject, temp, speedTime);
        show = true;
    }
    public void MoveBtnY_Hide()
    {
        Vector3 temp;
        PosList.TryGetValue("des", out temp);
        MoveY(gameObject, temp, speedTime);
        show = false;
    }

    public void MoveLeftBtnX()
    {
        if (!show)
        {
            Vector3 temp;
            PosList.TryGetValue("orign", out temp);
            MoveLeftX(gameObject, temp, speedTime);
            show = true;
        }
        else
        {
            Vector3 temp;
            PosList.TryGetValue("des", out temp);
            MoveLeftX(gameObject, temp, speedTime);
            show = false;
        }
    }
	private void MoveX(GameObject obj,Vector3 vec,float time)
	{
		//Tweener tweener = obj.GetComponent<Transform>().DOMoveX(vec, time);
		Tweener tweener = obj.GetComponent<Transform>().DOLocalMoveX(vec.x, time);
		tweener.SetUpdate(true);
		tweener.SetEase(Ease.Linear);
	}

	private void MoveY(GameObject obj,Vector3 vec,float time)
	{
		//Tweener tweener = obj.GetComponent<Transform>().DOMoveX(vec, time);
		Tweener tweener = obj.GetComponent<Transform>().DOLocalMoveY(vec.y, time);
		tweener.SetUpdate(true);
		tweener.SetEase(Ease.Linear);
	}

    private void MoveLeftX(GameObject obj, Vector3 vec, float time)
    {
        Tweener tweener = obj.GetComponent<Transform>().DOLocalMoveX(vec.x, time);
        tweener.SetUpdate(true);
        tweener.SetEase(Ease.Linear);
    }
}
