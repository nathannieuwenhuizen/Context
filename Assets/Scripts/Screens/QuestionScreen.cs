using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScreen : MonoBehaviour
{
    private int playerIndex = 1;
    private int questionPoints = 0;

    [SerializeField]
    private Text stellingText;

    [SerializeField]
    private CircleTimer timer;

    [SerializeField]
    private Text playerText;

    [SerializeField]
    private Text pointText;
    void Start()
    {
        //SessionData.CSESSION = new Session();
        //SessionData.CSESSION.stellingStartIndex = Random.Range(0, CardData.stellingen.Count - SessionData.CSESSION.player_count);
        //GetStelling();
        SessionData.CSESSION.points = 0;
    }
    public void NextStelling()
    {
        SessionData.CSESSION.points += QuestionPoints;
        QuestionPoints = 0;

        timer.TimeCount = timer.StartTime;
        playerIndex++;
        if (playerIndex > SessionData.CSESSION.player_count)
        {
            RoundManager.instance.NextButtonClicked();
            //gameObject.SetActive(false);
            return;
        }
        GetStelling();
    }
    public void GetStelling()
    {
        QuestionPoints = 0;
        playerText.text = "Speler " + playerIndex;
        stellingText.text = CardData.stellingen[SessionData.CSESSION.stellingStartIndex + playerIndex];
    }
    public void IncreasePoints()
    {
        QuestionPoints++;
    }

    public void UpdatePointText()
    {
        if (QuestionPoints < 2)
        {
            pointText.text = questionPoints + " punt";
        } else
        {
            pointText.text = questionPoints + " punten";
        }
    }
    public int QuestionPoints
    {
        get { return questionPoints; }
        set { questionPoints = value;
            UpdatePointText();
        }
    }
}
