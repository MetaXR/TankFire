using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenueUICtr : MonoBehaviour 
{
    public Button QuitBtn;

	void Start()
	{
        QuitBtn.onClick.AddListener(Quit);
	}

    void Quit()
    {
        Application.Quit();
    }

}
