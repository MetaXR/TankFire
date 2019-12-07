using System.Numerics;

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

		public Vector3 Position { get; set; }

        public long UserId { get; set; }
		
		public void Awake(long userid)
		{
			this.UnitType = UnitType.Hero;
            UserId = userid;
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