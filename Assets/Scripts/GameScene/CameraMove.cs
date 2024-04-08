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

    // Update is called once per frame
    void Update()
    {
        if (lookPos == null) return;
        targetPos = lookPos.position + lookPos.forward * offSetPos.z + Vector3.up * offSetPos.y;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, moveSpeed * Time.deltaTime);
        finalLookRotation = Quaternion.LookRotation(lookPos.position + Vector3.up * lookPosOffSetH - Camera.main.transform.position);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, finalLookRotation, rotateSpeed * Time.deltaTime);
    }

    public void SetLookTarget(Transform trans)
    {
        lookPos = trans;
    }

}
