using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{

    public Dropdown myDropdown;
    public static string myFigure;

    // Update is called once per frame
    void Update()
    {
        switch (myDropdown.value)
        {
            case 1:
                myFigure = "NatTF(1)";
                break;
            case 2:
                myFigure = "SpinT(1)";
                break;
            case 3:
                myFigure = "RevTB(1)";
                break;
            case 4:
                myFigure = "RevTF(1)";
                break;
            case 5:
                myFigure = "BasicLF(1)";
                break;
        }
    }
}
