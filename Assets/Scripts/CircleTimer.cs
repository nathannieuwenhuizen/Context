using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CircleTimer : MonoBehaviour
{
    [SerializeField]
    private float startTime = 60f;

    private float timerCount;

    [SerializeField]
    private Image valueImage;
    private bool isRunning = false;

    public UnityEvent timesUp;

    private void Start()
    {
        if (timesUp == null)
            timesUp = new UnityEvent();

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
            if (timesUp != null)
            {
                timesUp.Invoke();
            }
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
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
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
        set { timerCount = value;  UpdateVisuals(); }
    }
    public float StartTime
    {
        get { return startTime; }
        set { startTime = value; }
    }
    public void StartTimer(float value)
    {
        startTime = timerCount = value;
        IsRunning = true;
    }
    public void StopTimer()
    {
        IsRunning = false;
    }
}
