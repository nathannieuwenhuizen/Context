using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    private GameObject UIText;
    [SerializeField]
    private Canvas canvas;
    private GameObject InstantiatedText;

    private Rigidbody2D rb;
    private Vector3 myTargetPos;
    private bool idle = false;
    private float timeOnIdle = 0;
    private void Start()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();

        //rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(new Vector2(Random.Range(200f, 300f), Random.Range(-200f, 200f)));

        InstantiatedText = GameObject.Instantiate(UIText);
        InstantiatedText.GetComponent<UIFollowTarget>().Target = transform;
        InstantiatedText.GetComponent<UIFollowTarget>().canvas = canvas;
        InstantiatedText.transform.parent = canvas.gameObject.transform;

    }

    public void goToPos(Vector3 _targetPosition)
    {
        myTargetPos = _targetPosition;
        StartCoroutine(GoingToPosition());
    }

    private IEnumerator GoingToPosition()
    {
        idle = false;
        while (Vector2.Distance(transform.position, myTargetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, myTargetPos, Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        idle = true;
        Debug.Log("Idle");
        StartCoroutine(IdleFloat());
    }
    private IEnumerator IdleFloat()
    {
        timeOnIdle = Time.time;
        while (idle)
        {
            transform.position = new Vector2(myTargetPos.x, myTargetPos.y + Mathf.Sin(timeOnIdle - Time.time * 0.2f) * .5f);
            Debug.Log(Mathf.Sin(Time.deltaTime));

            yield return new WaitForFixedUpdate();
        }
    }

    public void SetupText(string val)
    {
        InstantiatedText.GetComponent<UIFollowTarget>().Text = val;
    }

    public void SetActive (bool val)
    {
        InstantiatedText.SetActive(val);
        gameObject.SetActive(val);
    }
}
