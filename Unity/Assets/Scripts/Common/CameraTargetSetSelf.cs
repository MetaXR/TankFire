using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetSetSelf : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
         Client.Instance.setCameraTargetEvent+=this.SetSelf ;
         this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSelf()
    {
        this.gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void OnDestroy()
    {
        Client.Instance.setCameraTargetEvent -= this.SetSelf;
    }

    
}
