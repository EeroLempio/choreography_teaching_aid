using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationPosition : MonoBehaviour
{
    public Transform pointOfReference;

    public string stateName;

    Animator animator;
    RuntimeAnimatorController runtimeAnimator;
    List<string> stateNames = new List<string>();
    Vector3 offSetPos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        runtimeAnimator= animator.runtimeAnimatorController;

        var clips = runtimeAnimator.animationClips;
        foreach(var clip in clips){
            stateNames.Add(clip.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* 
        if (Input.GetKeyDown(KeyCode.Space)){
            
            animator.SetTrigger("AnimTrigger");
            animator.Play(stateNames[1], 0);
            Debug.Log((animator.GetCurrentAnimatorClipInfo(0))[0].clip);
            //setPosition();
        }
        }*/
        if (Input.GetKeyDown(KeyCode.Space)){
            playState(stateName);
        }

        if (Input.anyKeyDown)
        {
        playAnims(Input.inputString.ToString());
        }
    }

    void setPosition()
    {
        Vector3 por = pointOfReference.position;
        transform.position = new Vector3(por.x, transform.position.y, por.z);
        transform.rotation = Quaternion.LookRotation(pointOfReference.forward, Vector3.up);
        pointOfReference.position = new Vector3(transform.position.x, por.y, transform.position.z);
        
    }

    void playState(string stateName){
        if(!stateNames.Contains(stateName)){
            Debug.LogError("No state with name \"" + stateName + "\" in animator");
            return;
        }

        animator.Play(stateName);
    }

    void playAnims(string num){
        int n;
        if(int.TryParse(num, out n)){
            //playState(stateNames[n]);
            var coroutine = PlayAndWaitForAnim(animator, stateNames[n]);
            StartCoroutine(coroutine);
        }
    }

    public IEnumerator PlayAndWaitForAnim(Animator targetAnim, string stateName)
    {
    targetAnim.Play(stateName);

    //Wait until we enter the current state
    while (!targetAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
    {
    yield return null;
    }

    //Now, Wait until the current state is done playing
    while ((targetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
    {
    yield return null;
    }

    Debug.Log("yaaa");
    }
}
