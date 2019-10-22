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
                myFigure = "Natural_Closed_change_123";
                break;
            case 2:
                myFigure = "Natural_Closed_Change_456";
                break;
            case 3:
                myFigure = "Natural_Turn_123";
                break;
            case 4:
                myFigure = "Natural_Turn_456";
                break;
            case 5:
                myFigure = "Natural_Turn_Outside_Partner";
                break;
            case 6:
                myFigure = "Open_Impetus";
                break;
            case 7:
                myFigure = "Open_Telemark";
                break;
            case 8:
                myFigure = "Weave_From_PP";
                break;
            case 9:
                myFigure = "Whisk";
                break;
            case 10:
                myFigure = "Chasse_From_PP";
                break;
            case 11:
                myFigure = "Chasse_To_Right";
                break;
            case 12:
                myFigure = "Closed_Telemark";
                break;
            case 13:
                myFigure = "Hesitation";
                break;
            case 14:
                myFigure = "Outside_Change";
                break;
            case 15:
                myFigure = "Reverse_Closed_Change_123";
                break;
            case 16:
                myFigure = "Reverse_Closed_Change_456";
                break;
            case 17:
                myFigure = "Reverse_Turn_123";
                break;
            case 18:
                myFigure = "Reverse_Turn_456";
                break;
            case 19:
                myFigure = "Spin_Turn_Under_Turned";
                break;
        }
    }
}
