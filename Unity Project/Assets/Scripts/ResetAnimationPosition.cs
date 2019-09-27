using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationPosition : MonoBehaviour
{
    public Transform pointOfReference;

    Animator anim;
    Vector3 offSetPos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            
            anim.SetTrigger("AnimTrigger");
            setPosition();
        }
    }

    void setPosition()
    {
        Debug.Log("jee");
        if (offSetPos == Vector3.zero)
        {
            offSetPos = pointOfReference.localPosition;
        }
        transform.position += (pointOfReference.localPosition - offSetPos);
        pointOfReference.localPosition = offSetPos;
    }
}
