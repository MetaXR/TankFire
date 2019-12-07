using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [MessageHandler((int)Opcode.G2C_HeatKick)]
    public class G2C_HeatKickHandler:AMHandler<G2C_HeatKick>
    {
        protected override void Run(Session session, G2C_HeatKick message)
        {
            Game.Scene.GetComponent<KeepAliveComponent>().UpdateLastHeatBeatTime(TimeHelper.Now());
        }
    }
}
