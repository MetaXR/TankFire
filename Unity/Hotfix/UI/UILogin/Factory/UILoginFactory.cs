using System;
using Model;
using UnityEngine;

namespace Hotfix
{
    [UIFactory((int)UIType.UILogin)]
    public class UILoginFactory : IUIFactory
    {
        public UI Create(Scene scene, UIType type, GameObject gameObject)
        {
	        try
			{
                GameObject prefab = Resources.Load<GameObject>("KV").Get<GameObject>("UILogin");             
                GameObject login = UnityEngine.Object.Instantiate(prefab);
                login.layer = LayerMask.NameToLayer(LayerNames.UI);
		        UI ui = EntityFactory.Create<UI, Scene, UI, GameObject>(scene, null, login);

				ui.AddComponent<UILoginComponent>();
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