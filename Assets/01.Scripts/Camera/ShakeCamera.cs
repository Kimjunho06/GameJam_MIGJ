using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeAmpltude = 1.2f;
    [SerializeField] private float shakeFrequency = 2.0f;

    [SerializeField] private CinemachineFreeLook freeLookCamera;
    private CinemachineBasicMultiChannelPerlin freeLookCameraNoise;

    private float shakeElapsedTime = 0f;

    private void Start()
    {
        if(freeLookCamera != null)
        {
            freeLookCameraNoise = freeLookCamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.V))
        {
            shakeElapsedTime = shakeDuration;
        }

        if(freeLookCamera != null || freeLookCameraNoise != null)
        {
            if(shakeElapsedTime > 0)
            {
                freeLookCameraNoise.m_AmplitudeGain = shakeAmpltude;
                freeLookCameraNoise.m_FrequencyGain = shakeFrequency;

                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                freeLookCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }
}
