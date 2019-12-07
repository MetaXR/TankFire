using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [MessageHandler(AppType.Gate)]
    class C2G_HeatKickHandler:AMHandler<C2G_HeatKick>
    {
        protected override void Run(Session session,C2G_HeatKick message)
        {
            SessionHeatKickComponent heatkick = session.GetComponent<SessionHeatKickComponent>();
            if (heatkick != null)
            {
                heatkick.UpdateHeatkickTime(TimeHelper.Now());
            }
            session.Send(new G2C_HeatKick());
        }
    }
}
