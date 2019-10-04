using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Transform pointOfReference;
    [HideInInspector]
    public List<string> stateNames;

    Dictionary<string, Vector3> startPositions;
    Animator animator;
    void Awake(){ init();}

    void init(){
        animator = GetComponent<Animator>();

        stateNames = new List<string>();
        startPositions = new Dictionary<string, Vector3>();
        var clips = animator.runtimeAnimatorController.animationClips;
        
        foreach(var clip in clips){
            var stateName = clip.name;
            clip.SampleAnimation(gameObject, 0);
            startPositions.Add(stateName, pointOfReference.position);
            stateNames.Add(stateName);
        }
    }
    void setPosition(string stateName)
    {
        Vector3 trPos = transform.position;
        Vector3 prPos = pointOfReference.position;
        Vector3 prFwd = pointOfReference.forward;
        Vector3 offset = startPositions[stateName];

        Vector3 lookPos = new Vector3(trPos.x + prFwd.x, 0, trPos.z + prFwd.z);
        this.transform.LookAt(lookPos);

        Vector3 posOffSet = transform.forward * offset.z + transform.right * offset.x;
        transform.position = new Vector3(prPos.x, trPos.y, prPos.z) - posOffSet;
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
        setPosition(stateName);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }

        while ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
        {
            yield return null;
        }
        playStates(stateNames, loop);
    }
}
