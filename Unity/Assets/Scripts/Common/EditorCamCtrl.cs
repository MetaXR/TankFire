using UnityEngine;
using System.Collections;

/*
 * @此摄像机模式为orthographic
 * 控制orthographicsize来控制缩放
 * 旋转则旋转自身
 * 通过旋转与平移的配合，实现场景漫游
 * *******/
public class EditorCamCtrl : MonoBehaviour {

    public float zoomSensitivity = 25;  //不要大于50
    public float translateSensitivity = 10f;
    public float rotSensitivity = 12;

    public float centerDistance = 10;   //视觉中心离摄像机距离 

    public float maxSize = 100;

    public float maxRotLimit = 10;
    public float minRotLimit = -10;

    //记录缩放时移动的距离
    private float zoomDistance = 0;

    private bool move = false;
    public float animSensitivity = 3f; //移动到目标位置时的动画系数
    private float orgAnimSensitivity = 0;

    private float percent = 0;
    private Vector3 orgAngle;
    private Vector3 targetAngle;
    private Vector3 orgPos;
    private Vector3 targetPos;
	private float orgFiledOfView;
	private float targetFiledOfView;

    private float nearOffset = 3;
    private bool nearMove = false;
    private Vector3[] roundRotPoints; //用于最后绕一个弧度靠近目标位置
    [HideInInspector]
    public Transform lookAt;

    #region 快速定位

    private bool locateZoom = false;
    private float locateZoomPercent = 0;
    private float camTargetSize = 0;
    private float camOrgSize = 0;

    #endregion

    #region 视角切换

    public enum CamViewType
    {
        TopView = 0,
        Lfet,
        Right,
    }

    #endregion

    //UI控制
    protected static int lockCount = 0;

    protected Rect validArea;   //有效范围

    private bool mTranslateEnable = true;
    public bool translateEnable
    {
        get
        {
            return mTranslateEnable;
        }
        set
        {
            mTranslateEnable = value;
        }
    }

    public delegate void CamAnimFun();
    private CamAnimFun smoothAnimFun;

    //优化
    private Transform selfTransform;
    private Camera selfCam;
    private float cSize;

	// Use this for initialization
	void Start () {
        selfTransform = transform;
        selfCam = GetComponent<Camera>();

        camTargetSize = GetComponent<Camera>().orthographicSize;
        orgAnimSensitivity = animSensitivity;

        validArea = new Rect(0, 0, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
            smoothAnimFun();
            if (percent > 1)
            {
                percent = 0;
                move = false;
                animSensitivity = orgAnimSensitivity;
            }
        }

        if (locateZoom)
        {
            selfCam.orthographicSize = Mathf.Lerp(camOrgSize, camTargetSize, locateZoomPercent += 0.05f);
            if (locateZoomPercent > 1)
            {
                locateZoom = false;
            }
        }
	}

    //优化
    float delta_rotation_x;
    float delta_rotation_y;

    Vector3 moveOffset;

