using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    [ObjectEvent]
    public class KeepAliveComponentEvent : ObjectEvent<KeepAliveComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public sealed class KeepAliveComponent : Component
    {
        private long heatBeatTime = 10 * 1000;
        public bool IsConnected;
        public long LastHeatBeatTime;
       
        public void Awake()
        {
            this.IsConnected = true;
            LastHeatBeatTime = TimeHelper.Now();
            UpdateHeart();
        }

        public void UpdateLastHeatBeatTime(long beattime)
        {
            LastHeatBeatTime = beattime;
        }

        public async void UpdateHeart()
        {
            while (true)
            {
                try
                {
                    await Game.Scene.GetComponent<TimerComponent>().WaitAsync(heatBeatTime);
                    SessionComponent.Instance.Session.Send(new C2G_HeatKick());
                }
                catch (Exception e)
                {
                    Log.Warning(e.Message);
                }

                if (this.LastHeatBeatTime < TimeHelper.Now() - heatBeatTime)
                {
                    Log.Warning("心跳停止了：Client is DisConnected!--------");                   
                    this.IsConnected = false;
                    ReLoginGate();
                    //EnterMap();
                }
            }
        }

        private void ReLoginGate()
        {
            if (IsConnected)
            {
                return;
            }
            long key = SessionComponent.Instance.SessionKey;
            SessionComponent.Instance.Session.CallWithAction(new LoginGate_RT() { Key = key,reconnect=1 },
                (response) => LoginGateOk(response)
            );

        }

        private void LoginGateOk(AResponse response)
        {
            LoginGate_RE g2CLoginGate = (LoginGate_RE)response;
            if (g2CLoginGate.Error != ErrorCode.ERR_Success)
            {
                Log.Error(g2CLoginGate.Error.ToString());
                return;
            }
            Log.Info("重新登陆gate成功!");
            this.IsConnected = true;
            this.LastHeatBeatTime = TimeHelper.Now();
        }
    }
}
