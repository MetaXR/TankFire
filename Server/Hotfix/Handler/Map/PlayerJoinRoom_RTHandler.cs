using System;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class PlayerJoinRoom_RTHandler : AMActorRpcHandler<Room, PlayerJoinRoom_RT, PlayerJoinRoom_RE>
    {
        protected override Task Run(Room room, PlayerJoinRoom_RT message, Action<PlayerJoinRoom_RE> reply)
        {
            PlayerJoinRoom_RE response = new PlayerJoinRoom_RE();

            try
            {
                Gamer gamer = room.GetComponent<RoomJoinKeyComponent>().Get(message.Key);

                //验证密匙
                if (gamer != null)
                {
                    room.Add(gamer);

                    //广播消息
                    Gamer[] gamers = room.GetAll();
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
                    room.Broadcast(new GamerEnter() { RoomID = room.Id, GamersInfo = gamersInfo });
                    Log.Info($"玩家{gamer.Id}进入房间");
                }
                else
                {
                    Log.Info($"玩家进入房间验证失败，密匙：{message.Key}");
                    response.Error = ErrorCode.ERR_JoinRoomError;
                }

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
            return Task.CompletedTask;
        }
    }
}
