
/// <summary>
/// session的心跳检测组件。记录最后一次心跳的时间。
/// </summary>
namespace Model
{
    [ObjectEvent]
    public class SessionHeatKickComponentEvent : ObjectEvent<SessionHeatKickComponent>, IStart
    {
        public void Start()
        {
            this.Get().Start();
        }
    }
    /**
     * 用于心跳检测。
     * 需要心跳检测的session会挂上这个组件，
     * **/
    public class SessionHeatKickComponent :Component
    {
        //最后更新时间。
        private long lastHeatkickTime;

        public void Start()
        {
            lastHeatkickTime = TimeHelper.Now();
        }

        /// <summary>
        /// 更新心跳时间。
        /// </summary>
        /// <param name="lastTime">最后的心跳时间</param>
        public void UpdateHeatkickTime(long lastTime)
        {
            this.lastHeatkickTime = lastTime;
        }

        /// <summary>
        /// 获取最后的心跳时间
        /// </summary>
        /// <returns></returns>
        public long GetLastHeatkickTime()
        {
            return this.lastHeatkickTime;
        }
    }//endclass
}
