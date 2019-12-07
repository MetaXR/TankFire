using System;
using UnityEngine;
using UnityEngine.UI;
using Model;
using UnityEngine.SceneManagement;

namespace Hotfix
{
    [ObjectEvent]
    public class UILobbyComponentEvent : ObjectEvent<UILobbyComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class UILobbyComponent : Component
    {
        public void Awake()
        {
            Init();
        }

        public async void OnStartMatch()
        {
            try
            {
                //发送开始匹配消息
                long playerId = ClientComponent.Instance.LocalPlayer.Id;
                StartMatch_RT startMatchRT = new StartMatch_RT() { PlayerID = playerId, Level = RoomLevel.Lv100 ,roomType=WDXFireMgr.Instance.roomType};
                StartMatch_RE startMatchRE = await SessionComponent.Instance.Session.Call<StartMatch_RE>(startMatchRT);

                if (startMatchRE.Error == ErrorCode.ERR_StartMatchError)
                {
                    Log.Error($"匹配失败:{MongoHelper.ToJson(startMatchRT)}");
                    return;
                }

                if(startMatchRE.Error == ErrorCode.ERR_UserMoneyLessError)
                {
                    Log.Error("余额不足");
                    return;
                }

                //切换到房间界面
                Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UIRoom);
                Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UILobby);
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
            }
        }

        private void OnBackMenue()
        {
            Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UILobby);
            SceneLoading.GetInstance().ChangeToScene(SceneName.Main);
        }

        private async void Init()
        {
            ReferenceCollector rc = this.GetEntity<UI>().GameObject.GetComponent<ReferenceCollector>();

            //添加事件
            rc.Get<GameObject>("StartMatch").GetComponent<Button>().onClick.Add(OnStartMatch);
            rc.Get<GameObject>("MenueBtn").GetComponent<Button>().onClick.Add(OnBackMenue); 
            rc.Get<GameObject>("Title").GetComponent<Text>().text = $"{SceneLoading.GetInstance().SceneNameDic[SceneManager.GetActiveScene().name]}";

            //获取玩家数据
            long userId = ClientComponent.Instance.LocalPlayer.UserID;
            GetUserInfo_RT getUserInfo_RT = new GetUserInfo_RT() { UserID = userId };
            GetUserInfo_RE getUserInfoRE = await SessionComponent.Instance.Session.Call<GetUserInfo_RE>(getUserInfo_RT);

            if (getUserInfoRE.Error == ErrorCode.ERR_QueryUserInfoError)
            {
                Log.Error("获取玩家信息异常 << " + MongoHelper.ToJson(getUserInfo_RT));
                return;
            }
            else
            {
                rc.Get<GameObject>("NickName").GetComponent<Text>().text = $"{getUserInfoRE.NickName}";
                rc.Get<GameObject>("Money").GetComponent<Text>().text = getUserInfoRE.Money.ToString();
            }
        }
    }
}
