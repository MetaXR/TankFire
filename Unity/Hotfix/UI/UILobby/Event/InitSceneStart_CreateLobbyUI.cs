using Model;

namespace Hotfix
{
	[Event((int)EventIdType.CreateLobbyUI)]
	public class InitSceneStart_CreateLobbyUI: IEvent
	{
		public void Run()
		{
			UI ui = Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UILobby);
		}
	}
}
