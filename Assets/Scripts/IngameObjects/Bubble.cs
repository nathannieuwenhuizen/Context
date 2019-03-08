using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    //objects
    [SerializeField] private GameObject UIText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject crown;
    private GameObject InstantiatedText;

    private Rigidbody2D rb;
    private Vector3 myTargetPos;
    private bool idle = false;
    private float timeOnIdle = 0;

    //color
    private SpriteRenderer sr;
    private bool clicked = false;

    //vals
    private int fromPlayer = -1;

    private void Start()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();

        sr = GetComponent<SpriteRenderer>();

        InstantiatedText = GameObject.Instantiate(UIText);
        InstantiatedText.GetComponent<UIFollowTarget>().Target = transform;
        InstantiatedText.GetComponent<UIFollowTarget>().canvas = canvas;
        InstantiatedText.transform.parent = canvas.gameObject.transform;
        InstantiatedText.transform.SetSiblingIndex(0);
    }
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool overSprite = sr.bounds.Contains(mousePosition);


        if (overSprite)
        {
            //If we've pressed down on the mouse (or touched on the iphone)
            if (Input.GetButtonDown("Fire1"))
            {
                Clicked = !Clicked;
            }
        }

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
        StartCoroutine(IdleFloat());
    }
    private IEnumerator IdleFloat()
    {
        timeOnIdle = Time.time;
        while (idle)
        {
            transform.position = new Vector2(myTargetPos.x, myTargetPos.y + Mathf.Sin((timeOnIdle - Time.time) * 0.2f) * .5f);
            yield return new WaitForFixedUpdate();
        }
    }
    public void DisplayCrown(bool show)
    {
        crown.SetActive(show);
    }


    public string Text
    {
        get {
            return InstantiatedText.GetComponent<UIFollowTarget>().Text;
        }
        set
        {
            InstantiatedText.GetComponent<UIFollowTarget>().Text = value;
        }
    }
    public int FromPlayer
    {
        get { return fromPlayer; }
        set { fromPlayer = value;}
    }

    public void SetActive (bool val)
    {
        InstantiatedText.SetActive(val);
        gameObject.SetActive(val);
    }

    public bool Clicked
    {
        get { return clicked; }
        set {
            clicked = value;
            sr.color = new Color(clicked ? 0.5f : 1, 1, clicked ? 0.5f : 1);
            if (clicked)
            {
                Bubble[] allBubbles = GameObject.FindObjectsOfType<Bubble>();
                foreach (Bubble bubble in allBubbles)
                {
                    if (bubble != this)
                    {
                        bubble.Clicked = false;
                    }
                }
            }
        }
    }
}
