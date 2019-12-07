using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [MessageHandler(AppType.Gate)]
    public class C2R_HeartMessageHander : AMRpcHandler<C2R_HeartMessage, R2C_HeartMessage>
    {
        protected override async void Run(Session session, C2R_HeartMessage message, Action<R2C_HeartMessage> reply)
        {
            R2C_HeartMessage response = new R2C_HeartMessage();
            try
            {
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
