using System.Collections.Generic;

public static class CardData
{

    public static List<string> stellingen = new List<string>
    {
        "Websites die vreemdgaan promoten (zoals Second Love) zouden verboden moeten worden",
        "De overheid moet “catfish” – i.e. personen die online relaties aangaan met een valse of misleidende identiteit – actief opsporen en vervolgen",
        "De media mogen pas verhalen over seksueel misbruik publiceren als de daders veroordeeld zijn",
        "Overal ter wereld mogen meisjes thuis blijven als hun school geen maandverband verstrekt",
        "Alle wc’s op scholen moeten genderneutraal worden",
        "De politie moet voor 50% uit vrouwen bestaan",
        "Debatteren drijft mensen uit elkaar",
        "Het drinken van alcohol tijdens de zwangerschap moet gelijk worden gesteld aan kindermishandeling",
        "Het overdragen van een SOA moet strafbaar worden",
        "Het WK Vrouwenvoetbal moet meer aandacht krijgen in Nederland",
        "Je mag alleen meedoen aan missverkiezingen als je nooit plastische chirurgie hebt ondergaan",
        "Make-up is liegen over je uiterlijk",
        "Mannen boven de 50 zouden geen kinderen mogen krijgen",
        "Mannen die werken met jonge kinderen moeten altijd onder toezicht staan",
        "Mannen en vrouwen moeten de zorg van hun kinderen gelijk verdelen",
        "Mensen die roken moeten een hogere verzekering precentage krijgen",
        "Op elke school moet coming-out dag gevierd worden",
        "Homoseksualiteit moet worden uitgelegd worden op de basisscholen",
        "Aftrekken staat gelijk aan massamoord",
        "Een relatie hebbne met een transgender van je eigen geslacht is homoseksueel",
        "Naaktheid op TV moet genormaniseerd worden",
        "Seksualiteit op social media moet gecensureerd worden",
        "Afrikaanse immigranten zijn sneller geneigd naar seksueel geweld",

    };

    public static List<Story> stories = new List<Story>
    {
        /*new Story(
            "",
            "",
            "",
            ""),*/
        new Story(
            "Twee vrienden van mij zijn begonnen met daten. Zij had een SOA en had dit duidelijk aan hem verteld. Ondanks dat ze samen veilig seks hadden, is hij toch besmet geraakt met de SOA. Hij is heel erg boos op haar. ",
            "Vind jij zijn reactie normaal?",
            "",
            ""),
        /*new Story(
            "Ik kreeg laatst dit verhaal te horen van iemand. Hij vertelde dat hij al een tijdje in een relatie zat met zijn vriendin met regelmatig seksueel contact. Door een slechte ervaring hiervoor te hebben meegemaakt met iemand anders vertelde hij haar dat hij liever niet meer sex zou willen hebben. Haar manier hoe zij naar hem opkeek als “macho” zorgde ervoor dat ze niet openstond voor zijn keuze en verliet hem daarna. ",
            "Wat vindt je van deze situatie?",
            "",
            ""),
        new Story(
            "Laatst kwam een vriendin van me in het cafe en kreeg ik dit te horen. Haar dochter zat al langere tijd met een vriend in een relatie en hadden naaktfotos onderling naar elkaar gestuurd. Haar vriend ging uit met zijn vriendengroep en liet terwijl dronken de foto zien aan zijn vrienden. Ze pakte de telefoon en poste het vervolgens op social media wat negatieve publiciteit aan haar gaf. ",
            "Wat vindt je van deze situatie?",
            "",
            ""),
        new Story(
            "De vorige keer had ik zelf een interessante ervaring hier in het cafe. Laatst kwam een vriendin van me met een aantal kennissen in cafe Bluebell. Ik geef zelf altijd een hand aan iedereen maar 1 vriendin wou mij een hand vol wangkussen geven waar ik niet comfortabel mee voelde. Ik besloot om af te staan dat ze erom vroeg maar ik werd alsnog door haar meegetrokken bij het wang kussen. ",
            "Wat vindt je van deze situatie?",
            "",
            ""),
        new Story("Ik ken een persoon die vaak naar festival gaat en hij zei tegen mij het volgende. “Ik was naar een festival gegaan, en toen stond ik naar een band te kijken. en toen kwam er een wijf aan die aan me zat te twerken maar ik vond dat niet leuk. Dus ik probeerde plaats te maken door weg te stappen. Maar ze bleef me volgen..”. ", 
            "Wat zou je doen in deze situatie?",
            "Jullie hebben waarschijnlijk een beter antwoord dat wat hij deed. Namelijk, hij zij: Ik heb haar naar een moshpit geleid en  erin geduwd", 
            "Is dit een hulplijn ofzo"),*/
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
