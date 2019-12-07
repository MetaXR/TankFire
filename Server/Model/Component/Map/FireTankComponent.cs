using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class FireTankComponent : Component
    {        
        public FireTank MFireTank { get; set; }     

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