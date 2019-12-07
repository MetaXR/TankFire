using UnityEngine;

namespace Model
{
	[ObjectEvent]
	public class CameraComponentEvent : ObjectEvent<CameraComponent>, IAwake,IUpdate
	{
		public void Awake()
		{
			this.Get().Awake();
		}

		public void Update()
		{
			this.Get().Update();
		}
	}

	public class CameraComponent : Component
	{
		// 战斗摄像机
		public Camera mainCamera;

		public Unit Unit;

		public Camera MainCamera
		{
			get
			{
				return this.mainCamera;
			}
		}

		public void Awake()
		{
			this.mainCamera = Camera.main;
		}

		public void Update()
		{
			// 摄像机每帧更新位置
			UpdatePosition();
		}

		private void UpdatePosition()
		{
            if (this.Unit == null)
                return;
			//Vector3 cameraPos = this.mainCamera.transform.position;
			//this.mainCamera.transform.position = new Vector3(this.Unit.Position.x, cameraPos.y, this.Unit.Position.z - 20);
		}
	}
}
