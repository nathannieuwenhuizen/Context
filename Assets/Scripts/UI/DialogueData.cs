using UnityEngine;
using System.Collections;

public struct DialogueStruct
{
    public DialogueStruct(string _name, string _line, bool _allignmentRight)
    {
        name = _name;
        line = _line;
        allignmentRight = _allignmentRight;
    }
    public string name;
    public string line;
    public bool allignmentRight;
}

public class DialogueData {
    public static string Ober = "Ober #007";
    public static string UI = "Info";

    /// <summary>
    /// Tutorial dialogue-----------------------------------------
    /// </summary>
    public static DialogueStruct[] ChooseHost = new DialogueStruct[]
    {
        new DialogueStruct(UI, "Choose your host to play this session...", false),
    };
    public static DialogueStruct[] IntroductionHost = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Hello everybody, my name is " +  Ober + ", and I welcome you to...", false),
        new DialogueStruct(Ober, "Bluebell Cafe!", false),
        new DialogueStruct(Ober, "Before we begin with your card game, would you please select how long each round would last?", false),
    };

    public static DialogueStruct[] BeginTheRound = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Don't worry, you can alter the time of each round whenever you want.", false),
        new DialogueStruct(Ober, "Let's begin the game!", false),
    };


    /// <summary>
    /// Mainworld dialogue-----------------------------------------
    /// </summary>
}
