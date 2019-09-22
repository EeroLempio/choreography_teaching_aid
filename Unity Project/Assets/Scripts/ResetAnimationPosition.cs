using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationPosition : MonoBehaviour
{
    public Transform pointOfReference;
    Vector3 offSetPos;
    // Start is called before the first frame update
    void Start()
    {
        offSetPos = pointOfReference.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setPosition()
    {
        transform.position += pointOfReference.position - offSetPos;
        pointOfReference.position = offSetPos;
    }
}
