using UnityEngine;

namespace Model
{
    [MessageHandler((int)Opcode.Actor_CreateFireTank)]
    public class Actor_CreateFireTankHandler : AMHandler<Actor_CreateFireTank>
    {
        protected override void Run(Session session, Actor_CreateFireTank message)
        {
            FireTank fireTank = EntityFactory.Create<FireTank,TankState>(message.TankInfo.state);
            fireTank.Id = message.TankInfo.UnitId;
            fireTank.BaseLife = message.TankInfo.Life;
            fireTank.Life = message.TankInfo.Life;
            fireTank.LossSpeed = message.TankInfo.Loss;
            fireTank.CTankState = message.TankInfo.state;
            FireTankComponent.Instance.MyFireTank = fireTank;          
        }
    }
}
