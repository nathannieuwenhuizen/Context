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
    private void Start()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(200f, 300f), Random.Range(-200f, 200f)));

        InstantiatedText = GameObject.Instantiate(UIText);
        InstantiatedText.GetComponent<UIFollowTarget>().Target = transform;
        InstantiatedText.GetComponent<UIFollowTarget>().canvas = canvas;
        InstantiatedText.transform.parent = canvas.gameObject.transform;
        //SetupText("dfghfg egrhn rgrbrbrg vg !!!!");

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
