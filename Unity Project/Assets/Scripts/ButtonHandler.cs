using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public static List<string> listOfFigures = new List<string>(0) { };

    public void createFigureList()
    {
        listOfFigures.Add(DropdownHandler.myFigure);
        GameObject.Find("ChoreographyText").GetComponentInChildren<Text>().text += "\n" + DropdownHandler.myFigure;
    }
}
