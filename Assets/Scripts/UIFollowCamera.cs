using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UIFollowCamera : MonoBehaviour
{

    void LateUpdate()
    {
        transform.LookAt(transform.position +  Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation* Vector3.up);
    }
}
