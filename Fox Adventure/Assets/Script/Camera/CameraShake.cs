using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer;
    private float totalShakeTime;
    private float startIntensity;

    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if(shakeTimer <= 0.0f) 
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
                Mathf.Lerp(startIntensity, 0.0f, 1 - (shakeTimer/totalShakeTime));
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startIntensity = intensity;
        shakeTimer = time;
        totalShakeTime = time;
    }
}
