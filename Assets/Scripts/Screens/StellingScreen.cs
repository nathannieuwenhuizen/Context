using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StellingScreen : MonoBehaviour
{
    private int playerIndex = 1;
    private float timeSaved = 0;

    [SerializeField]
    private Text stellingText;

    [SerializeField]
    private CircleTimer timer;

    [SerializeField]
    private Text playerText;

    void Start()
    {
        //SessionData.CSESSION = new Session();

        //SessionData.CSESSION.stellingStartIndex = Random.Range(0, CardData.stellingen.Count - SessionData.CSESSION.player_count);
        //GetStelling();
    }
    public void NextStelling()
    {
        SessionData.CSESSION.timeSavedAtFirstRound += Mathf.Round(timer.TimeCount);
        Debug.Log("time saved: " + SessionData.CSESSION.timeSavedAtFirstRound);
        timer.TimeCount = timer.StartTime;
        playerIndex++;
        if (playerIndex > SessionData.CSESSION.player_count)
        {
            RoundManager.instance.NextButtonClicked();
            gameObject.SetActive(false);
            return;
        }
        GetStelling();
    }
    public void GetStelling()
    {
        playerText.text = "Speler " + playerIndex;
        stellingText.text = CardData.stellingen[SessionData.CSESSION.stellingStartIndex + playerIndex];
    }
}
