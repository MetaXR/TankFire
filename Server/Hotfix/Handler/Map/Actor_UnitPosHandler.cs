using System.Threading.Tasks;
using Model;

namespace Hotfix
{   
    [ActorMessageHandler(AppType.Map)]
    public class Actor_UnitPosHandler : AMActorHandler<Room, Actor_UnitPos>
    {
        protected override Task Run(Room entity, Actor_UnitPos message)
        {            
            entity.Broadcast(message);           
            return Task.CompletedTask;
        }
    }
}
