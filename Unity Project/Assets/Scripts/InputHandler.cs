using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    bool mobile = true;

    float width, height;

    CameraController cameraController;
    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        cameraController = gameObject.GetComponent<CameraController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mobile){
            handleTouch();
        } else {
            handleMouse();
        }
    }

    void handleMouse(){
        cameraController.zoom(Input.GetAxis("Mouse ScrollWheel") * 6);
        if(Input.GetMouseButton(0))
            cameraController.rotate((Input.GetAxis("Mouse X") * 2), (Input.GetAxis("Mouse Y") * 2));
    }
    void handleTouch(){
        if(Input.touchCount > 0){
            Touch t0 = Input.GetTouch(0);
            Vector2 t0DeltaPos = t0.deltaPosition;
            if(Input.touchCount == 2){
                Touch t1 = Input.GetTouch(1);
                Vector2 t1DeltaPos = t1.deltaPosition;

                Vector2 t0PrevPos = t0.position - t0DeltaPos;
                Vector2 t1PrevPos = t1.position - t1DeltaPos;

                float prevTDeltaMag = (t0PrevPos - t1PrevPos).magnitude;
                float tDeltaMag = (t0.position - t1.position).magnitude;

                float zoomAmount = (prevTDeltaMag - tDeltaMag) / height;

                cameraController.zoom(zoomAmount * 30);
            } else {
                t0DeltaPos.x = t0DeltaPos.x / width;
                t0DeltaPos.y = t0DeltaPos.y / height;
                cameraController.rotate(t0DeltaPos.x * 30, t0DeltaPos.y * 30);
            }
        }
    }
}
