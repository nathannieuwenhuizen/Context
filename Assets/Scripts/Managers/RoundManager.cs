using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RoundManager : MonoBehaviour
{
    //what the round is
    private Round cRound;
    //holds the value on how far the round is going
    private int progressState = 0;

    [Header("general objects")]
    [SerializeField] GameObject nextButton;
    [SerializeField] private FadeScreen fadeScreen;
    private AudioSource audioS;

    [Header("dialogue")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject dialogueObject;
    [SerializeField] private Text oberNameText;


    [Header("personal answer objects")]
    [SerializeField] private GameObject personalAnswerScreen;
    [SerializeField] private Text personText;
    [SerializeField] private InputField personalAnswerField;
    [SerializeField] private Slider personalAnswerSlider;
    [SerializeField] private Text personalStellingText;
    [SerializeField] private Text storyText;

    [Header("Spinning bottle round")]
    [SerializeField] private GameObject bottleScreen;
    [SerializeField] private SpinningBottle spinningBottle;

    [Header("Team final round")]
    [SerializeField] private GameObject teamScreen;
    [SerializeField] private Text teamEensNames;
    [SerializeField] private Text teamOneensNames;
    private float finalAnswer;


    [Header("Ober object")]
    [SerializeField] private GameObject oberObject;
    private bool inPersonalAnswerMode = false;
    private int answerIndex = 0;


    [Header("timer and final answer")]
    [SerializeField] private CountDownTimer timer;
    [SerializeField] private GameObject allAnswerScreen;
    [SerializeField] private Text allAnswersField;
    [SerializeField] private Text finallStellingText;

    private bool inFinalAnswerMode = false;
    private bool timesUp = false;


    [Header("choosing another round objects")]
    [SerializeField] private GameObject chooseScreen;
    private bool newRound;
    

    void Start()
    {
        cRound = new Round();
        cRound.stelling = (int)Mathf.Floor(Random.Range(0, CardData.stories.Count));
        cRound.answers = new List<float> { };

        audioS = GetComponent<AudioSource>();

        SessionData.CSESSION = new Session();

        oberNameText.text = SessionData.CSESSION.character == 0 ? SessionData.Melissa : SessionData.John;
        storyText.text = CardData.stories[cRound.stelling].begin + "\n\n" + CardData.stories[cRound.stelling].question;


        StartCoroutine(FirstDialogue());
    }
    public IEnumerator FirstDialogue()
    {
        yield return new WaitForSeconds(.2f);
        dialogueManager.StartDialogue(DialogueData.Round1Is, false, null);

    }
    public void NextButtonClicked()
    {
        Debug.Log(progressState);
        audioS.Play();

        if (inPersonalAnswerMode) {
            PersonAnswered();
            return;
        }
        if (inFinalAnswerMode)
        {
            return;
        }

        if (dialogueManager.InDialogue)
        {
            return;
        }


        progressState++;
        switch (progressState)
        {
            case 1:
                //situatie wordt verteld
                dialogueManager.StartDialogue(DialogueData.getDialogueFromString(CardData.stories[cRound.stelling].begin + CardData.stories[cRound.stelling].question), false, null);
                break;
            case 2:
                //players fill in their answers----------------

                //personal mode is activated
                inPersonalAnswerMode = true;

                personalStellingText.text = CardData.stories[cRound.stelling].begin + "\n\n" + CardData.stories[cRound.stelling].question;

                dialogueObject.SetActive(false);
                PersonToAnswer();
                nextButton.SetActive(false);
                break;
            case 3:
                //ober talks and shows bubbles of opinions--------

                //hide personal answer screen
                personalAnswerScreen.SetActive(false);
                nextButton.SetActive(true);


                //show ober
                oberObject.SetActive(true);

                //next dialogue with callback
                dialogueManager.StartDialogue(DialogueData.AfterRound1, false, null);
                break;
            case 4:
                //show the opinions.
                string answers = "";
                for (int i = 0; i < cRound.answers.Count; i++)
                {
                    answers += SessionData.CSESSION.players[i].name + ": \n" + DialogueData.NumberToText(cRound.answers[i]) + "\n\n";
                }
                answers += "Gemiddelde mening: \n" + DialogueData.NumberToText(GetAverageOpinion());
                answers += "\n\n";
                dialogueManager.StartDialogue(DialogueData.getDialogueFromString(answers), false, null);
                break;
            case 5:
                dialogueManager.StartDialogue(DialogueData.Round2Is, false, null);
                break;
            case 6:
                //spinning bottle round active
                bottleScreen.SetActive(true);
                nextButton.SetActive(false);
                break;
            case 7:
                //after spin round text appears that explains the final round
                bottleScreen.SetActive(false);
                nextButton.SetActive(true);

                dialogueManager.StartDialogue(DialogueData.Round3Is, false, null);
                break;
            case 8:
                //team round screen
                nextButton.SetActive(false);
                teamScreen.SetActive(true);

                //seeting random names
                teamEensNames.text = teamOneensNames.text = "";
                List<Player> shuffeledNames = RoundManager.Randomize(SessionData.CSESSION.players);
                for (int i = 0; i < shuffeledNames.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        teamEensNames.text += shuffeledNames[i].name + "\n\n";
                    }
                    else
                    {
                        teamOneensNames.text += shuffeledNames[i].name + "\n\n";
                    }
                }
                break;
            case 9:
                //after round screen select screen
                nextButton.SetActive(true);
                teamScreen.SetActive(false);

                string AverageOpinion = DialogueData.NumberToText(GetAverageOpinion());
                string FinalOpinion = DialogueData.NumberToText(finalAnswer);
                string resultComparedToAverage = "Wat interessant is dat jullie als gemiddelide mening " + AverageOpinion + " hebben, en jullie eindigen met de mening " + FinalOpinion + ".";
                string ResultDialogue = "Ik ben blij dat jullie tot een gezamelijke mening zijn uitgekomen.. " + resultComparedToAverage + ". Dit is het einde van deze ronde. Willen jullie nog een ronde spelen?";

                dialogueManager.StartDialogue(DialogueData.getDialogueFromString(ResultDialogue), false, () =>
                {
                    nextButton.SetActive(false);
                    chooseScreen.SetActive(true);
                });
                break;
            case 10:
                //last text before round ends
                chooseScreen.SetActive(false);
                nextButton.SetActive(true);
                dialogueManager.StartDialogue(newRound ? DialogueData.NewRoundChosen : DialogueData.EndOfSession, true, () =>
                {
                    StartCoroutine( GoToScene(newRound ? 1 : 0));
                });
                break;
        }
    }
    private IEnumerator GoToScene(int val)
    {
        fadeScreen.FadeTo(1f, 0.5f);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(val);

    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PersonToAnswer()
    {
        Debug.Log("personal should be active");
        //personal answer screen is visible and the values reset and update for gthe person based on index
        personalAnswerScreen.SetActive(true);
        personText.text = SessionData.CSESSION.players[answerIndex].name;
        personalAnswerField.text = "";
        personalAnswerSlider.value = .5f;
    }

    public void FinalAnswerDone(int val) { 
        switch (val)
        {
            case 0:
                finalAnswer = 0f;
                break;
            case 1:
                finalAnswer = 1f;
                break;
            case 5:
                finalAnswer = .5f;
                break;
        }
        NextButtonClicked();
    }
    //check timer
    private IEnumerator CheckTimer()
    {
        while (inFinalAnswerMode)
        {
            if (timer.TimeCount <= 0) { timesUp = true; }
            yield return new WaitForFixedUpdate();
        }
    }

    public void PersonAnswered()
    {
        if (personalAnswerField.text == "")
        {
            //return;
        }
        cRound.answers.Add(personalAnswerSlider.value);

        answerIndex++;
        if (answerIndex < SessionData.CSESSION.players.Count)
        {
            PersonToAnswer();
        }
        else
        {
            inPersonalAnswerMode = false;
            NextButtonClicked();
        }
    }
    public void NewRound(bool val)
    {
        newRound = val;
        dialogueManager.InDialogue = false;
        NextButtonClicked();
    }
    public class Round
    {
        public int stelling;
        public CardSet cardSet;
        public string finalAnswer;
        public List<float> answers;
    }
    public class CardSet
    {
        
    }
    public class Card
    {
        public int type;
        public string content;
    }
    public static List<T> Randomize<T>(List<T> list)
    {
        List<T> randomizedList = new List<T>();
        System.Random rnd = new System.Random();
        
        while (list.Count > 0)
        {
            int index = rnd.Next(0, list.Count); //pick a random item from the master list
            randomizedList.Add(list[index]); //place it at the end of the randomized list
            list.RemoveAt(index);
        }
        return randomizedList;
    }
    public float GetAverageOpinion()
    {
        float val = 0;
        for (int i = 0; i < cRound.answers.Count; i++)
        {
            val += cRound.answers[i];
        }
        val /= cRound.answers.Count;
        return val;
    }
}
