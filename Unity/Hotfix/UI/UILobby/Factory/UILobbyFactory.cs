using System;
using Model;
using UnityEngine;

namespace Hotfix
{
    [UIFactory((int)UIType.UILobby)]
    public class UILobbyFactory : IUIFactory
    {
        public UI Create(Scene scene, UIType type, GameObject gameObject)
        {
	        try
	        {
                GameObject prefab = Resources.Load<GameObject>("KV").Get<GameObject>("UILobby");
                GameObject lobby = UnityEngine.Object.Instantiate(prefab);
                lobby.layer = LayerMask.NameToLayer(LayerNames.UI);
				UI ui = EntityFactory.Create<UI, Scene, UI, GameObject>(scene, null, lobby);

				ui.AddComponent<UILobbyComponent>();
				return ui;
	        }
	        catch (Exception e)
	        {
				Log.Error(e.ToStr());
		        return null;
	        }
		}
    }
}