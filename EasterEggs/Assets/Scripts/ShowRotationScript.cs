using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using TMPro;

public class ShowRotationScript : MonoBehaviour
{
    [SerializeField] UnityEngine.XR.XRNode m_VRNode = UnityEngine.XR.XRNode.Head;
    float TotalRotation = 0;
    public bool WinGame = false;
    GameObject Camera;
    Vector3 LastForward = new Vector3(0,0,0);

    private void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        LastForward = Camera.transform.TransformDirection(Vector3.forward);
        //Quaternion QuatHead = UnityEngine.XR.InputTracking.GetLocalRotation(m_VRNode);
        //Vector3 EulerAngles = QuatHead.eulerAngles;
        //LastRotation = EulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(UnityEngine.XR.InputTracking.GetLocalRotation(m_VRNode));
        //Quaternion QuatHead = UnityEngine.XR.InputTracking.GetLocalRotation(m_VRNode);
        //Vector3 EulerAngles = QuatHead.eulerAngles;

        //Vector3 Movement = -LastRotation + EulerAngles;
        //TotalRotation += Mathf.Abs(Movement.x) + Mathf.Abs(Movement.y) + Mathf.Abs(Movement.z);


        if (WinGame)
        {
            TextMeshProUGUI TextmeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
            TextmeshProUGUI.SetText(((int)TotalRotation).ToString());
        }
        else
        {
            TotalRotation += Vector3.Angle(LastForward, Camera.transform.TransformDirection(Vector3.forward));
            LastForward = Camera.transform.TransformDirection(Vector3.forward);
        }
        //TextMeshProUGUI TextmeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
        //TextmeshProUGUI.SetText(EulerAngles.ToString("F2"));

    }
    


    private void LateUpdate()
    {
        //var quaternion = InputTracking.GetLocalRotation(m_VRNode);
        //LogRotation("LateUpdate");
    }

    private IEnumerator EndOfFrameUpdate()
    {
        yield return new WaitForEndOfFrame();
        /*while (true)
        {
            yield return new WaitForEndOfFrame();
            LogRotation("EndOfFrame");
        }*/
    }

    /*private void LogRotation(string id)
    {
        
        var euler = quaternion.eulerAngles;
        Debug.Log(string.Format("{0} {1}, ({2}) Quaternion {3} Euler {4}", logPrefix, id, m_VRNode, quaternion.ToString("F2"), euler.ToString("F2")));
    }*/
}
