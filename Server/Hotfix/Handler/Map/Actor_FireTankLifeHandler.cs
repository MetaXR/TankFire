using System;
using System.Threading.Tasks;
using System.Linq;
using Model;
using System.Collections.Generic;


namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_FireTankLifeHander : AMActorHandler<Room, Actor_FireTankLife>
    {
        protected override async Task Run(Room room, Actor_FireTankLife message)
        {          
            await Task.CompletedTask;            
            room.RoomFireTank.Life -= message.tankLoss;           
            if (room.RoomFireTank.Life <= 0)
            {
                room.RoomFireTank.Life = 0;               
            }
            message.tankLife = room.RoomFireTank.Life;
            room.Broadcast(message);

            if (message.tankLife <= 0)
            {      
                //保证结束逻辑只执行一次。
                if (room.State == RoomState.Game)
                {
                    //游戏结束逻辑
                    room.Broadcast(new Gameover());
                    //同步匹配服务器结束游戏
                    room.State = RoomState.Ready;
                    MapHelper.SendMessage(new SyncRoomState() { RoomID = room.Id, State = room.State });

                    //Gamer[] gamers = room.GetAll();
                    //for (int i = 0; i < gamers.Length; i++)
                    //{
                    //    //bool isKickOut = gamers[i].isOffline;
                    //    //踢出玩家
                    //    //if (isKickOut)
                    //    ////{
                    //    //ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(gamers[i].Id);
                    //    //actorProxy.Send(new PlayerQuit() { PlayerID = gamers[i].Id });
                    //    //gamers[i].Id = 0;
                    //    //}

                    //    //Player player = Game.Scene.GetComponent<PlayerComponent>().Get(gamers[i].Id);
                    //    //if (player != null)
                    //    //{
                    //    //    //向Actor对象发送退出消息
                    //    //    ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(player.ActorID);
                    //    //    actorProxy.Send(new PlayerQuit() { PlayerID = player.Id });
                    //    //    player.ActorID = 0;
                    //    //    Log.Warning("Actor_FireTankLifeHandler--------PlayerQuit send success!!!");
                    //    //}                        
                    //}
                    //???????room.Broadcast(new PlayerQuit());
                }
            }
            
        }
    }
}