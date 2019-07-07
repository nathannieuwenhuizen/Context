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
        new DialogueStruct(Ober, "Hallo, bezoekers! Mijn naam is Melissa, en ik wil jullie graag verwelkomen bij…", false),
        new DialogueStruct(Ober, "… Het Bluebell Cafe!", false),
        new DialogueStruct(Ober, "Voordat we beginnen leg ik jullie de regels binnen het cafe uit.", false),
    };



    /// <summary>
    /// round dialogue-----------------------------------------
    /// </summary>

    public static DialogueStruct[] Round1Is = new DialogueStruct[]
    {
        new DialogueStruct(Ober, "Iedere ronde wordt in tweeën verdeeld. In de eerste ronde mag een deelnemer zijn mening geven over de stelling die hij toegewezen heeft gekregen.", false),
        new DialogueStruct(Ober, "In ronde twee mogen zijn groepsgenoten vragen stellen over deze mening, of de stelling in het algemeen.", false),
        new DialogueStruct(Ober, "Hou wel de timer in de gaten! Er is een tijdslimiet!", false),
        new DialogueStruct(Ober, "Als je niet aan de beurt bent, luister dan goed naar de persoon die aan het woord is! Wanneer iemand aan het woord is, is het de bedoeling dat de telefoon in het midden van de tafel ligt. ", false),
        new DialogueStruct(Ober, "Wanneer jij vind dat de deelnemer die aan het woord is zijn mening goed uit, of een vraag goed beantwoord, klik dan op de “like” button op het scherm.", false),
        new DialogueStruct(Ober, "Op deze manier verzamel je punten als een groep! ", false),
        new DialogueStruct(Ober, "Dit betekend dat je binnen een ronde meerdere keren op de “like” button mag klikken, mits de deelnemer die aan het woord is meerdere goede argumenten of antwoorden heeft.", false),
        new DialogueStruct(Ober, "Aan het einde van het spel worden jullie totaal aantal punten berekend. ", false),
        new DialogueStruct(Ober, "Het is de bedoeling dat naarmate je het spel vaker speelt dit aantal omhoog gaat omdat jullie steeds beter worden in het beargumenteren van jullie meningen! ", false),
        new DialogueStruct(Ober, "Om het jullie makkelijker te maken hoeven jullie niet zelf te beslissen wie er straks begint.", false),
        new DialogueStruct(Ober, "Dat doet de tool voor jullie! Zodra de eerste deelnemer gekozen is, draaien jullie met de klok mee. Heel veel success!", false),
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
        new DialogueStruct(Ober, "Bedankt voor het spelen van Cafe Bluebell! Ik wens jullie nog een fijne dag!", false),
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
