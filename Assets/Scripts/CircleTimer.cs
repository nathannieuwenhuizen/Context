using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
    [SerializeField]
    private float startTime = 60f;

    private float timerCount;

    [SerializeField]
    private Image valueImage;
    private bool isRunning = false;

    private void Start()
    {
        timerCount = startTime;
    }
    void Update()
    {
        if (timerCount > 0)
        {
            Countdown();
        } else if (isRunning)
        {
            isRunning = false;
            Debug.Log("time is 0");
        }
    }

    private void Countdown()
    {
        if (!isRunning)
        {
            return;
        }
        timerCount -= Time.deltaTime;
        valueImage.fillAmount = timerCount / startTime;
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
    public void StartTimer(float value)
    {
        startTime = timerCount = value;
        IsRunning = true;
    }
}
