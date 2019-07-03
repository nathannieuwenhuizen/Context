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
        new DialogueStruct(Ober, "Hallo alle spelers en bezoekers! Ik wil jullie verwelkomen bij...", false),
        new DialogueStruct(Ober, "het Bluebell Cafe!", false),
        new DialogueStruct(Ober, "Laten we samen een leuk spel spelen!", false),
    };



    /// <summary>
    /// round dialogue-----------------------------------------
    /// </summary>

    public static DialogueStruct[] Round1Is = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Dit spel wordt opgedeeld in rondes.", false),
        new DialogueStruct(Ober, "Per ronde krijgt een speler een stelling te zien. Hij of zij spreekt deze stelling uit aan de groep en kondigt zijn/haar mening. Daarna drukt hij/zij op de knop voordat de tijd op is.", false),
        new DialogueStruct(Ober, "Kies nu een speler aan die wilt beginnen, die houdt deze mobiel vast", false),
        new DialogueStruct(Ober, "Je krijgt nu de stelling te zien", false),
    };
    public static DialogueStruct[] Round2Is = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "In de tweede ronde krijgen jullie weer je stellingen te zien, maar dit keer mogen de andere spelers vragen stellen over jouw mening en argumenten.", false),
        new DialogueStruct(Ober, "Het is de bedoeling om zo veel mogelijk vragen te kunnen beantwoorden voordat de tijd op is.", false),
        new DialogueStruct(Ober, "Als je een vraag wilt stellen, druk je op de grote witte knop", false),
    };

    public static DialogueStruct[] NewRoundChosen = new DialogueStruct[]
        {
        new DialogueStruct(Ober, "Awesome!", false),
        new DialogueStruct(Ober, "Hier komt de volgende ronde!", false),
    };

    public static DialogueStruct[] EndOfSession = new DialogueStruct[]
        {
        new DialogueStruct(Ober, "Bedankt voor het spelen van Cafe Bluebell! Ik wens jullie een fijne dag!", false),
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
