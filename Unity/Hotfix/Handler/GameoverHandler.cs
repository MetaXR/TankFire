using Model;
using UnityEngine;

namespace Hotfix
{
    [MessageHandler((ushort)Opcode.Gameover)]
    public class GameoverHandler : AMHandler<Gameover>
    {
        protected override void Run(Session session, Gameover message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.UIRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            if (uiRoom.Get("EndPanel(Clone)") == null)
            {
                UI uiEndPanel = UIEndFactory.Create(Hotfix.Scene, UIType.EndPanel, uiRoom);
                UIEndComponent endComponent = uiEndPanel.GetComponent<UIEndComponent>();
                uiRoom.Add(uiEndPanel);               
                foreach (var gamer in gamerComponent.GetAll())
                {
                    gamer.GetComponent<GamerUIComponent>().UpdateInfo();
                }
                Client.Instance.DoTankDead();
            }
            else
            {
                return;
            }
           
        }
    }
}
