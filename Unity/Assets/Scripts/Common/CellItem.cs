using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellItem : MonoBehaviour
{
    public float lightFactor = 3f;
    public float backFactor = 1f;
    public int matindex;
    public int layerIndex;
    public float averageDistance;
    public int modelID;
    public int CellID;
    public bool ifTransparent=false;
    public Material[] materialList;
    private float aphValue;
    public float AphValue {
        get { return aphValue;}
        set { aphValue = value;}
    }
    private float tweenTime;
    public float TweenTime{
        get { return tweenTime;}
        set { tweenTime = value;}
    }

    public void OnMouseEnter()
    {
        if (gameObject.GetComponent<Renderer>() == null) return;
        gameObject.GetComponent<Renderer>().sharedMaterials[matindex].SetFloat("_LightFactor", lightFactor);
        if (ifTransparent)
        {
            for (int i = 0; i < materialList.Length; i++)
            {
               // Utility.SetMaterialRenderingMode(materialList[i], RenderingMode.Transparent);
                //DOTween.ToAlpha(() => materialList[i].color, x => materialList[i].color = x, AphValue, TweenTime);
            }
        }
    }

    public void OnMouseExit()
    {
        if (gameObject.GetComponent<Renderer>() == null) return;
        gameObject.GetComponent<Renderer>().sharedMaterials[matindex].SetFloat("_LightFactor", backFactor);
        if (ifTransparent)
        {
            for (int i = 0; i < materialList.Length; i++)
            {
              //  Utility.SetMaterialRenderingMode(materialList[i], RenderingMode.Opaque);
            }
        }
    }

    public void OnMouseDown()
    {
        Client.Instance.DoSetTankCellInfo(modelID, CellID);
        if (gameObject.GetComponent<Renderer>() == null) return;
        gameObject.GetComponent<Renderer>().sharedMaterials[matindex].SetFloat("_LightFactor", backFactor);       
        if (ifTransparent)
        {
            RenderingMode rendMode;
            for (int i = 0; i < materialList.Length; i++)
            {
                Utility.SetMaterialRenderingMode(materialList[i], (materialList[i].renderQueue < 3000) ? RenderingMode.Transparent : RenderingMode.Opaque);
            }
        }
    }
}
