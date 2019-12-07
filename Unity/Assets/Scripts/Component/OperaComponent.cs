using UnityEngine;

namespace Model
{
    [ObjectEvent]
    public class OperaComponentEvent : ObjectEvent<OperaComponent>, IUpdate, IAwake
    {
        public void Update()
        {
            this.Get().Update();
        }

	    public void Awake()
	    {
		    this.Get().Awake();
	    }
    }

    public class OperaComponent: Component
    {
        public Vector3 ClickPoint;

	    public int mapMask;

	    public void Awake()
	    {
		    this.mapMask = LayerMask.GetMask("Map");
	    }

        public void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
	            if (Physics.Raycast(ray, out hit, 1000, this.mapMask))
	            {
					this.ClickPoint = hit.point;
                    Unit unit = ClientComponent.Instance.LocalUnit;
                    float tilt=310;
                    for (int i = 0; i < unit.GameObject.transform.childCount; i++)
                    {
                        GameObject child = unit.GameObject.transform.GetChild(i).gameObject;
                        if (child.GetComponent<FireEquip>() != null)
                        {

                            FireEquip fireEquip = child.GetComponent<FireEquip>();
                            tilt = fireEquip.uDTilt;
                        }
                    }
                    Debug.Log(this.ClickPoint.ToString() + "OperaComponent-----Update------Input.GetMouseButtonDown");
                    Debug.Log(tilt.ToString() + "OperaComponent-----Update------tilt");
                    SessionComponent.Instance.Session.Send(new Actor_UnitPos()
                    {
                        UserId = ClientComponent.Instance.LocalPlayer.UserID,
                        UnitId = ClientComponent.Instance.LocalPlayer.Id,
                        X = (int)(this.ClickPoint.x * 1000),
                        Z = (int)(this.ClickPoint.z * 1000),
                        udtilt = (int)(tilt)
                    });
				}
            }
        }
    }
}
