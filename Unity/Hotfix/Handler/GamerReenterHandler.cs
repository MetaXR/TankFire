using Model;

namespace Hotfix
{
    [MessageHandler((ushort)Opcode.GamerReenter)]
    public class GamerReenterHandler : AMHandler<GamerReenter>
    {
        protected override void Run(Session session, GamerReenter message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.UIRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.PastID);
            if(gamer != null)
            {
                gamer.Id = message.NewID;
                gamer.unit.Id = message.NewID;
                gamerComponent.Replace(message.PastID, gamer);
            }
        }
    }
}
