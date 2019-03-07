using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    private Round cRound;
    private int progressState = 0;

    [Header("general objects")]
    [SerializeField] GameObject nextButton;
    [SerializeField] Text stellingObject;
    [SerializeField] GameObject opinionBubble;
    List<GameObject> bubbles;

    [Header("dialogue")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject dialogueObject;


    [Header("personal answer objects")]
    [SerializeField] private GameObject personalAnswerScreen;
    [SerializeField] private Text personText;
    [SerializeField] private InputField personalAnswerField;
    [SerializeField] private Text personalStellingText;

    [Header("Ober object")]
    [SerializeField] private GameObject oberObject;
    private bool inPersonalAnswerMode = false;
    private int answerIndex = 0;


    [Header("timer and final answer")]
    [SerializeField] private CountDownTimer timer;
    [SerializeField] private GameObject allAnswerScreen;
    [SerializeField] private Text allAnswersField;
    [SerializeField] private InputField finalAnswerField;
    [SerializeField] private Text finallStellingText;

    private bool inFinalAnswerMode = false;
    private bool timesUp = false;


    [Header("choosing another round objects")]
    [SerializeField] private GameObject chooseScreen;
    private bool newRound;


    void Start()
    {
        cRound = new Round();
        cRound.answers = new List<string> { };

        stellingObject.gameObject.SetActive(true);
        stellingObject.text = "";

        dialogueManager.StartDialogue(DialogueData.getDialogueFromString(CardData.stories[0].begin + CardData.stories[0].question), false, null);
        SessionData.CSESSION = new Session();
        //StartCoroutine(SpawnOpinionBubbles());
    }

    public void NextButtonClicked()
    {
        if(inPersonalAnswerMode) {
            PersonAnswered();
            return;
        }
        if (inFinalAnswerMode)
        {
            FinalAnswered();
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
                //players fill in their answers----------------

                //personal mode is activated
                inPersonalAnswerMode = true;

                stellingObject.gameObject.SetActive(false);
                personalStellingText.text = CardData.stories[0].question;

                dialogueObject.SetActive(false);
                PersonToAnswer();
                break;
            case 2:
                //ober talks and shows bubbles of opinions--------

                //hide personal answer screen
                personalAnswerScreen.SetActive(false);

                //show ober
                oberObject.SetActive(true);

                //next dialogue with callback
                dialogueManager.StartDialogue(DialogueData.AfterAnswers, true, () => SetTimerAndScreenReady());
                break;
            case 3:
                //hide final answer screen
                allAnswerScreen.SetActive(false);
                hideBubbles();

                //show ober
                oberObject.SetActive(true);

                //after final round
                dialogueManager.StartDialogue(DialogueData.getDialogueFromString(DialogueData.getRandomAnswerResponse() + CardData.stories[0].punchline + ". Wil je nog een ronde spelen?"), false, () => {
                    chooseScreen.SetActive(true);
                    nextButton.SetActive(false);
                });

                break;
            case 4:
                chooseScreen.SetActive(false);
                dialogueManager.StartDialogue(newRound ? DialogueData.NewRoundChosen : DialogueData.EndOfSession, true, () =>
                {
                    SceneManager.LoadScene(newRound ? 1 : 0);
                });
                break;
        }
    }
    public void PersonToAnswer()
    {
        Debug.Log("personal should be active");
        //personal answer screen is visible and the values reset and update for gthe person based on index
        personalAnswerScreen.SetActive(true);
        personText.text = SessionData.CSESSION.players[answerIndex] + "'s opinion...";
        personalAnswerField.text = "";

        //hide ober
        oberObject.SetActive(false);
    }

    public void SetTimerAndScreenReady ()
    {
        finallStellingText.text = "";

        inFinalAnswerMode = true;

        //set timer
        timer.TimeCount = SessionData.CSESSION.timePerRound * 60; //min to sec
        timer.IsRunning = true;
        StartCoroutine(CheckTimer());

        //all answers are showing
        allAnswerScreen.SetActive(true);
        /*
        string allAnsers = "";
        for (int i = 0; i < cRound.answers.Count; i++)
        {
            allAnsers += cRound.answers[i] + "\n";
        }
        allAnswersField.text = allAnsers;*/

        //hide ober
        oberObject.SetActive(false);
        StartCoroutine(SpawnOpinionBubbles());

    }

    public IEnumerator SpawnOpinionBubbles()
    {
        bubbles = new List<GameObject> { };
        List<string> allAnswers = cRound.answers;
        //List<string> allAnswers = new List<string> { "a", "b", "c", "d"};
        Debug.Log("nani?");
        while (allAnswers.Count > 0)
        {
            int randomVal = (int)Mathf.Floor(Random.value * allAnswers.Count);
            Debug.Log("nani?" + randomVal);

            GameObject tmpBubble = GameObject.Instantiate(opinionBubble, new Vector2(-4f, 0), Quaternion.identity);

            bubbles.Add(tmpBubble);

            yield return new WaitForSeconds(0.01f);
            tmpBubble.GetComponent<Bubble>().SetupText(allAnswers[randomVal]); //must be later because of instantiation of the ui text :(

            yield return new WaitForSeconds(0.5f);
            allAnswers.RemoveAt(randomVal);

        }
    }
    public void hideBubbles()
    {
        foreach (GameObject bubble in bubbles)
        {
            bubble.GetComponent<Bubble>().SetActive(false);
        }
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
            return;
        }
        cRound.answers.Add(personalAnswerField.text);

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
    public void FinalAnswered()
    {
        Debug.Log("final answer" + finalAnswerField.text);
        //check if the field isnt empty
        if (finalAnswerField.text == "") { return; }

        //update
        cRound.finalAnswer = finalAnswerField.text;
        inFinalAnswerMode = false;
        NextButtonClicked();

    }
    public void NewRound(bool val)
    {
        newRound = val;
        NextButtonClicked();
    }
    public class Round
    {
        public string stelling;
        public CardSet cardSet;
        public string finalAnswer;
        public List<string> answers;
    }
    public class CardSet
    {
        
    }
    public class Card
    {
        public int type;
        public string content;
    }
}
