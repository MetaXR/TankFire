#define SHADER_CONTROLL

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
//using Vectrosity;
using UnityEngine.UI;
//using DG.Tweening;

public class DetailCameraCtr : MonoBehaviour
{
    //public Transform ContrastCamera;
    //public Vector3 LookAt;          // Desired lookat position
    //public float Distance;          // Desired distance (units, ie Meters)
    //public float Rotation;          // Desired rotation (degrees)
    //public float Tilt;              // Desired tilt (degrees)

    public Transform targetObject;
    public Vector3 targetOffset;
    public float xDegOffset;
    public float yDegOffset;
    public float averageDistance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = 0.6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomSpeed = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;
    public float rotateOnOff = 1;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;

    private float desiredDistance;

    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    private float idleTimer = 0.0f;
    private float idleSmooth = 0.0f;

    private Vector2 pointA;
    private Vector2 pointB;
    private float desiredDistanceTemp;

    public bool Spin = true;
    public bool SpinTemp = true;

    private float m_desiredDistance;
    private float m_currentDistance;
    private Vector3 m_position;
    public float minDistanceForShader = 4.5f;
    public bool isAutoSpin=false;
    public void DistanceIn()
    {
        desiredDistance = Mathf.Clamp(desiredDistance - 1.0f, minDistance, maxDistance);
    }

    public void DistanceOut()
    {
        desiredDistance = Mathf.Clamp(desiredDistance + 1.0f, minDistance, maxDistance);
    }
    public void Init()
    {


        currentDistance = averageDistance;
        desiredDistance = averageDistance;
        //rotation = transform.rotation;
        //currentRotation = Quaternion.Euler(new Vector3(0,90,0));
        //desiredRotation = Quaternion.Euler(new Vector3(0,90,0));
        //position = transform.position;


        xDeg = xDegOffset;
        yDeg = yDegOffset;
        //xDeg = Vector3.Angle(Vector3.right, transform.right) + xDegOffset;
        //yDeg = Vector3.Angle(Vector3.up, transform.up) + yDegOffset;
        //position = targetObject.position - (rotation * Vector3.forward * currentDistance + targetOffset);
    }

