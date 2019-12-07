using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Model;
public class FireEquip : MonoBehaviour
{
    public ParticleSystem particleWater;
    public ParticleSystem particlePaoMo;
    private ParticleSystem.EmissionModule particleWaterEmission;
    private ParticleSystem.EmissionModule particlePaoMoEmission;
    public  bool offOn=false;
    public bool waterOrPaoMo=true;
    public KeyCode upKey = KeyCode.Q;
    public KeyCode downKey = KeyCode.E;
    public float uDTilt=310;
    public KeyCode leftKey = KeyCode.F;
    public KeyCode rightKey = KeyCode.R;
    public float rLTilt;
    public float TiltSpeed = 30;   
    public long PlayerID;
	
	void Start()
	{
	    Client.Instance.onTankDead += this.EndParticle;
	    particleWaterEmission = particleWater.emission;
	    particlePaoMoEmission = particlePaoMo.emission;
        particleWater.trigger.SetCollider(0, GameObject.Find("Scene/FireTanK").GetComponent<BoxCollider>());      
        particleWater.gameObject.GetComponent<EquipCollision>().PlayerId = PlayerID;
        SetParticle();

        if (PlayerID == ClientComponent.Instance.LocalUnit.Id)
        {
            offOn = true;
        }
        else
        {
            offOn = false;
        }
    }

    void OnDestroy()
    {
        Client.Instance.onTankDead -= this.EndParticle;
    }

    void LateUpdate()
    {
        //Debug.Log(transform.localRotation.eulerAngles + "firequipSetting");
        if (offOn == false)
            return;
        if (Input.GetKey(upKey))
        {
            uDTilt += TiltSpeed * Time.deltaTime;
        }
        if (Input.GetKey(downKey))
        {
            uDTilt -= TiltSpeed * Time.deltaTime;
        }

        if (Input.GetKey(rightKey))
        {
            rLTilt += TiltSpeed * Time.deltaTime;
        }
        if (Input.GetKey(leftKey))
        {
            rLTilt -= TiltSpeed * Time.deltaTime;
        }
        //Utility.Clamp(uDTilt, -60, 0);
        //Mathf.Clamp(rLTilt, -30f, 30f);
        uDTilt = Utility.ClampEquip(uDTilt, -60, 0);
        //Debug.Log(uDTilt.ToString()+ "::::::uDTilt");
        //Debug.Log(rLTilt.ToString() + "::::::rLTilt");
        var rotation = Quaternion.Euler(uDTilt, 0, 0);
        transform.localRotation = rotation;
    }

    public void StartParticle()
    {
        SetParticle();     
        if (WDXFireMgr.Instance.CurEquipeType == EquipeType.WATER)
        {
            particleWater.gameObject.SetActive(true);           
            particleWater.Play();
        }
        else
        {
            particlePaoMo.gameObject.SetActive(true);
            //particleWater.GetComponent<EquipCollision>().PlayerId = PlayerID;
            particlePaoMo.Play();
        }
       //offOn = true;
    }

    public void SetUDTilt(float udTilt)
    {
        uDTilt=Utility.ClampEquip(uDTilt, -60, 0);
        var rotation = Quaternion.Euler(udTilt, 0, 0);
        transform.localRotation = rotation;
    }

    public void SetParticle()
    {
        var rotation = Quaternion.Euler(-60f, 0, 0);
        transform.rotation = rotation;
        if (WDXFireMgr.Instance.CurEquipeType == EquipeType.WATER)
        {
            int constValue = WDXFireMgr.Instance.CurFieItem.LiquidSetting["Value"];
            particleWaterEmission.rateOverTime = new ParticleSystem.MinMaxCurve(constValue);
        }
        else
        {
            int constValue = WDXFireMgr.Instance.CurFieItem.LiquidSetting["Value"];
            particlePaoMoEmission.rateOverTime = new ParticleSystem.MinMaxCurve(constValue);
        }
    }

    public void EndParticle()
    {
        particleWater.Stop();
        particlePaoMo.Stop();
        //offOn = false;
    }
}
