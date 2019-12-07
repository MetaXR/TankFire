using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Match)]
    public class GamerQuitRoomHandler : AMHandler<GamerQuitRoom>
    {
        protected override void Run(Session session, GamerQuitRoom message)
        {
            //同步房间玩家
            RoomManagerComponent roomManager = Game.Scene.GetComponent<RoomManagerComponent>();
            Room room = roomManager.Get(message.RoomID);
            Gamer gamer = room.Get(message.PlayerID);
            room.Remove(message.PlayerID);
            Game.Scene.GetComponent<MatchComponent>().Playing.Remove(gamer.UserID);
            Game.Scene.GetComponent<MatcherComponent>().Remove(message.PlayerID);
            Log.Info($"玩家{message.PlayerID}退出被移除匹配队列");
            Log.Info($"匹配服务器同步：玩家{message.PlayerID}离开房间{room.Id}");
            if (room.Count == 0)
            {
                roomManager.Recycle(room.Id);
                Log.Info($"回收房间{room.Id}");
            }
        }
    }
}
