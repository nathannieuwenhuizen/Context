using System.Collections.Generic;

public static class CardData
{

    public static string testDiscussion = "Artiesten die vrouw-onvriendelijke uitspraken doen moeten worden geboycot";

    public static List<Story> stories = new List<Story>
    {
        /*new Story(
            "", 
            "", 
            "", 
            ""),*/
        new Story("Ik ken een persoon die vaak naar festival gaat en hij zei tegen mij het volgende. “Ik was naar een festival gegaan, en toen stond ik naar een band te kijken. en toen kwam er een wijf aan die aan me zat te twerken maar ik vond dat niet leuk. Dus ik probeerde plaats te maken door weg te stappen. Maar ze bleef me volgen..”. ", 
            "Wat zou je doen in deze situatie?",
            "Jullie hebben waarschijnlijk een beter antwoord dat wat hij deed. Namelijk, hij zij: Ik heb haar naar een moshpit geleid en  erin geduwd", 
            "Dit is een hulplijn ofzo"),
    };

    public struct Story
    {
        public Story(string _begin, string _question, string _punchline = "", string _help ="")
        {
            begin = _begin;
            question = _question;
            punchline = _punchline;
            help = _help;
        }
        public string begin;
        public string question;
        public string punchline;
        public string help;
    }

}
