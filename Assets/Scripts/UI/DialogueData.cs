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
    /// session dialogue-----------------------------------------
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
    /// round dialogue-----------------------------------------
    /// </summary>
    public static DialogueStruct[] RoundIs = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "This is the discussion of this round.", false),
        new DialogueStruct(Ober, "Now each of you will write their opinion in turn.", false),
        //new DialogueStruct(Ober, "Then we'll bring all those anwsers back and you get " + SessionData.CSESSION.timePerRound + " minutes to finalize the answer of the group.", false),
        new DialogueStruct(Ober, "Are you all ready?", false),
    };
    public static DialogueStruct[] AfterAnswers = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Now comes the answers you've filled in to the screen.", false),
        new DialogueStruct(Ober, "When that happens, the timer will start counting", false),
        new DialogueStruct(Ober, "Good luck!", false),
    };
    public static DialogueStruct[] AfterRoundWin = new DialogueStruct[]
        {
        new DialogueStruct(Ober, "You all got a final answer! Great!", false),
        new DialogueStruct(Ober, "Would you like to play another round?", false),
    };
    public static DialogueStruct[] AfterRoundLose = new DialogueStruct[]
        {
        new DialogueStruct(Ober, "You didn't ogt it in time...", false),
        new DialogueStruct(Ober, "Aw...", false),
        new DialogueStruct(Ober, "Wanna try again?", false),
    };

    public static DialogueStruct[] NewRoundChosen = new DialogueStruct[]
        {
        new DialogueStruct(Ober, "Awesome!", false),
        new DialogueStruct(Ober, "Here comes the next round.", false),
    };

    public static DialogueStruct[] EndOfSession = new DialogueStruct[]
        {
        new DialogueStruct(Ober, "Have a great day!", false),
    };



}
