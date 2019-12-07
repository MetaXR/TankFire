using System.Linq;
using System.Threading.Tasks;
using Model;
using System.Collections.Generic;
using System;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class PlayerReady_RTHandler : AMActorHandler<Room, PlayerReady>
    {
        protected override Task Run(Room entity, PlayerReady message)
        {
            Gamer gamer = entity.Get(message.PlayerID);

            if (gamer != null)
            {
                gamer.IsReady = true;

                Gamer[] gamers = entity.GetAll();

                //转发玩家准备消息
                entity.Broadcast(message);
                Log.Info($"玩家{gamer.Id}准备");               

                //房间内有3名玩家且全部准备则开始游戏
                if (entity.Count == 3 && gamers.Where(g => g.IsReady).Count() == 3)
                {
                    //同步匹配服务器开始游戏
                    entity.State = RoomState.Game;
                    MapHelper.SendMessage(new SyncRoomState() { RoomID = entity.Id, State = entity.State });
                    //创建fireTanK
                    Actor_CreateFireTank actorCreateFireTank = new Actor_CreateFireTank();
                    FireTankInfo tankInfo = new FireTankInfo();
                    tankInfo.UnitId = entity.RoomFireTank.Id;
                    tankInfo.Life = entity.RoomFireTank.Life;
                    tankInfo.Loss = entity.RoomFireTank.Loss;
                    tankInfo.state = entity.RoomFireTank.State;
                    actorCreateFireTank.TankInfo = tankInfo;
                    entity.Broadcast(actorCreateFireTank);

                    foreach (var _gamer in gamers)
                    {
                        ActorProxy actorProxy = _gamer.GetComponent<UnitGateComponent>().GetActorProxy();
                        actorProxy.Send(new GameStart(){});
                    }                   
                    
                    Log.Info($"房间{entity.Id}开始游戏");
                }
            }

            return Task.CompletedTask;
        }
    }
}
