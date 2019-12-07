using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : SingletonMonoBehavior<Tips>
{

	// Use this for initialization
	void Start ()
    {
        Client.Instance.tipEvent += this.Show;
    }

    void OnDestory()
    {
        Client.Instance.tipEvent -= this.Show;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show()
    {
        Debug.Log("tips ------- ");
    }
}
