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

    [Space]
    [Header("buttons")]
    [SerializeField]
    private GameObject backToMenuButton;
    [SerializeField]
    private GameObject resumeButton;

    private void Start()
    {
        //ShowResult();
    }
    public void ShowResult()
    {
        //amountPersonText.text = SessionData.CSESSION.player_count.ToString();
        //firstRoundText.text = SessionData.CSESSION.timeSavedAtFirstRound.ToString();
        //secondRoundText.text = SessionData.CSESSION.points.ToString();
        //likesText.text = SessionData.CSESSION.opinionLikes.ToString();

        StartCoroutine(TransitionToNumber(amountPersonText, 2));
        StartCoroutine(TransitionToNumber(firstRoundText, 14));
        StartCoroutine(TransitionToNumber(secondRoundText, 12));
        StartCoroutine(TransitionToNumber(likesText, 6));

        //StartCoroutine(TransitionToNumber(amountPersonText, SessionData.CSESSION.player_count));
        //StartCoroutine(TransitionToNumber(firstRoundText, (int)SessionData.CSESSION.timeSavedAtFirstRound));
        //StartCoroutine(TransitionToNumber(secondRoundText, SessionData.CSESSION.points));
        //StartCoroutine(TransitionToNumber(likesText, SessionData.CSESSION.opinionLikes));
    }
    IEnumerator TransitionToNumber(Text text, int val)
    {
        float currentVal = 0;
        while (currentVal < val - 0.5f)
        {
            currentVal = Mathf.Lerp(currentVal, val, Time.deltaTime * (50f / val));
            text.text = Mathf.Floor(currentVal).ToString();
            yield return new WaitForSeconds(Time.deltaTime);
        }
        text.text = val.ToString();
        text.fontSize += 10;
        text.color = new Color(.5f, 1f, .5f);
        text.transform.GetChild(0).GetComponent<UIParticleSystem>().Play();
        yield return new WaitForSeconds(.5f);

        backToMenuButton.SetActive(true);
        resumeButton.SetActive(true);

    }
}