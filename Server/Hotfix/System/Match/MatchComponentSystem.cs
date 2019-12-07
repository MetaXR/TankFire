using Model;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace Hotfix
{
    [ObjectEvent]
    public class MatchComponentEvent : ObjectEvent<MatchComponent>, IUpdate
    {
        public void Update()
        {
            this.Get().Update();
        }
    }

    public static class MatchComponentSystem
    {
        public static void Update(this MatchComponent self)
        {
            while (true)
            {
                MatcherComponent matcherComponent = Game.Scene.GetComponent<MatcherComponent>();
                List<Matcher> matchers = new List<Matcher>(matcherComponent.GetAll());             
                RoomManagerComponent roomManager = Game.Scene.GetComponent<RoomManagerComponent>();
                Room room = roomManager.GetReadyRoom();

                if (matchers.Count == 0)
                {
                    //当没有匹配玩家时直接结束
                    break;
                }

                if (room == null && matchers.Count >= 3)
                {
                    //当还有一桌匹配玩家且没有可加入房间时使用空房间
                    Log.Info("   if (room == null && matchers.Count >= 3)1--  " + matchers.Count().ToString());
                    room = roomManager.GetIdleRoom();
                   
                }

                if (room != null)
                {
                    Log.Info("   if (room != null)1--  " + room.RoomFireType.ToString() + matchers.Count().ToString());
                    //当有房间时匹配玩家直接加入
                    while (matchers.Count > 0 && room.Count < 3 && matchers.Where(a => a.roomType == room.RoomFireType).Count() > 0)
                    {
                        IEnumerable<Matcher> matcherEnum = matchers.TakeWhile<Matcher>(a => a.roomType == room.RoomFireType);
                        foreach (var matcher in matcherEnum)
                        {
                            JoinRoom(self, room, matcher);
                            //matchers.Remove(matcher);
                        }                        
                        //JoinRoom(self, room, matchers.Dequeue());
                    }
                }
                else if (matchers.Count >= 3)
                {
                    Log.Info("if (matchers.Count >= 3) --  "+matchers.Count().ToString());
                    //当还有一桌匹配玩家且没有空房间时创建新房间
                    RoomType temptype = RoomType.WENDING;
                    for (int i = 1; i < 6; i++)
                    {
                        temptype = (RoomType)i;
                        Log.Info("Create Room TempType" + temptype.ToString());
                        if (matchers.Where(a => a.roomType == temptype).Count() >= 3)
                        {
                            Log.Info("Create Room success" + temptype.ToString());
                            CreateRoom(self, temptype);
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    break;
                }

                //移除匹配成功玩家
                while (self.MatchSuccessQueue.Count > 0)
                {
                    matcherComponent.Remove(self.MatchSuccessQueue.Dequeue().PlayerID);
                }
            }
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="self"></param>
        public static async void CreateRoom(this MatchComponent self,RoomType roomType)
        {
            if (self.CreateRoomLock)
            {
                return;
            }

            //消息加锁，避免因为延迟重复发多次创建消息
            self.CreateRoomLock = true;

            //发送创建房间消息
            string mapAddress = Game.Scene.GetComponent<RealmMapAddressComponent>().GetAddress().GetComponent<InnerConfig>().Address;
            IPEndPoint iPEndPoint = NetworkHelper.ToIPEndPoint(mapAddress);
            Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Create(iPEndPoint);
            CreateRoom_RE createRoomRE = await mapSession.Call<CreateRoom_RE>(new CreateRoom_RT() { Level = RoomLevel.Lv100 ,type=roomType});

            Room room = EntityFactory.CreateWithId<Room>(createRoomRE.RoomID);
            room.RoomFireType = createRoomRE.type;
            Log.Warning(room.Id.ToString() + "MatchComponentSystem create room in match server!!");
            Game.Scene.GetComponent<RoomManagerComponent>().Add(room);

            //解锁
            self.CreateRoomLock = false;
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="room"></param>
        /// <param name="matcher"></param>
        public static async void JoinRoom(this MatchComponent self, Room room, Matcher matcher)
        {
            //玩家加入房间，移除匹配队列
            self.Playing.Add(matcher.UserID, room.Id);
            self.MatchSuccessQueue.Enqueue(matcher);
            room.Add(EntityFactory.CreateWithId<Gamer, long>(matcher.PlayerID, matcher.UserID));

            //发送获取加入房间密匙消息
            ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(room.Id);
            GetJoinRoomKey_RE playerJoinRoomRE = await actorProxy.Call<GetJoinRoomKey_RE>(new GetJoinRoomKey_RT()
            {
                PlayerID = matcher.PlayerID,
                UserID = matcher.UserID,
                GateSeesionID = matcher.GateSessionID
            });

            //发送匹配成功消息                  
            string gateAddress = Game.Scene.GetComponent<StartConfigComponent>().Get(matcher.GateAppID).GetComponent<InnerConfig>().Address;
            Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Create(NetworkHelper.ToIPEndPoint(gateAddress));
            gateSession.Send(new MatchSuccess() { PlayerID = matcher.PlayerID, RoomID = room.Id, Key = playerJoinRoomRE.Key });
        }
    }
}
