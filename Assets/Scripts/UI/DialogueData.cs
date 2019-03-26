using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public static DialogueStruct[] IntroductionHost = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Hallo alle spelers en bezoekers! Ik wil jullie verwelkomen aan...", false),
        new DialogueStruct(Ober, "het Bluebell Cafe!", false),
        new DialogueStruct(Ober, "Laten we samen een leuke spel beginnen!", false),
    };



    /// <summary>
    /// round dialogue-----------------------------------------
    /// </summary>

    public static DialogueStruct[] Round1Is = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Deze spel wordt ingedeeld in drie rondes.", false),
        new DialogueStruct(Ober, "In de eerste ronde krijgen jullie een situatie over een gevoelig onderwerp te horen en gaan jullie om de beurd jullie mening in een de slider invoeren.", false),
        new DialogueStruct(Ober, "Zijn jullie er klaar voor?", false),
        new DialogueStruct(Ober, "Okay!", false),
        new DialogueStruct(Ober, "Ik ga nu de situatie vertellen...", false),
    };
    public static DialogueStruct[] AfterRound1 = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Jullie hebben het snel ingevuld!", false),
        new DialogueStruct(Ober, "Jullie kunnen het situatie rechtsboven op het boek icoontje teruglezen", false),
        new DialogueStruct(Ober, "Ik laat nu jullie anwoorden die jullie gegeven hebben weergeven op een lijst", false),
        new DialogueStruct(Ober, "Wanneer jullie andermans antwoorden hebben gelezen, gaan we verder naar de tweede ronde!", false),
    };
    public static DialogueStruct[] Round2Is = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Nu komt de tweede ronde! YAY!", false),
        new DialogueStruct(Ober, "In deze ronde ga ik een fles laten draaien.", false),
        new DialogueStruct(Ober, "Wanneer deze ophoud met draaien moet de persoon waar hij op wijst ( of die het dichtste bij zit ) zijn/haar mening te verdedigen. ", false),
        new DialogueStruct(Ober, "Jullie kunnen gerust de tijd nemen!", false),
    };
    public static DialogueStruct[] Round3Is = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Ik hoop dat de fles ronde leuk was.", false),
        new DialogueStruct(Ober, "Nu komt de beslissende ronde.", false),
        new DialogueStruct(Ober, "Ik ga jullie zo in twee teams verdelen.", false),
        new DialogueStruct(Ober, "Een team 'Eens'.", false),
        new DialogueStruct(Ober, "En natuurlijk...", false),
        new DialogueStruct(Ober, "Een team 'Oneens'.", false),
        new DialogueStruct(Ober, "Jullie gaan voor jullie gekozen mening een discusie voeren en uiteindelijk gezamelijk tot een antwoord uitkomen.", false),
        new DialogueStruct(Ober, "Laten we  beginnen!", false),
    };
    public static DialogueStruct[] NewRoundChosen = new DialogueStruct[]
        {
        new DialogueStruct(Ober, "Awesome!", false),
        new DialogueStruct(Ober, "Hier komt de volgende ronde!", false),
    };

    public static DialogueStruct[] EndOfSession = new DialogueStruct[]
        {
        new DialogueStruct(Ober, "Dank voor het spelen van Cafe Bluebell! Fijne dag nog toegewenst!", false),
    };


    //=-------------------------------------------------------=


    public static DialogueStruct[] getDialogueFromString(string val)
    {
        string[] lines = WholeLineToSepereateLines(val);

        DialogueStruct[] result = new DialogueStruct[lines.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new DialogueStruct(Ober, lines[i], false);
        }
        return result;
    }
    public static string[] WholeLineToSepereateLines(string val)
    {
        return val.Split(new string[] { ". " }, System.StringSplitOptions.None);
    }

    public static string getRandomAnswerResponse()
    {
        List<string> vals = new List<string>
        {
            "Zo! Dat is best een interesant antwoord.... ",
            "Mooi antwoord. ",
            "Goed beantwoord!. ",
            "Jullie antwoord zet me op denken. ",
            "Aha, jullie zijn eruit gekomen, goed gedaan!.  ",
        };
        return vals[Random.Range(0, vals.Count- 1)];
    }
    public static string NumberToText(float val)
    {
        string result = "";
        if (val < .2)
        {
            result = "hellemaal oneens!";
        } else if (val < .4)
        {
            result = "beetje oneens";
        }
        else if (val < .6)
        {
            result = "neutraal hierover";
        }
        else if (val< .8)
        {
            result = "beetje mee eens";
        }
        else
        {
            result = "hellemaal mee eens!";
        }
        return result;
    }


}
