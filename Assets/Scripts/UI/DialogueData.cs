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
    public static DialogueStruct[] AfterRound1 = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Jullie hebben het snel ingevuld!", false),
        new DialogueStruct(Ober, "Klik op het boek icoontje om de situatie terug te lezen.", false),
        new DialogueStruct(Ober, "Ik ben erg benieuwd naar jullie antwoorden!", false),
        new DialogueStruct(Ober, "Wanneer jullie de antwoorden hebben doorgelezen, gaan we door naar de tweede ronde!", false),
    };
    public static DialogueStruct[] Round2Is = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Nu komt de tweede ronde! YAY!", false),
        new DialogueStruct(Ober, "In deze ronde draaien we met de fles. Maximaal 3 keer.", false),
        new DialogueStruct(Ober, "Wanneer deze ophoud met draaien is de aangewezen persoon aan de beurt. Zij/hij mag zijn/haar mening verdedigen. ", false),
        new DialogueStruct(Ober, "Neem gerust de tijd.", false),
    };
    public static DialogueStruct[] Round3Is = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Ik hoop dat jullie interessante argumenten hebben gehoord.", false),
        new DialogueStruct(Ober, "Nu komt de beslissende ronde.", false),
        new DialogueStruct(Ober, "Ik ga jullie opdelen in twee teams.", false),
        new DialogueStruct(Ober, "Een team 'Eens'.", false),
        new DialogueStruct(Ober, "En natuurlijk...", false),
        new DialogueStruct(Ober, "Een team 'Oneens'.", false),
        new DialogueStruct(Ober, "Jullie gaan deze meningen verdedigen en komen samen tot een uiteindelijke conclusie.", false),
        new DialogueStruct(Ober, "Laten we beginnen!", false),
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
