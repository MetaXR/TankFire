using System.Collections.Generic;
using System.Linq;

namespace Model
{
    [ObjectEvent]
    public class FireTankComponentEvent : ObjectEvent<FireTankComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class FireTankComponent : Component
    {
        public static FireTankComponent Instance { get; private set; }

        public FireTank MyFireTank;       

        public void Awake()
        {
            Instance = this;
        }

        public void SetLifePanel()
        {

        }

        public void SendFireTankLifeLoss(float timedeltaTime)
        {
           
        }
       
        public override void Dispose()
        {
            if (this.Id == 0)
            {
                return;
            }
            base.Dispose(); 
            Instance = null;
        }
    }
}