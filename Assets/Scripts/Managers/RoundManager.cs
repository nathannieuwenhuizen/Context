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
    [SerializeField] private Text dialogueText;


    [Header("personal answer objects")]
    [SerializeField] private GameObject personalAnswerScreen;
    [SerializeField] private Text personText;
    [SerializeField] private InputField personalAnswerField;
    [SerializeField] private Slider personalAnswerSlider;
    [SerializeField] private Text personalStellingText;
    [SerializeField] private Text storyText;

    [Header("Spinning bottle round")]
    [SerializeField] private GameObject bottleScreen;

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


    [Space]
    [Header("new screens")]
    [SerializeField] private StellingScreen stellingScreen;
    [SerializeField] private QuestionScreen questionScreen;
    [SerializeField] private ResultScreen resultScreen;


    public static RoundManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        cRound = new Round();
        cRound.stelling = (int)Mathf.Floor(Random.Range(0, CardData.stories.Count));
        cRound.answers = new List<float> { };

        audioS = GetComponent<AudioSource>();

        if (SessionData.CSESSION == null)
        {
            SessionData.CSESSION = new Session();
        }
        SessionData.CSESSION.stellingStartIndex = Random.Range(0, CardData.stellingen.Count - SessionData.CSESSION.player_count);
        RandomStellingen();

        SessionData.CSESSION.points = 0;
        SessionData.CSESSION.timeSavedAtFirstRound = 0;
        SessionData.CSESSION.opinionLikes = 0;
        Time.timeScale = 1;

        //oberNameText.text = SessionData.CSESSION.character == 0 ? SessionData.Melissa : SessionData.John;
        //storyText.text = CardData.stories[cRound.stelling].begin + "\n\n" + CardData.stories[cRound.stelling].question;


        StartCoroutine(FirstDialogue());
    }
    public void RandomStellingen()
    {
        SessionData.CSESSION.stellingen = new List<int> { };
        List<int> allStellingen = new List<int> { };
        for (int i = 0; i < CardData.stellingen.Count; i++)
        {
            allStellingen.Add(i);
        }

        for (int i = 0; i < SessionData.CSESSION.player_count; i++)
        {
            int index = Random.Range(0, allStellingen.Count);
            SessionData.CSESSION.stellingen.Add(allStellingen[index]);
            allStellingen.Remove(index);
        }
        Debug.Log(SessionData.CSESSION.stellingen[0]);
        Debug.Log(SessionData.CSESSION.stellingen[1]);
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

        //if (inPersonalAnswerMode) {
        //    PersonAnswered();
        //    return;
        //}
        //if (inFinalAnswerMode)
        //{
        //    return;
        //}

        if (dialogueManager.InDialogue)
        {
            return;
        }


        progressState++;
        switch (progressState)
        {
            case 1:
                nextButton.SetActive(false);
                bottleScreen.gameObject.SetActive(true);
                dialogueText.text = "";
                //stellingScreen.GetComponent<BasicScreen>().SlideIn();
                break;
            case 2:
                bottleScreen.GetComponent<BasicScreen>().SlideOut();
                stellingScreen.gameObject.SetActive(true);
                //stellingScreen.GetComponent<BasicScreen>().SlideIn();
                stellingScreen.GetStelling();
                break;
            case 3:
                resultScreen.gameObject.SetActive(true);
                //resultScreen.ShowResult();
                break;
            case 4:
                resultScreen.GetComponent<BasicScreen>().SlideOut();
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
