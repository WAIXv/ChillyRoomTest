﻿using System;
using System.Collections;
using Event;
using Input;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Listening to")] 
        [SerializeField] private Vector3EventChannelSO _cameraShakeEvent;
        
        private void OnEnable()
        {
            _cameraShakeEvent.OnEventRaised += OnCameraShake;
        }
        
        private void OnDisable()
        {
            _cameraShakeEvent.OnEventRaised -= OnCameraShake;
        }

        private void OnCameraShake(Vector3 info)
        {
            StartCoroutine(ShakeCamera(info, info.z));
        }

        private IEnumerator ShakeCamera(Vector2 dir, float duration)
        {
            var startTime = Time.time;
            var originPos = transform.position;
            while(Time.time - startTime < duration / 2)
            {
                transform.position = originPos + (Vector3)dir;
                yield return null;
            }

            while (Time.time - startTime > duration / 2)
            {
                transform.position = originPos;
                yield break;
            }
        }
    }
}