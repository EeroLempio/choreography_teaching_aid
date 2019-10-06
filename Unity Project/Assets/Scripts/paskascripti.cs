using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paskascripti : MonoBehaviour
{
    public static List<string> mylist;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void startDance()
    {
        AnimatorController ac = GetComponent<AnimatorController>();
        ac.playStates(mylist, true);
    }

    // Update is called once per frame
    void Update()
    {
        mylist = ButtonHandler.listOfFigures;
    }
}
