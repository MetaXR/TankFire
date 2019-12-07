using System.Numerics;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public enum TankState
    {
        NORMAL = 1,
        FIRING,
        BE_ATTACKING,
        DEAD,
        WDXFIRE,
        BZXFIRE,
        FYXFIRE
    }

    [ObjectEvent]
    public class FireTankEvent : ObjectEvent<FireTank>, IAwake<TankState>, IAwake<long>
    {
        public void Awake(TankState tankType)
        {
            this.Get().Awake(tankType);
        }
        public void Awake(long id)
        {
            this.Get().Awake(id);
        }
    }

    public sealed class FireTank : Entity
    {
        public TankState State { get; set; }

        public long PlayerId { get; set; }

        [BsonIgnore]
        public Vector3 Position { get; set; }

        public float Life { get; set; }
        public float Loss { get; set; }

        public void Awake(TankState state)
        {
            this.State = state;
            Life = 100000f;
            Loss = 50f;
        }

        public void ResetState()
        {
            this.State = TankState.FIRING;
            Life = 100000f;
            Loss = 50f;
        }

        public void Awake(long id)
        {
            this.PlayerId = id;
        }

        public override void Dispose()
        {
            if (this.Id == 0)
            {
                return;
            }

            base.Dispose();
        }
    }
}