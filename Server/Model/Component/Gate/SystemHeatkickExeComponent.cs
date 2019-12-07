/**
 * 系统执行心跳检测，挂在了这个组件的进程将开启心跳检测功能。
 * 
 * **/
namespace Model
{
    [ObjectEvent]
    public class SystemHeatkickExeComponentEvent : ObjectEvent<SystemHeatkickExeComponent> ,IAwake<long>
    {
        public void Awake(long a)
        {
            this.Get().StartExeHeatkick(a);
        }

         
    }

    public class SystemHeatkickExeComponent : Component
    {
        public long heartBeatTime = 10*1000;           //  心跳监测时间间隔。
        /// <summary>
        /// 开启心跳检测
        /// </summary>
        public async void StartExeHeatkick(long heartBeatTime) 
        {
            this.heartBeatTime = heartBeatTime;
            while (true)
            {
                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(10*1000);
                Log.Error("循环执行心跳检测");
                NetOuterComponent netOuter = Game.Scene.GetComponent<NetOuterComponent>();
                if (netOuter != null)
                {
                    foreach (Session session in netOuter.GetAllSessions())
                    {
                        SessionHeatKickComponent heatKick = session.GetComponent<SessionHeatKickComponent>();
                        if (heatKick != null)
                        {
                            long time = TimeHelper.Now();
                            long last = heatKick.GetLastHeatkickTime();
                            if (heatKick.GetLastHeatkickTime() < TimeHelper.Now() - heartBeatTime)
                            {
                                Log.Error($"session：{session.Id} 断开连接.");
                                netOuter.Remove(session.Id);
                                
                            }
                        }
                    }
                }
            }
        }
    }//classend
}
