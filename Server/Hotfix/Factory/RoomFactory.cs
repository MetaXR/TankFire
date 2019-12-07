using Model;

namespace Hotfix
{
    public static class RoomFactory
    {
        public static Room Create(int level)
        {
            Room room = EntityFactory.Create<Room>();
            room.AddComponent<RoomJoinKeyComponent>();            
            //添加管理,避免被GC释放
            Game.Scene.GetComponent<RoomComponent>().Add(room);
            return room;
        }
    }
}
