using System;
using System.Threading.Tasks;

namespace Model
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_FrieEquipStartHandler : AMActorHandler<Room, Actor_FrieEquipStart>
    {
        protected override async Task Run(Room room, Actor_FrieEquipStart message)
        { 
            await Task.CompletedTask;          
            room.Broadcast(message);
        }
    }
}
