namespace Model
{
    [ObjectEvent]
    public class PlayerEvent: ObjectEvent<Player>,IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

	public sealed class Player : Entity
	{
		public long UnitId { get; set; }
		public long UserID { get; set; }
        public string Account;

        public void Awake()
        {
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