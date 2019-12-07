using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowItem : MonoBehaviour
{   
    public Text tTitle;
    public Text tContext;


    private Transform camTrans;
    public Transform CAMTrans
    {
        get { return camTrans; }
        set { camTrans = value; }
    }

    private int id;
    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string Title
    {
        set { tTitle.text = value; }
    }

    public string Desc
    {
        set { tContext.text = value; }
    }

    public void SetCamera(Transform Trans)
    {
        camTrans = Trans;
    }

    private float m_viewDistance = 300f;
    public float viewDistance
    {
        set { m_viewDistance = value; }
    }

    void Update()
    {
        Vector3 lookPos = camTrans.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);

        if (camTrans.GetComponent<RtsCamera>().Distance < m_viewDistance && camTrans.GetComponent<RtsCamera>().Distance > 50.0f)
        {
            float tempdis = camTrans.GetComponent<RtsCamera>().Distance / 1000.0f;
            transform.localScale = new Vector3(-tempdis, tempdis, tempdis);
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }
}
