using System;
using UnityEngine;
using UnityEngine.UI;
using MirzaBeig.VFX;
public class PaoMoCtr : MonoBehaviour
{
    public Button menueBtn;
    public Button PaoMoBtn;
    public Button UpPMBtn;
    public Button DownPMBtn;
    public ParticleSystems ParticleUp;
    public ParticleSystems ParticleDown;
    public GameObject WintipObj;
	// Use this for initialization
	void Start () 
    {
        menueBtn.onClick.AddListener(delegate() { SceneLoading.GetInstance().ChangeToScene(SceneName.Main); });
        PaoMoBtn.onClick.AddListener(OverLookClick);
        UpPMBtn.onClick.AddListener(UpPMClick);
        DownPMBtn.onClick.AddListener(DownPMClick);
        ParticleDown.stop();
        ParticleUp.stop();
	}
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void OverLookClick()
    {
        //WintipObj.SetActive(true);
        ParticleDown.stopImediate();
        ParticleUp.stopImediate();
    }

    public void DownPMClick()
    {
        //WintipObj.SetActive(false);
        ParticleDown.play();
        ParticleUp.stopImediate();
    }

    public void UpPMClick()
    {
        //WintipObj.SetActive(false);
        ParticleDown.stopImediate();
        ParticleUp.play();
    }
}
