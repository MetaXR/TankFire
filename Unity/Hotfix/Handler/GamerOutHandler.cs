using Model;

namespace Hotfix
{
    [MessageHandler((ushort)Opcode.GamerOut)]
    public class GamerOutHandler : AMHandler<GamerOut>
    {
        protected override void Run(Session session, GamerOut message)
        {
            //移除玩家
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.UIRoom);
            uiRoom.GetComponent<GamerComponent>().Remove(message.PlayerID);           
            Log.Warning("GamerOutHandler-------" + message.PlayerID);
        }
    }
}
