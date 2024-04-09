using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform lookPos;
    public Vector3 offSetPos;
    public float lookPosOffSetH, moveSpeed, rotateSpeed;
    private Vector3 targetPos;
    private Quaternion finalLookRotation;
    public float angleYSpeed = 100f, angle = 0;

    // Update is called once per frame
    void Update()
    {
        if (lookPos == null) return;
        angle = Mathf.Clamp(angle + Input.GetAxis("Mouse Y") * angleYSpeed * Time.deltaTime, -30, 30);
        //Debug.Log(angle);
        //Debug.Log(Mathf.Deg2Rad * angle);
        targetPos = lookPos.position + lookPos.forward * offSetPos.z + Vector3.up * (offSetPos.z *  Mathf.Tan(Mathf.Deg2Rad * angle) + offSetPos.y);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, moveSpeed * Time.deltaTime);
        finalLookRotation = Quaternion.LookRotation(lookPos.position + Vector3.up * lookPosOffSetH - Camera.main.transform.position);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, finalLookRotation, rotateSpeed * Time.deltaTime);
    }

    public void SetLookTarget(Transform trans)
    {
        lookPos = trans;
    }

}
