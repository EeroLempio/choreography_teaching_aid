using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paskascripti : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> mylist = new List<string>(new string[] { "BasicLF(1)", "NatTF(1)", "RevTB(1)" });
        AnimatorController ac = GetComponent<AnimatorController>();
        ac.playStates(mylist, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
