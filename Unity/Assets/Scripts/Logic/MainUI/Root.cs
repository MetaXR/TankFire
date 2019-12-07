using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour 
{
    public void OnBtnClick()
    {
        Instantiate(Resources.Load(FilePath.uiPath + "Canvas_Menue") as GameObject);
        Destroy(this.gameObject);
    }
}
