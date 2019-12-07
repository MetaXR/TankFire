using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EquipTip : MonoBehaviour
{
    private Camera TargetCamera;
	// Use this for initialization
	void Start ()
	{
	    TargetCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
	{
	  
	}

    void LateUpdate()
    {
        Vector3 tarPos = TargetCamera.transform.position;
        tarPos = transform.position - tarPos;
        tarPos = transform.position + tarPos;
        GetComponent<RectTransform>().DOLookAt(tarPos, 0.5f, AxisConstraint.Y);
    }

}
