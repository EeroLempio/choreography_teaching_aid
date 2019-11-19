using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Transform pointOfReference1, pointOfReference2;
    [HideInInspector]
    public List<string> stateNames;

    Dictionary<string, Vector3> startPositions;
    Dictionary<string, float> startAngles;
    Animator animator;
    void Awake() { init(); }

    float angleToForwardBetween0And360(Vector3 vec) {
        Vector3 vecP = Vector3.ProjectOnPlane(vec.normalized, transform.up);
        float angle = Vector3.Angle(transform.forward, vecP);
        float res = Vector3.Dot(vecP, transform.right) >= 0 ? angle : 360 - angle;
        Debug.Log(res);
        return res;
    }

    void init()
    {
        animator = GetComponent<Animator>();

        stateNames = new List<string>();
        startPositions = new Dictionary<string, Vector3>();
        startAngles = new Dictionary<string, float>();
        var clips = animator.runtimeAnimatorController.animationClips;

        foreach (var clip in clips)
        {
            var stateName = clip.name;
            clip.SampleAnimation(gameObject, 0);
            startPositions.Add(stateName, getBetweenPosition());
            startAngles.Add(stateName, angleToForwardBetween0And360(pointOfReference1.forward));
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
        float angle = startAngles[stateName];
        
        Vector3 posOffSet = transform.forward * offset.z + transform.right * offset.x;
        transform.position = new Vector3(prPos.x, trPos.y, prPos.z) - posOffSet;
        float nowAngle = angleToForwardBetween0And360(prFwd);
        transform.RotateAround(prPos, Vector3.up, nowAngle - angle);
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
