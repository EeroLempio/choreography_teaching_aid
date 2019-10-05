using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform pointOfReference;
    [Range(0, 20)]
    public float minDistance, maxDistance;
    
    Transform pivot;
    Transform cameraTransform;
    // Start is called before the first frame update

    void Awake()
    {
        cameraTransform = transform;
        pivot = new GameObject("CameraPivot").transform;
        cameraTransform.rotation = new Quaternion();
        cameraTransform.position = new Vector3(0, 0, -maxDistance);
        cameraTransform.parent = pivot;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        track();
        zoom(Input.GetAxis("Mouse ScrollWheel") * 6);
        if(Input.GetMouseButton(0))
            rotate((Input.GetAxis("Mouse X") * 2), (Input.GetAxis("Mouse Y") * 2));
    }

    void track(){
        pivot.position = pointOfReference.position;
    }

    void zoom(float amount){
        if(
            (amount < 0 && Vector3.Distance(cameraTransform.position, pivot.position) > minDistance) || 
            (amount > 0 && Vector3.Distance(cameraTransform.position, pivot.position) < maxDistance)){
            cameraTransform.position -= cameraTransform.forward * amount;
        }
    }

    void rotate(float hzAmount, float vtAmount){
        pivot.Rotate(0,hzAmount,0, Space.World);
        if(
            (vtAmount < 0 && Vector3.Dot(cameraTransform.forward, Vector3.up) < 0) || 
            (vtAmount > 0 && Vector3.Dot(cameraTransform.up, Vector3.up) > 0)){
            pivot.Rotate(vtAmount,0,0, Space.Self);
        }
    }
}
