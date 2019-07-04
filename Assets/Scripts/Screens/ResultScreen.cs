using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    [SerializeField]
    private Text amountPersonText;
    [SerializeField]
    private Text firstRoundText;
    [SerializeField]
    private Text secondRoundText;
    [SerializeField]
    private Text likesText;

    public void ShowResult()
    {
        amountPersonText.text = SessionData.CSESSION.player_count.ToString();
        firstRoundText.text = SessionData.CSESSION.timeSavedAtFirstRound.ToString();
        secondRoundText.text = SessionData.CSESSION.points.ToString();
        likesText.text = SessionData.CSESSION.opinionLikes.ToString();
    }
}
