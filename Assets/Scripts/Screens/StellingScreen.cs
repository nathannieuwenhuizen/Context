using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StellingScreen : MonoBehaviour
{
    private int playerIndex = 1;
    private float timeSaved = 0;

    [SerializeField]
    private QuestionScreen questionScreen;

    [SerializeField]
    private Text stellingText;

    [SerializeField]
    private CircleTimer timer;

    [SerializeField]
    private Text playerText;

    [SerializeField]
    private Text likesText;
    private int opinionLikes = 0;
    void Start()
    {
        //SessionData.CSESSION = new Session();

        //SessionData.CSESSION.stellingStartIndex = Random.Range(0, CardData.stellingen.Count - SessionData.CSESSION.player_count);
        //GetStelling();
        OpinionLikes = 0;
    }
    public void NextStelling()
    {
        SessionData.CSESSION.timeSavedAtFirstRound += Mathf.Round(timer.TimeCount);
        SessionData.CSESSION.opinionLikes += opinionLikes;
        OpinionLikes = 0;

        timer.TimeCount = timer.StartTime;
        playerIndex++;

        gameObject.SetActive(false);
        questionScreen.gameObject.SetActive(true);
        questionScreen.GetStelling();

        if (playerIndex > SessionData.CSESSION.player_count)
        {
            //RoundManager.instance.NextButtonClicked();
            //gameObject.SetActive(false);
            return;
        }
        GetStelling();
    }
    public void GetStelling()
    {
        playerText.text = "Speler " + playerIndex;
        //stellingText.text = CardData.stellingen[SessionData.CSESSION.stellingStartIndex + playerIndex];
        stellingText.text = CardData.stellingen[SessionData.CSESSION.stellingen[playerIndex - 1]];
    }
    public void IncreaseLike()
    {
        OpinionLikes++;
    }

    public void UpdateLikeText()
    {
        if (OpinionLikes <= 0)
        {
            likesText.text = "like opinion";
        }
        else if (OpinionLikes < 2)
        {
            likesText.text = opinionLikes + " like";
        }
        else
        {
            likesText.text = opinionLikes + " likes";
        }
    }
    public int OpinionLikes
    {
        get { return opinionLikes; }
        set
        {
            opinionLikes = value;
            UpdateLikeText();
        }
    }

}
