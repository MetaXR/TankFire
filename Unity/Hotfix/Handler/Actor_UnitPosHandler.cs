using Model;
using UnityEngine;

namespace Hotfix
{
    [MessageHandler((ushort)Opcode.Actor_UnitPos)]
    public class Actor_UnitPosHandler : AMHandler<Actor_UnitPos>
    {
        protected override void Run(Session session, Actor_UnitPos message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.UIRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.UnitId);
            Vector3 dest = new Vector3(message.X/1000f, 0, message.Z/1000f);
            MoveComponent moveComponent = gamer.unit.GetComponent<MoveComponent>();
            moveComponent.MoveToDest(dest, 0.3f);
            moveComponent.Turn(dest, 0.1f);
            for (int i = 0; i < gamer.unit.GameObject.transform.childCount; i++)
            {
                GameObject child = gamer.unit.GameObject.transform.GetChild(i).gameObject;
                if (child.GetComponent<FireEquip>() != null)
                {

                    FireEquip fireEquip = child.GetComponent<FireEquip>();                   
                    fireEquip.SetUDTilt(message.udtilt);
                }
            }

        }
    }
}