using System;
using UnityEngine;
using UnityEngine.UI;

namespace Model
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
		private Text text;

		public void Awake()
		{
			ReferenceCollector rc = this.GetEntity<UI>().GameObject.GetComponent<ReferenceCollector>();			
            rc.Get<GameObject>("MatchBtn").GetComponent<Button>().onClick.Add(this.OnMatch);			
            rc.Get<GameObject>("EnterMap").GetComponent<Button>().onClick.Add(this.EnterMap);
			this.text = rc.Get<GameObject>("Text").GetComponent<Text>();
		}

		private void OnMatch()
		{
			
		}	

		private void EnterMap()
		{
			try
			{
				SessionComponent.Instance.Session.CallWithAction(new C2G_EnterMap(),response=>EnterMapSuc(response));
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}	
		}

        private void EnterMapSuc(AResponse response)
        {
            G2C_EnterMap g2CEnterMap = response as G2C_EnterMap;
            PlayerComponent.Instance.MyPlayer.UnitId = g2CEnterMap.UnitId;
            Log.Debug("-------curPlayerCout-------------:=" + g2CEnterMap.Count);
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UILobby);            
        }
	}
}
