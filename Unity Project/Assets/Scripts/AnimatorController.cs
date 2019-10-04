using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [HideInInspector]
    public List<string> stateNames;

    public Transform pointOfReference;

    Animator animator;
    void Awake(){ init();}

    void init(){
        animator = GetComponent<Animator>();

        stateNames = new List<string>();
        var clips = animator.runtimeAnimatorController.animationClips;
        foreach(var clip in clips){
            stateNames.Add(clip.name);
        }
        Debug.Log(stateNames.Count);
    }
    void setPosition()
    {
        Vector3 por = pointOfReference.position;
        transform.position = new Vector3(por.x, transform.position.y, por.z);
        transform.rotation = Quaternion.LookRotation(pointOfReference.forward, Vector3.up);
        pointOfReference.position = new Vector3(transform.position.x, por.y, transform.position.z);
    }

    public void playStates(List<string> stateNames, bool loop){

        if(stateNames.Count == 0){
            return;
        }
        string stateName = stateNames[0];
        stateNames.RemoveAt(0);
        if (loop){
            stateNames.Add(stateName);
        }
        var coroutine = playStates(stateName, stateNames, loop);
            StartCoroutine(coroutine);
    }
    IEnumerator playStates(string stateName, List<string> stateNames, bool loop)
    {
        animator.Play(stateName);
        setPosition();

        //Wait until we enter the current state
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }

        //Now, Wait until the current state is done playing
        while ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
        {
            yield return null;
        }

        playStates(stateNames, loop);
    }
}
