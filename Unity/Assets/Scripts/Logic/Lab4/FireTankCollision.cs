using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirzaBeig.VFX;
using UnityEngine.UI;
using Model;
using System;
using System.Linq;

public class FireTankCollision : MonoBehaviour
{
    public int sum = 0;   
    public Dictionary<long, int> indexDic = new Dictionary<long, int>();
    public Dictionary<long, bool> boolDic = new Dictionary<long, bool>();
    public List<bool> boolList = new List<bool>();

    private void Start()
    {
        Client.Instance.setifCollision += SetIfAllCollision;
    }

    private void OnDestroy()
    {
        Client.Instance.setifCollision -= SetIfAllCollision;
    }

    public void SetIfAllCollision(bool boolVar, long unitId)
    {       
            if (!boolDic.ContainsKey(unitId))
            {
                boolDic.Add(unitId, boolVar);
            }
            else
            {
                boolDic[unitId] = boolVar;
            }
    }

    public void ResetAllCollsion()
    {
        indexDic.Clear();
    }

    void OnParticleCollision(GameObject other)
    {
        if (FireTankComponent.Instance.MyFireTank.CTankState == TankState.FIRING && FireTankComponent.Instance.MyFireTank.Life > 0.0f)
        {
            FireTankComponent.Instance.MyFireTank.CTankState = TankState.BE_ATTACKING;
            Client.Instance.DoTankBeAttacking();
        }
        if (FireTankComponent.Instance.MyFireTank.CTankState == TankState.BE_ATTACKING && FireTankComponent.Instance.MyFireTank.Life > 0.0f)
        {
            //必须3个人同时喷射，才会发送火量减小通知给服务器。
            //sum = 0;
            //foreach (var temp in indexDic)
            //{
            //    sum += temp.Value;
            //}
            //Debug.Log(sum+ "-----总和--sum---");
            //if (sum < 3)
            //{
            //    indexDic.Clear();
            //    sum = 0;
            //    return;
            //}
            boolList = boolDic.Values.ToList<bool>();
            if (boolList[0] && boolList[1] && boolList[2])
            {
                float lifeLoss = Time.deltaTime * FireTankComponent.Instance.MyFireTank.LossSpeed;
                SessionComponent.Instance.Session.Send(new Actor_FireTankLife()
                {
                    tankLife = FireTankComponent.Instance.MyFireTank.Life,
                    tankLoss = FireTankComponent.Instance.MyFireTank.LossSpeed,
                });
                Debug.Log("send 粒子碰上了----" + other.transform.parent.parent.gameObject.name);
            }           
            //indexDic.Clear();
            //sum = 0;
        }
        if (FireTankComponent.Instance.MyFireTank.Life <= 0.0f)
        {
            //Client.Instance.DoTankDead();
        }
    }   
}
