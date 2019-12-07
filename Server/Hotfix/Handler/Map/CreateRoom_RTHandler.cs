using System;
using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Map)]
    public class CreateRoom_RTHandler : AMRpcHandler<CreateRoom_RT, CreateRoom_RE>
    {
        protected override async void Run(Session session, CreateRoom_RT message, Action<CreateRoom_RE> reply)
        {
            CreateRoom_RE response = new CreateRoom_RE();
            try
            {
                //创建房间
                Room room = RoomFactory.Create((int)RoomLevel.Lv100);
                await room.AddComponent<ActorComponent>().AddLocation();
                room.RoomFireType = message.type;
                Log.Info($"新建房间{room.Id}");

                response.RoomID = room.Id;
                response.type = room.RoomFireType;
                reply(response);

                FireTank fireTank = EntityFactory.Create<FireTank, TankState>(TankState.FIRING);
                await fireTank.AddComponent<ActorComponent>().AddLocation();
                room.RoomFireTank = fireTank;
                
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