    public void Reset()
    {
        averageDistance = currentDistance;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.logger()
    }
    void LateUpdate()
    {
        if (isAutoSpin)
        {
            xDeg += xSpeed * 0.01f * 1f*Time.deltaTime;
           //Debug.Log(xDeg);
        }
        if (Input.GetMouseButton(0))
        {
            //Debug.Log(1);
            xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            //xDeg = Angle(xDeg);
            yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            yDeg = Mathf.Clamp(Angle(yDeg), yMinLimit, yMaxLimit);

            desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
            currentRotation = transform.rotation;
            rotation = Quaternion.Lerp(currentRotation, desiredRotation, 0.02f * zoomDampening);
            transform.rotation = rotation;
            //if (ContrastCamera != null)
            //    ContrastCamera.rotation = rotation;
            idleTimer = 0;
            idleSmooth = 0;
            SpinTemp = true;
        }
        else
        {
            if (Spin && SpinTemp)
            {
                idleTimer += 0.0002f;
                if (idleTimer > rotateOnOff && rotateOnOff > 0)
                {
                    idleSmooth += (0.02f + idleSmooth) * 0.005f;
                    idleSmooth = Mathf.Clamp(idleSmooth, 0, 0.25f);
                    xDeg += xSpeed * 0.01f * idleSmooth;
                }
            }
            xDeg = Angle(xDeg);
            yDeg = Mathf.Clamp(Angle(yDeg), yMinLimit, yMaxLimit);
            desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
            currentRotation = transform.rotation;
            rotation = Quaternion.Lerp(currentRotation, desiredRotation, 0.02f * zoomDampening * 2);
            transform.rotation = rotation;
            //if (ContrastCamera != null)
            //    ContrastCamera.rotation = rotation;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            desiredDistance = Mathf.Clamp(desiredDistance + zoomSpeed, minDistance, maxDistance);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            desiredDistance = Mathf.Clamp(desiredDistance - zoomSpeed, minDistance, maxDistance);
        }

        //Debug.Log ("==================== xDeg:" + xDeg + " yDeg:" + yDeg + " desiredDistance:" + desiredDistance);

        #region pingbi
        float angle = 0.0f;
        if (xDeg < 0)
            angle = 360 + xDeg;
        else
            angle = xDeg;
        float zOffset = 0.0f;
        float focalDistance = 0.3f;

        if (angle >= 0 && angle < 45)
        {
            zOffset = -focalDistance;
        }
        else if (angle >= 45 && angle < 90)
        {
            float localAngle = 90.0f - angle;
            float ratio = localAngle / 45.0f;
            zOffset = -ratio * focalDistance;
        }
        else if (angle >= 90 && angle < 135)
        {
            float localAngle = angle - 90.0f;
            float ratio = localAngle / 45.0f;
            zOffset = ratio * focalDistance;
        }
        else if (angle >= 135 && angle < 225)
        {
            zOffset = focalDistance;
        }
        else if (angle >= 225 && angle < 270)
        {
            float localAngle = 270.0f - angle;
            float ratio = localAngle / 45.0f;
            zOffset = ratio * focalDistance;
        }
        else if (angle >= 270 && angle < 315)
        {
            float localAngle = angle - 270.0f;
            float ratio = localAngle / 45.0f;
            zOffset = -ratio * focalDistance;
        }
        else if (angle >= 315 && angle < 360)
        {
            zOffset = -focalDistance;
        } 
        #endregion
        currentDistance = Mathf.Lerp(currentDistance, GetDistance(desiredDistance - 1.2f, desiredDistance, xDeg), 0.02f * zoomDampening);
        if (targetObject != null)
        {
            position = targetObject.position + new Vector3(0.0f, 0.0f, zOffset) - (rotation * Vector3.forward * currentDistance + targetOffset);
        }
        else
        {
            Debug.Log("target为空");
        }
                

//#if SHADER_CONTROLL
//        m_currentDistance = Mathf.Lerp(m_currentDistance, GetDistance(m_desiredDistance - 1.2f, m_desiredDistance, xDeg), 0.02f * zoomDampening);
//        m_position = targetObject.position + new Vector3(0.0f, 0.0f, zOffset) - (rotation * Vector3.forward * m_currentDistance + targetOffset);
//#endif
        transform.position = position;
        //if (ContrastCamera != null)
        //    ContrastCamera.position = position;
    }

    public void AutoSpin()
    {
        isAutoSpin = !isAutoSpin;
    }




 public void Start()
    {
        Init();
        //NotificationCenter.DefaultCenter ().AddObserver (this, "CameraFocus");
    }

    private static float isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        var leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        var leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        return leng1 / leng2;
    }

    private static float GetDistance(float minDistance, float maxDistance, float angle)
    {
        double t1 = System.Math.Pow(maxDistance * minDistance, 2);
        double t2 = System.Math.Pow(System.Math.Cos(angle * System.Math.PI / 180) * minDistance, 2);
        double t3 = System.Math.Pow(System.Math.Sin(angle * System.Math.PI / 180) * maxDistance, 2);
        double distancePow = t1 / (t2 + t3);
        double distance = System.Math.Sqrt(distancePow);
        return (float)distance;
    }

    private static Vector3 GetPont(float minDistance, float maxDistance, float angle)
    {
        double t1 = System.Math.Pow(maxDistance * minDistance, 2);
        double t2 = System.Math.Pow(minDistance, 2);
        double t3 = System.Math.Pow(System.Math.Tan(angle * System.Math.PI / 180), 2) * System.Math.Pow(maxDistance, 2);
        double x = System.Math.Sqrt(t1 / (t2 + t3));
        double y = System.Math.Tan(angle * System.Math.PI / 180) * x;
        return new Vector3((float)x, (float)y, 5.0f);
    }

    private static float Angle(float angle)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return angle;
    }


    public void SetAverageDistance(float Distance )
    {
        averageDistance = Distance;
    }
}