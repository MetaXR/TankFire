using UnityEngine;
using MirzaBeig.VFX;

namespace Model
{   
    [ObjectEvent]
    public class FireTankEvent : ObjectEvent<FireTank>, IAwake<TankState>
    {
        public void Awake(TankState state)
        {
            this.Get().Awake(state);
        }
    }

    public sealed class FireTank : Entity
    {        
        public GameObject GameObject;
        public TankState CTankState;
        public TankLifeBar TankHeadBar;
        public ParticleSystems FXFire;
        public float BaseLife { get; set; }
        public float Life { get; set; }
        public float LossSpeed { get; set; }       
        public void Awake(TankState state)
        {
            this.GameObject=GameObject.Find("Scene/FireTanK");
            ReferenceCollector RC = this.GameObject.GetComponent<ReferenceCollector>();
            GameObject TankBar = RC.Get<GameObject>("PanelLifeBar");
            GameObject FXObj = RC.Get<GameObject>("FXFire");
            TankHeadBar = TankBar.GetComponent<TankLifeBar>();
            FXFire = FXObj.GetComponent<ParticleSystems>();
            TankBar.SetActive(true);
            FXObj.SetActive(true);
            TankHeadBar.lifeBar.size = 1f;
            TankHeadBar.lifeText.text = string.Format("<color=#F1FF00FF>{0:G}</color>/{1:G}", System.Convert.ToInt32(0), System.Convert.ToInt32(0));
            CTankState = state;            
            FXFire.play();
        }
        public void SetFireTankLifePanel()
        {
            TankHeadBar.lifeBar.size = FireTankComponent.Instance.MyFireTank.Life / FireTankComponent.Instance.MyFireTank.BaseLife;
            TankHeadBar.lifeText.text = string.Format("<color=#F1FF00FF>{0:G}</color>/{1:G}", System.Convert.ToInt32(FireTankComponent.Instance.MyFireTank.Life), System.Convert.ToInt32(FireTankComponent.Instance.MyFireTank.BaseLife));
        }

        public void EndData()
        {
            TankHeadBar.lifeBar.size = 1f;
            TankHeadBar.lifeText.text = string.Format("<color=#F1FF00FF>{0:G}</color>/{1:G}", System.Convert.ToInt32(FireTankComponent.Instance.MyFireTank.BaseLife), System.Convert.ToInt32(FireTankComponent.Instance.MyFireTank.BaseLife));
            FXFire.setPlaybackSpeed(0.5f);
            FXFire.stop();
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