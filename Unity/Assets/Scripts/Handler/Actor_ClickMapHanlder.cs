using System;
using UnityEngine;

namespace Model
{
    [MessageHandler((int)Opcode.Actor_ClickMap)]
    public class Actor_ClickMapHandler : AMHandler<Actor_ClickMap>
    {
        protected override void Run(Session session, Actor_ClickMap message)
        {
            Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            Vector3 dest = new Vector3(message.X / 1000f, 4, message.Z / 1000f);
            moveComponent.MoveToDest(dest, 5);
            moveComponent.Turn2D(dest - unit.Position);
        }
    }
}
