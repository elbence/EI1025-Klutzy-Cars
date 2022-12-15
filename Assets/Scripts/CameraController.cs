using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    private Transform target;

    void Update() {
        if (target == null) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            cam.Follow = target;
        }
    }
}
