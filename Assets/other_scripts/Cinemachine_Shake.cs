using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinemachine_Shake : MonoBehaviour
{
    // Start is called before the first frame update
    public static Cinemachine_Shake Instance{get;private set;}
    private CinemachineVirtualCamera Cine_camera;
    private float shaketimer;
    private void Awake()
    {
        Instance=this;
        Cine_camera= GetComponent<CinemachineVirtualCamera>();
    }
    public void Shake_Camera(float intesity,float time)
    {
CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin=Cine_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain=intesity;
    shaketimer=time;
    }

    // Update is called once per frame
   private  void Update()
    {
        if(shaketimer>0)
        {
            shaketimer-=Time.deltaTime;
            if(shaketimer<=0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin=Cine_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain=0f;
            }
        }
    }
}

