using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScreen : MonoBehaviour
{
    private int playerIndex = 1;
    private int questionPoints = 0;

    [SerializeField]
    private StellingScreen stellingScreen;

    [SerializeField]
    private Text stellingText;

    [SerializeField]
    private CircleTimer timer;

    [SerializeField]
    private Text playerText;

    [SerializeField]
    private Text pointText;

    [SerializeField]
    private Button startTimerButton;

    void Start()
    {
        //SessionData.CSESSION = new Session();
        //SessionData.CSESSION.stellingStartIndex = Random.Range(0, CardData.stellingen.Count - SessionData.CSESSION.player_count);
        //GetStelling();
        SessionData.CSESSION.points = 0;
        QuestionPoints = 0;
        startTimerButton.onClick.AddListener(StartTimer);

    }
    public void NextStelling()
    {
        SessionData.CSESSION.points += QuestionPoints;
        GetComponent<BasicScreen>().SlideOut();
        QuestionPoints = 0;

        timer.TimeCount = timer.StartTime;
        playerIndex++;

        if (playerIndex > SessionData.CSESSION.player_count)
        {
            RoundManager.instance.NextButtonClicked();
            //gameObject.SetActive(false);
            return;
        }
        stellingScreen.gameObject.SetActive(true);
        stellingScreen.GetStelling();
        stellingScreen.GetComponent<BasicScreen>().SlideIn();
    }
    public void GetStelling()
    {
        GetComponent<BasicScreen>().ResetTransform();
        QuestionPoints = 0;
        playerText.text = "Speler " + playerIndex;
        stellingText.text = CardData.stellingen[SessionData.CSESSION.stellingen[playerIndex - 1]];
    }
    public void IncreasePoints()
    {
        QuestionPoints++;
    }

    public void UpdatePointText()
    {
        if (questionPoints <= 0)
        {
            pointText.text = "like answer";
        }
        else if (QuestionPoints < 2)
        {
            pointText.text = questionPoints + " like";
        } else
        {
            pointText.text = questionPoints + " likes";
        }
    }
    public int QuestionPoints
    {
        get { return questionPoints; }
        set { questionPoints = value;
            UpdatePointText();
        }
    }
    public void StartTimer()
    {
        timer.StartTimer(SessionData.QuestionDuration);
    }

}
