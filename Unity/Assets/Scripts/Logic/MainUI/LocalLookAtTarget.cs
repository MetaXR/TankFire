using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalLookAtTarget : MonoBehaviour
{

    public GameObject TargetGameObject;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(TargetGameObject.transform,Vector3.up);
    }
}
