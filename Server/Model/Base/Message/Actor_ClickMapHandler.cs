using System;
using System.Threading.Tasks;

namespace Model
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_ClickMapHandler:AMActorHandler<Unit,Actor_ClickMap>
    {
        protected override async Task Run(Unit unit, Actor_ClickMap message)
        {
            await Task.CompletedTask;            
            Actor_ClickMap clickMap = new Actor_ClickMap();
            clickMap = message;
            clickMap.Id = message.Id;
            //MessageHelper.Broadcast(clickMap);
        }
    }
}
