using System.Collections.Generic;
using System.Text.RegularExpressions;
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

    Dictionary <string, string[]> rules;

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

        initializeRulesDictionary();
    }

    void initializeRulesDictionary(){
        rules = new Dictionary<string, string[]>();

        foreach(var name in stateNames) {
            var file = Resources.Load<TextAsset>("Text/" + name);
            string[] lines = Regex.Split(file.text, "\r\n|\r|\n");

            rules.Add(name, lines);
        }
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
            myDropdown.ClearOptions();

            foreach (var figure in rules[lastFigure])
            {
                figureNames.Add(figure);
            }
            myDropdown.AddOptions(figureNames);
        }
    }
}
