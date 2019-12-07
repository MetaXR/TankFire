using Model;

namespace Hotfix
{
    [ObjectEvent]
    public class GamerEvent : ObjectEvent<Gamer>, IAwake<long>
    {
        public void Awake(long id)
        {
            this.Get().Awake(id);
        }
    }

    public sealed class Gamer : Entity
    {
        public long UserID { get; private set; }
        public bool IsReady { get; set; }
        public Unit unit { get; set; }

       

        public void Awake(long id)
        {
            this.UserID = id;
        }

        public override void Dispose()
        {
            if (this.Id == 0)
            {
                return;
            }
            unit?.Dispose();
            base.Dispose();
        }
    }
}
