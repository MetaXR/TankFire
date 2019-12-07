using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObj : MonoBehaviour {

    public float lightFactor = 3f;
    public float backFactor = 1f;
    public int matindex;
    public int layerIndex;
    //public string ModelName;
    public float averageDistance;
    //public GameObject[] sandongs;
	// Use this for initialization
    public int modelID;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDetilData(string ModelName)
    {
       //DetailGameObject.GetComponent<DetailCameraCtr>().SetAverageDistance(averageDistance);
       //DetailGameObject.GetComponent<DetailCameraCtr>().Start();
    }
    public void OnMouseEnter()
    {
        //gameObject.GetComponent<Renderer>().materials[matindex].SetFloat("_LightFactor", lightFactor);
        gameObject.GetComponent<Renderer>().sharedMaterials[matindex].SetFloat("_LightFactor", lightFactor);
    }

    public void OnMouseExit()
    {
        //gameObject.GetComponent<Renderer>().materials[matindex].SetFloat("_LightFactor", 1.1f);
        gameObject.GetComponent<Renderer>().sharedMaterials[matindex].SetFloat("_LightFactor", backFactor);
    }

    public void OnMouseDown()
    {
        //gameObject.GetComponent<Renderer>().materials[matindex].SetFloat("_LightFactor", 1.1f);
        gameObject.GetComponent<Renderer>().sharedMaterials[matindex].SetFloat("_LightFactor", backFactor);
    }

}
