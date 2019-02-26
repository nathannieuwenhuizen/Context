using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private Text _countDownText;
    private float timerCount = 60;

    private bool isRunning = true;
    void Update()
    {
        if (timerCount <= 0)
        {
            _countDownText.text = "00:00";
        }

        else
        {
            Countdown();
        }
    }

    private void Countdown()
    {
        if (!isRunning)
        {
            return;
        }
        timerCount -= Time.deltaTime;

        var minutes = timerCount / 60;
        var seconds = timerCount % 60;
        var fraction = (timerCount * 100) % 100;

        _countDownText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(minutes), seconds);
    }

    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }
    public float TimeCount
    {
        get { return timerCount; }
        set { timerCount = value; }
    }
}