    void LateUpdate()
    {
        //锁住的时候不作为
        if (lockCount != 0)
        {
            return;
        }

        //不在有效范围则不作为
        if (!validArea.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
        {
            return;
        }

        #region 缩放

        float zoomOffset = Input.GetAxis("Mouse ScrollWheel");

        float limitParameter = centerDistance > 10 ? 10 : centerDistance;
        float disOffset = -zoomOffset * zoomSensitivity * Time.deltaTime * limitParameter;
        //centerDistance -= disOffset;
        selfTransform.Translate(0, 0, -disOffset);

        //if (!selfCam.isOrthoGraphic)
        //{
        //    cSize = selfCam.fieldOfView;
        //    if (cSize > maxSize && zoomOffset > 0)
        //        return;

        //    float tOffset = zoomOffset * zoomSensitivity * cSize * Time.deltaTime;
        //    selfCam.fieldOfView += tOffset;
        //}
        //else
        //{
        //    cSize = selfCam.orthographicSize;
        //    if (cSize > maxSize && zoomOffset > 0)
        //        return;

        //    float tOffset = zoomOffset * zoomSensitivity * cSize * Time.deltaTime;
        //    selfCam.orthographicSize += tOffset;
        //}

	    #endregion

        #region 平移
        if (mTranslateEnable)
        {
            //中键平移
            if (Input.GetMouseButton(0)) //left mouse btn translate
            {
                float px = Input.GetAxis("Mouse X");
                float py = Input.GetAxis("Mouse Y");

                //selfTransform.Translate(-1 * new Vector3(px, py, 0) * translateSensitivity * Time.deltaTime);
                moveOffset += -1 * new Vector3(px, py, 0) * translateSensitivity * Time.deltaTime;
                selfTransform.Translate(Vector3.Lerp(Vector3.zero, moveOffset, 0.5f));
                moveOffset = moveOffset * 0.5f;
            }
        }
        #endregion

        #region 旋转

        //按住鼠标右键移动鼠标旋转镜头
        if (Input.GetMouseButton(1))
        {
            //float delta_rotation_x = Input.GetAxis("Mouse X") * cSize * rotSensitivity * Time.deltaTime;
            //float delta_rotation_y = -Input.GetAxis("Mouse Y") * cSize * rotSensitivity * Time.deltaTime;

            //selfTransform.Rotate(Vector3.up, delta_rotation_x, Space.World);
            //selfTransform.Rotate(Vector3.right, delta_rotation_y);

            delta_rotation_x = Input.GetAxis("Mouse X") * rotSensitivity * Time.deltaTime;
            delta_rotation_y = -Input.GetAxis("Mouse Y") * rotSensitivity * Time.deltaTime;

            //delta_rotation_x = Mathf.Clamp(delta_rotation_x, minRotLimit, maxRotLimit);
            //delta_rotation_y = Mathf.Clamp(delta_rotation_y, minRotLimit, maxRotLimit);
            //旋转是以镜头当前视野中心点为原点进行的 在一个平行于地面的水平面上旋转

            //先算出当前视野中心的坐标，中心的概念就是正对 上下左右都对齐，离开一个distance距离
            //所以中心点相对镜头参照系的坐标就是0,0,distance,乘以镜头的变换,在加上镜头的世界坐标 就是中心点的世界坐标了
            Vector3 position = selfTransform.rotation * new Vector3(0, 0, centerDistance) + selfTransform.position;

            //Y轴方向上用世界坐标的变换就可以拉 否则镜头会歪的 
            selfTransform.Rotate(0, delta_rotation_x, 0, Space.World);
            //x轴方向的旋转是相对自身的 否则镜头也会歪
            selfTransform.Rotate(delta_rotation_y, 0, 0);
            //转完以后 把这个新的旋转角度 乘以一个“正对中心”的相对坐标 再加上中心点的坐标 就是新的镜头世界坐标啦
            selfTransform.position = selfTransform.rotation * new Vector3(0, 0, -centerDistance) + position;
        }

        #endregion
    }

    //快速定位
    public void QuickLocate(GameObject obj)
    {
        //获取物体原始尺寸
        Vector3 objSize = obj.GetComponent<MeshFilter>().mesh.bounds.size;

        //把物体移到摄像机中间并调整摄像机的投影窗口大小
        LerpToTargetPos(transform.rotation * new Vector3(0, 0, -10) + obj.transform.position,transform.localEulerAngles);
        LerpToTargetOrthorgraphicSize(GetComponent<Camera>().orthographicSize, (objSize.x + objSize.y + objSize.z) * 0.45f);
    }

    //顶视角
    public void MoveToView(CamViewType type)
    {
        move = true;
        switch (type)
        {
            case CamViewType.TopView:
                orgAngle = transform.eulerAngles;
                orgPos = transform.position;

                targetAngle = new Vector3(90, 0, 0);

                //找到视觉中心点，再向Y轴方向移动等量距离
                targetPos = transform.position + transform.forward * (centerDistance - zoomDistance) + new Vector3(0, centerDistance - zoomDistance, 0);
                break;
            case CamViewType.Lfet:
                move = false;
                break;
            case CamViewType.Right:
                move = false;
                break;
            default:
                move = false;
                break;

        }
    }

    /// <summary>
    /// 移动到目标位置
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="angle"></param>
    public void LerpToTargetPos(Vector3 pos,Vector3 angle)
    {
		LerpToTargetPos (pos, angle, GetComponent<Camera>().fieldOfView);
    }

	public void LerpToTargetPos(Vector3 pos,Vector3 angle,float filedOfView)
	{
        move = true;
        orgAngle = AngleIn360(transform.eulerAngles);
		orgPos = transform.position;
		orgFiledOfView = GetComponent<Camera>().fieldOfView;
		targetPos = pos;
        targetAngle = AngleIn360(angle);
        targetAngle = AngleQuickToTarget(orgAngle, targetAngle);
		targetFiledOfView = filedOfView;

        smoothAnimFun = SmoothToTargetAnim;
	}

    /// <summary>
    /// 移动到指定位置的实现过程
    /// </summary>
    void SmoothToTargetAnim()
    {
        percent += animSensitivity * Time.deltaTime ;
        selfTransform.position = Vector3.Lerp(orgPos, targetPos, percent);
        selfTransform.eulerAngles = Vector3.Lerp(orgAngle, targetAngle, percent);
		selfCam.fieldOfView = Mathf.Lerp(orgFiledOfView,targetFiledOfView,percent);
    }

    /// <summary>
    /// 先直线靠近物体，在旋转角度正对物体
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="angle"></param>
    /// <param name="filedOfView"></param>
    /// <param name="minDistance"></param>
    public void NearThenRotToTarget(Vector3 pos,Vector3 angle, float filedOfView, float minDistance)
    {
        move = true;
        orgAngle = AngleIn360(transform.eulerAngles);
        orgPos = transform.position;
        orgFiledOfView = GetComponent<Camera>().fieldOfView;
        targetPos = pos;
        targetAngle = AngleIn360(angle);
        targetAngle = AngleQuickToTarget(orgAngle, targetAngle);
        targetFiledOfView = filedOfView;

        nearMove = true;
        nearOffset = minDistance;

        smoothAnimFun = SmoothNearThenRotAnim;
    }

    /// <summary>
    /// 先直线靠近物体，在旋转角度正对物体实现过程
    /// </summary>
    void SmoothNearThenRotAnim()
    {
        if(nearMove)
        {
            if ((selfTransform.position - targetPos).magnitude > nearOffset * nearOffset)
            {
                selfTransform.position = Vector3.Lerp(selfTransform.position, targetPos, 0.1f);
            }
            else
            {
                nearMove = false;
                selfTransform.position = targetPos;
                selfTransform.localEulerAngles = targetAngle;
                selfCam.fieldOfView = targetFiledOfView;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orgSize"></param>
    /// <param name="targetSize"></param>
    public void LerpToTargetOrthorgraphicSize(float orgSize, float targetSize)
    {
        locateZoom = true;
        locateZoomPercent = 0;
        camOrgSize = orgSize;
        camTargetSize = targetSize;
    }

    #region 角度预处理

    //确定是目标角度使能快速旋转到目标角度
    float AngleQuickToTarget(float angle, float targetAngle)
    {
        float d1 = targetAngle - angle;
        int dir = d1 > 0 ? -1 : 1;
        float d2 = d1 + dir * 360;

        if (Mathf.Abs(d1) < Mathf.Abs(d2))
        {
            return targetAngle;
        }
        else
        {
            return targetAngle + dir * 360;
        }
    }

    Vector3 AngleQuickToTarget(Vector3 angle, Vector3 targetAngle)
    {
        Vector3 res = new Vector3();
        res.x = AngleQuickToTarget(angle.x, targetAngle.x);
        res.y = AngleQuickToTarget(angle.y, targetAngle.y);
        res.z = AngleQuickToTarget(angle.z, targetAngle.z);

        return res;
    }

    //把角度转化到360度的区间中
    float AngleIn360(float angle)
    {
        while (angle < 0)
        {
            angle += 360;
        }

        while (angle > 360)
        {
            angle -= 360;
        }

        return angle;
    }

    Vector3 AngleIn360(Vector3 angle)
    {
        angle.x = AngleIn360(angle.x);
        angle.y = AngleIn360(angle.y);
        angle.z = AngleIn360(angle.z);

        return angle;
    }

    #endregion

    //锁住摄像机
    public static void LockCam()
    {
        ++lockCount;
    }

    //解锁
    public static void UnlockCam()
    {
        --lockCount;

        if (lockCount < 0)
        {
            lockCount = 0;

            Debug.Log("Too Many Camera Unlock!");
        }
    }
}
