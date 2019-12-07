using System;
using System.Collections.Generic;
using UnityEngine;
using Model;

namespace Hotfix
{
    [MessageHandler((int)Opcode.Actor_FrieEquipStart)]
    public class Actor_FireEquipStartHandler : AMHandler<Actor_FrieEquipStart>
    {
        protected override void Run(Session session, Actor_FrieEquipStart message)
        {
            EquipInfo equipInfo = message.MyEquipInfo;
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.UIRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();           
            bool haveGun=false;
            if (gamerComponent.Get(equipInfo.unitID) != null)
            {
                Unit unit = gamerComponent.Get(equipInfo.unitID).unit;
                for (int i = 0; i < unit.GameObject.transform.childCount; i++)
                {
                    GameObject child = unit.GameObject.transform.GetChild(i).gameObject;
                    if (child.GetComponent<FireEquip>() != null)
                    {
                        
                        FireEquip fireEquip = child.GetComponent<FireEquip>();
                        fireEquip.waterOrPaoMo = true;                        
                        fireEquip.StartParticle();
                        haveGun = true;                        
                        //if (unit.Id == ClientComponent.Instance.LocalUnit.Id)
                        //{
                        //    fireEquip.offOn = true;
                        //}
                        //else
                        //{
                        //    fireEquip.offOn = false;
                        //}
                    }
                }

                if (!haveGun)
                {
                    GameObject gun = GameObject.Instantiate(Resources.Load(FilePath.fxPath + "equipWaterGun") as GameObject) as GameObject;
                    gun.transform.parent = unit.GameObject.transform;
                    gun.transform.localPosition = new Vector3(0.3f, 0, 0.6f);
                    if (gun.GetComponent<FireEquip>() != null)
                    {
                        FireEquip fireEquip = gun.GetComponent<FireEquip>();
                        fireEquip.PlayerID = equipInfo.unitID;
                        fireEquip.waterOrPaoMo = true;
                        fireEquip.StartParticle();
                        haveGun = true;
                        //Log.Debug("Actor_FireEquipStartHandler----Unit.Id"+unit.Id);
                        //Log.Debug("Actor_FireEquipStartHandler----equipInfo.unitID" + equipInfo.unitID);
                        //Log.Debug("ClientComponent.Instance.LocalUnit.Id" + ClientComponent.Instance.LocalUnit.Id+ "--" + ClientComponent.Instance.LocalUnit.UserId+
                        //    "ClientComponent.Instance.LocalPlayer" + ClientComponent.Instance.LocalPlayer.Id+"--"+ ClientComponent.Instance.LocalPlayer.UserID+"--"+ ClientComponent.Instance.LocalPlayer.UnitId);
                        //if (unit.Id == ClientComponent.Instance.LocalUnit.Id)
                        //{
                        //    fireEquip.offOn = true;
                        //}
                        //else
                        //{
                        //    fireEquip.offOn = false;
                        //}
                    }
                    else
                    {
                        Log.Debug("the gun form person is error!!!");
                        haveGun = false;
                    }
                }
                else
                {
                    for (int i = 0; i < unit.GameObject.transform.childCount; i++)
                    {
                        GameObject child = unit.GameObject.transform.GetChild(i).gameObject;
                        if (child.GetComponent<FireEquip>() != null)
                        {
                            FireEquip fireEquip = child.GetComponent<FireEquip>();
                            fireEquip.waterOrPaoMo = true;
                            fireEquip.StartParticle();
                            haveGun = true;
                            //if (unit.Id == ClientComponent.Instance.LocalUnit.Id)
                            //{
                            //    fireEquip.offOn = true;
                            //}
                            //else
                            //{
                            //    fireEquip.offOn = false;
                            //}
                        }
                    }
                }
            }            
        }
    }
}
