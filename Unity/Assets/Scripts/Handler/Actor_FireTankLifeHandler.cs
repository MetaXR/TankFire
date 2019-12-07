using UnityEngine;

namespace Model
{
    [MessageHandler((int)Opcode.Actor_FireTankLife)]
    public class Actor_FireTankLifeHandler : AMHandler<Actor_FireTankLife>
    {
        protected override void Run(Session session, Actor_FireTankLife message)
        {
            FireTankComponent.Instance.MyFireTank.Life = message.tankLife;
            FireTankComponent.Instance.MyFireTank.SetFireTankLifePanel();
        }
    }
}
