using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Transform pointOfReference1, pointOfReference2;
    [HideInInspector]
    public List<string> stateNames;

    Dictionary<string, Vector3> startPositions;
    Dictionary<string, Vector3> startDirs;
    Animator animator;
    void Awake() { init(); }

    void init()
    {
        animator = GetComponent<Animator>();

        stateNames = new List<string>();
        startPositions = new Dictionary<string, Vector3>();
        startDirs = new Dictionary<string, Vector3>();
        var clips = animator.runtimeAnimatorController.animationClips;

        foreach (var clip in clips)
        {
            var stateName = clip.name;
            clip.SampleAnimation(gameObject, 0);
            startPositions.Add(stateName, getBetweenPosition());
            startDirs.Add(stateName, pointOfReference1.forward);
            stateNames.Add(stateName);
        }
    }

    Vector3 getBetweenPosition(){
        return pointOfReference1.position + (0.5f * (pointOfReference2.position - pointOfReference1.position));
    }

    void setPosition(string stateName)
    {
        Vector3 trPos = transform.position;
        Vector3 prPos = getBetweenPosition();
        Vector3 prFwd = pointOfReference1.forward;
        Vector3 offset = startPositions[stateName];
        Vector3 dir = startDirs[stateName];
        
        Vector3 posOffSet = transform.forward * offset.z + transform.right * offset.x;
        Vector3 pos = new Vector3(prPos.x, trPos.y, prPos.z) - posOffSet;
        Vector3 lookAtPos = pointOfReference1.position + prFwd + (transform.position - pointOfReference1.position);
        lookAtPos.y = 0;
        transform.LookAt(lookAtPos);

        transform.position = pos;

        transform.RotateAround(prPos, Vector3.up, Vector3.SignedAngle(Vector3.forward, dir, Vector3.up));
    }

    public void playStates(List<string> stateNames, bool loop)
    {
        if (stateNames.Count == 0)
        {
            return;
        }
        string stateName = stateNames[0];
        stateNames.RemoveAt(0);
        if (loop)
        {
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
