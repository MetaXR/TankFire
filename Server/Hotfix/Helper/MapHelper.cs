using Model;
using System.Net;

namespace Hotfix
{
    public static class MapHelper
    {
        /// <summary>
        /// 发送消息给匹配服务器
        /// </summary>
        /// <param name="message"></param>
        public static void SendMessage(AMessage message)
        {
            string matchAddress = Game.Scene.GetComponent<StartConfigComponent>().MatchConfig.GetComponent<InnerConfig>().Address;
            IPEndPoint iPEndPoint = NetworkHelper.ToIPEndPoint(matchAddress);
            Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Create(iPEndPoint);
            matchSession.Send(message);
        }
    }
}
