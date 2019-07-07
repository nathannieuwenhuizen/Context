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

    private bool isBlinking = false;
    private Color valueColor;

    public UnityEvent timesUp;

    private void Start()
    {
        if (timesUp == null)
            timesUp = new UnityEvent();

        timerCount = startTime;
        valueColor = valueImage.color;
    }
    void Update()
    {
        if (timerCount > 0)
        {
            Countdown();
            if (timerCount < startTime / 4)
            {
                if (!isBlinking)
                {
                    isBlinking = true;
                    Debug.Log("blink start");
                    StartCoroutine(Blinking());
                }
            }
        } else if (isRunning)
        {
            isRunning = false;
            if (timesUp != null)
            {
                timesUp.Invoke();
            }
        }
    }
    IEnumerator Blinking()
    {
        while(timerCount > 0)
        {
            float blinkSpeed = 5f;
            float rValue = valueColor.r;
            Debug.Log(rValue);
            while (valueColor.g > 0)
            {
                valueColor.g -= Time.deltaTime * blinkSpeed;
                valueColor.b -= Time.deltaTime * blinkSpeed;
                valueImage.color = valueColor;
                Debug.Log("blink up");
                yield return new WaitForSeconds(Time.deltaTime);
            }
            while (valueColor.g < rValue)
            {
                valueColor.g += Time.deltaTime * blinkSpeed;
                valueColor.b += Time.deltaTime * blinkSpeed;
                valueImage.color = valueColor;
                Debug.Log("blink down");
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(.3f);
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
        StopAllCoroutines();

        valueColor.g = 1;
        valueColor.b = 1;
        valueImage.color = valueColor;

        isBlinking = false;
        IsRunning = true;
    }
    public void StopTimer()
    {
        StopAllCoroutines();
        valueColor.g = 1;
        valueColor.b = 1;
        valueImage.color = valueColor;

        IsRunning = false;
    }
}
