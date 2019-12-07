using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix
{
    [MessageHandler((ushort)Opcode.GameStart)]
    public class GameStartHandler : AMHandler<GameStart>
    {
        protected override void Run(Session session ,GameStart message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.UIRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            uiRoom.GetComponent<UIRoomComponent>().RoomBg.SetActive(false);
            //初始化玩家UI
            foreach (var gamer in gamerComponent.GetAll())
            {
                GamerUIComponent gamerUI = gamer.GetComponent<GamerUIComponent>();
                gamerUI.GameStart();
            }          
        }
    }
}
