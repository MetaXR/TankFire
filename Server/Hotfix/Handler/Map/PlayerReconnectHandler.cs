using System.Threading.Tasks;
using Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class PlayerReconnectHandler : AMActorHandler<Room, PlayerReconnect>
    {
        protected override Task Run(Room entity, PlayerReconnect message)
        {
            Gamer gamer = entity.GetAll().Where(g => g.UserID == message.UserID).FirstOrDefault();
            if (gamer != null)
            {
                long pastId = gamer.Id;
                gamer.Id = message.PlayerID;
                gamer.isOffline = false;
                gamer.unit.Id = message.PlayerID;
                entity.Replace(pastId, gamer);                

                UnitGateComponent unitGateComponent = gamer.GetComponent<UnitGateComponent>();
                unitGateComponent.GateSessionId = message.GateSessionID;
                ActorProxy actorProxy = unitGateComponent.GetActorProxy();               

                entity.Broadcast(new GamerReenter() { PastID = pastId, NewID = gamer.Id });

                //发送房间玩家信息
                Gamer[] gamers = entity.GetAll();
                GamerInfo[] gamersInfo = new GamerInfo[gamers.Length];
                for (int i = 0; i < gamers.Length; i++)
                {
                    gamersInfo[i] = new GamerInfo();
                    gamersInfo[i].PlayerID = gamers[i].Id;
                    gamersInfo[i].UserID = gamers[i].UserID;
                    gamersInfo[i].IsReady = gamers[i].IsReady;
                    Unit u = gamers[i].unit;
                    gamersInfo[i].unitInfo = new UnitInfo() { UnitId = u.Id, X = 0, Z = 0 };
                }
                actorProxy.Send(new GamerEnter() { RoomID = entity.Id, GamersInfo = gamersInfo });
                
                //发送玩家手牌
                actorProxy.Send(new GameStart());
               
                //发送重连消息
                actorProxy.Send(new GamerReconnect()
                {
                    PlayerID = gamer.Id,
                    Multiples = 5,                   
                });              

                Log.Info($"玩家{gamer.Id}重连");
            }
            return Task.CompletedTask;
        }
    }
}
