using Model;
using UnityEngine;

namespace Hotfix
{
    [MessageHandler((ushort)Opcode.GamerEnter)]
    public class GamerEnterHandler : AMHandler<GamerEnter>
    {
        protected override void Run(Session session, GamerEnter message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.UIRoom);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            //隐藏匹配提示
            GameObject matchPrompt = uiRoom.GameObject.Get<GameObject>("MatchPrompt");
            if (matchPrompt.activeSelf)
            {
                matchPrompt.SetActive(false);
                uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(true);
            }          
            //添加未显示玩家
            for (int i = 0; i < message.GamersInfo.Length; i++)
            {
                GamerInfo info = message.GamersInfo[i];
                if (gamerComponent.Get(info.PlayerID) == null)
                {
                    Gamer gamer = EntityFactory.CreateWithId<Gamer, long>(info.PlayerID, info.UserID);
                    gamer.IsReady = info.IsReady;
                    Unit unit = UnitFactory.Create(info.PlayerID, info.UserID);
                    gamer.unit = unit;                   
                    gamer.AddComponent<GamerUIComponent, UI>(uiRoom);
                    gamerComponent.Add(gamer);
                }             
            }       
        }
    }
}
