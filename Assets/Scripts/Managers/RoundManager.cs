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
    [SerializeField] Text stellingObject;
    [SerializeField] private FadeScreen fadeScreen;
    [SerializeField] GameObject opinionBubble;
    private List<Bubble> bubbles;
    private AudioSource audioS;

    [Header("dialogue")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject dialogueObject;
    [SerializeField] private Text oberNameText;


    [Header("personal answer objects")]
    [SerializeField] private GameObject personalAnswerScreen;
    [SerializeField] private Text personText;
    [SerializeField] private InputField personalAnswerField;
    [SerializeField] private Text personalStellingText;
    [SerializeField] private Text storyText;

    [Header("Ober object")]
    [SerializeField] private GameObject oberObject;
    private bool inPersonalAnswerMode = false;
    private int answerIndex = 0;


    [Header("timer and final answer")]
    [SerializeField] private CountDownTimer timer;
    [SerializeField] private GameObject allAnswerScreen;
    [SerializeField] private Text allAnswersField;
    [SerializeField] private Text finallStellingText;
    [SerializeField] private ParticleSystem bubbleParticle;

    private bool inFinalAnswerMode = false;
    private bool timesUp = false;


    [Header("choosing another round objects")]
    [SerializeField] private GameObject chooseScreen;
    private bool newRound;


    void Start()
    {
        cRound = new Round();
        cRound.stelling = (int)Mathf.Floor(Random.Range(0, CardData.stories.Count));
        cRound.answers = new List<string> { };

        stellingObject.gameObject.SetActive(true);
        stellingObject.text = "";
        bubbleParticle.enableEmission = false;
        audioS = GetComponent<AudioSource>();

        dialogueManager.StartDialogue(DialogueData.getDialogueFromString(CardData.stories[cRound.stelling].begin + CardData.stories[cRound.stelling].question), false, null);
        //SessionData.CSESSION = new Session();

        //StartCoroutine(SpawnOpinionBubbles());

        oberNameText.text = SessionData.CSESSION.character == 0 ? SessionData.Melissa : SessionData.John;

    }

    public void NextButtonClicked()
    {
        Debug.Log(progressState);

        if (inPersonalAnswerMode) {
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

        audioS.Play();

        progressState++;
        switch (progressState)
        {
            case 1:
                //players fill in their answers----------------

                //personal mode is activated
                inPersonalAnswerMode = true;

                storyText.text = CardData.stories[cRound.stelling].begin + "\n\n" + CardData.stories[cRound.stelling].question;

                stellingObject.gameObject.SetActive(false);
                personalStellingText.text = CardData.stories[cRound.stelling].begin + "\n\n" + CardData.stories[cRound.stelling].question;

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
                dialogueManager.StartDialogue(DialogueData.getDialogueFromString(DialogueData.getRandomAnswerResponse() + CardData.stories[cRound.stelling].punchline + ". Wil je nog een ronde spelen?"), false, () => {
                    chooseScreen.SetActive(true);
                    nextButton.SetActive(false);
                });

                break;
            case 4:
                chooseScreen.SetActive(false);
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
    public void PersonToAnswer()
    {
        Debug.Log("personal should be active");
        //personal answer screen is visible and the values reset and update for gthe person based on index
        personalAnswerScreen.SetActive(true);
        personText.text = SessionData.CSESSION.players[answerIndex].name;
        personalAnswerField.text = "";
        Handheld.Vibrate();

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
        bubbleParticle.enableEmission = true;
        bubbles = new List<Bubble> { };
        List<string> allAnswers = cRound.answers;
        //List<string> allAnswers = new List<string> { "a", "b", "c", "d", "e", "f"};
        List<int> playerIndexes = new List<int> { };
        for (int j = 0; j < allAnswers.Count; j++)
        {
            playerIndexes.Add(j);
        }
        playerIndexes = RoundManager.Randomize(playerIndexes);


        List<Vector2> positions = new List<Vector2>
        {
            new Vector2(-1.2f,2.5f),
            new Vector2(1.2f,2.8f),
            new Vector2(-1.2f,0f),
            new Vector2(1.2f,0.3f),
            new Vector2(-1.2f,-2.5f),
            new Vector2(1.2f,-2.2f),
        };
        for (int i = 0; i < playerIndexes.Count; i++)
        {
            Debug.Log("index" + playerIndexes[ i]);

            GameObject tmpBubble = GameObject.Instantiate(opinionBubble, new Vector2(-4f, 0), Quaternion.identity);

            bubbles.Add(tmpBubble.GetComponent<Bubble>());
            tmpBubble.GetComponent<Bubble>().goToPos(positions[i]);

            yield return new WaitForSeconds(0.01f);
            tmpBubble.GetComponent<Bubble>().Text = allAnswers[playerIndexes[i]]; //must be later because of instantiation of the ui text :(
            tmpBubble.GetComponent<Bubble>().FromPlayer = playerIndexes[i];
            tmpBubble.GetComponent<Bubble>().DisplayCrown(SessionData.HasHighestScore(playerIndexes[i]));

            yield return new WaitForSeconds(0.5f);
        }
        bubbleParticle.enableEmission = false;

    }
    public void hideBubbles()
    {
        foreach (Bubble bubble in bubbles)
        {
            bubble.SetActive(false);
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
        Bubble chosenBubble = ChosenBubble();
        if (chosenBubble == null) { return; }

        //update
        cRound.finalAnswer = chosenBubble.Text;

        //winner gets a point
        Player winner = SessionData.CSESSION.players[chosenBubble.FromPlayer];
        winner.score += 1;
        SessionData.CSESSION.players[chosenBubble.FromPlayer] = winner;

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
        public int stelling;
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
    public Bubble ChosenBubble()
    {
        foreach (Bubble bubble in bubbles)
        {
            if (bubble.Clicked)
            {
                return bubble;
            }
        }
        return null;
    }
}
