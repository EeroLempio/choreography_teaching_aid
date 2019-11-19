using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform pointOfReference1, pointOfReference2;
    [Range(12, 90)]
    public float minFov, maxFov;
    
    Transform pivot;
    Transform cameraTransform;

    Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
        cameraTransform = transform;
        pivot = new GameObject("CameraPivot").transform;
        cameraTransform.rotation = new Quaternion();
        cameraTransform.position = new Vector3(0, 0, -10);
        cameraTransform.parent = pivot;
    }

    void Update()
    {
        track();
    }

    void track(){
        pivot.position = pointOfReference1.position + (0.5f * (pointOfReference2.position - pointOfReference1.position));
    }

    public void zoom(float amount){
        camera.fieldOfView += amount;
        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFov, maxFov);
    }

    public void rotate(float hzAmount, float vtAmount){
        pivot.Rotate(0,hzAmount,0, Space.World);
        if(
            (vtAmount < 0 && Vector3.Dot(cameraTransform.forward, Vector3.up) < 0) || 
            (vtAmount > 0 && Vector3.Dot(cameraTransform.up, Vector3.up) > 0)){
            pivot.Rotate(vtAmount,0,0, Space.Self);
        }
    }
}
