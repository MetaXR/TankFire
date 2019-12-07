using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;
using System.IO;
namespace Hotfix
{
    [ObjectEvent]
    public class UILoginComponentEvent : ObjectEvent<UILoginComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class UILoginComponent : Component
    {
        private InputField account;
        private InputField password;
        private bool isRegistering;
        private bool isLogining;
        private Text prompt;
        private Text HotfixPrompt;
       
        public void Awake()
        {
            ReferenceCollector rc = this.GetEntity<UI>().GameObject.GetComponent<ReferenceCollector>();
            rc.Get<GameObject>("LoginBtn").GetComponent<Button>().onClick.Add(OnLogin);
            rc.Get<GameObject>("RegisterBtn").GetComponent<Button>().onClick.Add(OnRegister);
            this.account = rc.Get<GameObject>("Account").GetComponent<InputField>();
            this.password= rc.Get<GameObject>("Password").GetComponent<InputField>();
            this.prompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();
            this.HotfixPrompt = rc.Get<GameObject>("HotfixPrompt").GetComponent<Text>();          
        }
       
        private void OnLogin()
		{
			Session session = null;            
            IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalDate.GetInstance().InitGlobalProto.RealmAddress);
			session = Game.Scene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
			string text = this.account.GetComponent<InputField>().text;               
            session.CallWithAction(new Login_RT() { Account = account.text, Password = password.text }, (response) => LoginOK(session, response));
        }

		private void LoginOK(Session loginSession, AResponse response)
		{
			loginSession.Dispose();

            Login_RE r2CLogin = (Login_RE) response;
			if (r2CLogin.Error != ErrorCode.ERR_Success)
			{
				Log.Error(r2CLogin.Error.ToString());
				return;
			}

			IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalDate.GetInstance().InitGlobalProto.GateAddress);
			Session gateSession = Game.Scene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
			Game.Scene.AddComponent<SessionComponent>().Session = gateSession;
            SessionComponent.Instance.SessionKey = r2CLogin.Key;
			SessionComponent.Instance.Session.CallWithAction(new LoginGate_RT() { Key = r2CLogin.Key },
				(response2)=>LoginGateOk(response2)
			);
		}

		private void LoginGateOk(AResponse response)
		{
            LoginGate_RE g2CLoginGate = (LoginGate_RE) response;
			if (g2CLoginGate.Error != ErrorCode.ERR_Success)
			{
				Log.Error(g2CLoginGate.Error.ToString());
				return;
			}

			Log.Info("登陆gate成功!");
            
            // 创建Player
            Player player = Model.EntityFactory.CreateWithId<Player>(g2CLoginGate.PlayerID);
			PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
			playerComponent.MyPlayer = player;

            //保存本地玩家
            //Player player = Model.EntityFactory.CreateWithId<Player>(g2CLoginGate.PlayerID);
            player.UserID = g2CLoginGate.UserID;
            ClientComponent.Instance.LocalPlayer = player;

            //Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UILobby);
			Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UILogin);
            SceneLoading.GetInstance().ChangeToScene(SceneName.Main);
		}

        private async void OnRegister()
        {
            if (isRegistering)
            {
                return;
            }

            isRegistering = true;
            Session session = null;
            prompt.text = "";
            try
            {
                //创建登录服务器连接
                IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalDate.GetInstance().InitGlobalProto.RealmAddress);
                session = Hotfix.Scene.ModelScene.GetComponent<NetOuterComponent>().Create(connetEndPoint);

                //发送注册请求
                prompt.text = "正在注册中....";
                Register_RE registerRE = await session.Call<Register_RE>(new Register_RT() { Account = account.text, Password = password.text });
                prompt.text = "";
                if (registerRE.Error == ErrorCode.ERR_AccountAlreadyRegister)
                {
                    prompt.text = "注册失败，账号已被注册";
                    account.text = "";
                    password.text = "";
                    session.Dispose();
                    isRegistering = false;
                    return;
                }

                //注册成功自动登录
                OnLogin();
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
            }
            finally
            {
                session?.Dispose();
                isRegistering = false;
            }
        }    
	}
}
