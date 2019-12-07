using Model;

namespace Hotfix
{
    [MessageHandler((ushort)Opcode.GamerMoneyLess)]
    public class GamerMoneyLessHandler : AMHandler<GamerMoneyLess>
    {
        protected override void Run(Session session, GamerMoneyLess message)
        {
            //发送退出消息
            long playerId = ClientComponent.Instance.LocalPlayer.Id;
            if (message.PlayerID == playerId)
            {
                SessionComponent.Instance.Session.Send(new Quit() { PlayerID = playerId });
            }

            //切换到大厅界面
            Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UILobby);
            Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UIRoom);
        }
    }
}
