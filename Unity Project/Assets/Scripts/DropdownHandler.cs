using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public Dropdown myDropdown;

    public static string fileName;

    public static string lastFigure = "";

    public List<string> stateNames;

    Animator animator;

    void Awake() { init(); }

    void init()
    {
        animator = GetComponent<Animator>();

        stateNames = new List<string>();
        var clips = animator.runtimeAnimatorController.animationClips;

        foreach (var clip in clips)
        {
            var stateName = clip.name;
            if (clip.name != "BasePose")
            {
                stateNames.Add(stateName);
            }
        }
        myDropdown.AddOptions(stateNames);
    }

    void Start()
    {
        myDropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes
        myDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(myDropdown);
        });
    }

    void DropdownValueChanged(Dropdown change)
    {
        lastFigure = change.options[change.value].text;
    }

    public void updateDropdown()
    {
        var figureNames = new List<string>();

        if (lastFigure != null)
        {
            string fileName = lastFigure + ".json";
            string figurePath = Path.Combine(Application.dataPath + "/Resources/Text/", fileName);
            string[] figures = File.ReadAllLines(figurePath);
            myDropdown.ClearOptions();

            foreach (var figure in figures)
            {
                figureNames.Add(figure);
            }
            myDropdown.AddOptions(figureNames);
        }
    }
}
