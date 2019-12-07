using Model;

namespace Hotfix
{
    [MessageHandler((ushort)Opcode.RoomKey)]
    public class RoomKeyHandler : AMHandler<RoomKey>
    {
        protected override async void Run(Session session, RoomKey message)
        {
            //发送加入房间消息
            Player localPlayer = ClientComponent.Instance.LocalPlayer;
            PlayerJoinRoom_RE playerJoinRoomRE = await SessionComponent.Instance.Session.Call<PlayerJoinRoom_RE>(new PlayerJoinRoom_RT()
            {
                Key = message.Key,
            });

            //加入房间失败退出到大厅
            if (playerJoinRoomRE.Error == ErrorCode.ERR_JoinRoomError)
            {
                Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UILobby);
                Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UIRoom);
                return;
            }
        }
    }
}
