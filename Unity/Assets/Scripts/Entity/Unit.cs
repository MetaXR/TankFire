using UnityEngine;

namespace Model
{
	public enum UnitType
	{
		Hero,
		Npc
	}

    [ObjectEvent]
    public class UnitEvent : ObjectEvent<Unit>, IAwake<long>
    {
        public void Awake(long userid)
        {
            this.Get().Awake(userid);
        }
    }

    public sealed class Unit: Entity
	{
        public UnitType UnitType { get; private set; }

        public VInt3 IntPos;

		public GameObject GameObject;

        public long UserId { get; set; }

        public void Awake(long userid)
        {
            this.UnitType = UnitType.Hero;
            UserId = userid;
        }

        public Vector3 Position
		{
			get
			{
				return GameObject.transform.position;
			}
			set
			{
				GameObject.transform.position = value;
			}
		}

		public Quaternion Rotation
		{
			get
			{
				return GameObject.transform.rotation;
			}
			set
			{
				GameObject.transform.rotation = value;
			}
		}

		public override void Dispose()
		{
			if (this.Id == 0)
			{
				return;
			}
            UnityEngine.Object.Destroy(this.GameObject);
			base.Dispose();
		}
	}
}