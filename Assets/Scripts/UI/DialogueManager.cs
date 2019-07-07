using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour {

    [SerializeField]
    private Text mesh;
    [SerializeField]
    private GameObject UIObject;
    [SerializeField]
    private AudioSource writingSound;

    private bool isWriting = false;
    private bool inDialogue = false;
    private bool goToNextLine = false;
    [SerializeField] private GameObject button;
    [SerializeField] private Image arrowIndicator;
    private Color arrowColor;
    public delegate void CallbackDelegate();

    void Start()
    {
        UIObject.SetActive(false);
        arrowColor = new Color(1, 1, 1, 0);
        arrowIndicator.color = arrowColor;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
        }

        if (inDialogue)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {

                bool uiIsButton = EventSystem.current.currentSelectedGameObject == button;
                Debug.Log(uiIsButton);

                if (uiIsButton) {
                Progress();
                }
            }
        }
    }
    public void Progress()
    {
        if (isWriting)
            isWriting = false;
        else
            goToNextLine = true;
    }
    
    public void StartDialogue(DialogueStruct[] dialogue, bool disapearWhenFinish, System.Action callBack)
    {
        StartCoroutine(WritingDialogue(dialogue, disapearWhenFinish, callBack));
    }
    IEnumerator WritingDialogue(DialogueStruct[] dialogue, bool disapearWhenFinish, System.Action callBack)
    {
        UIObject.SetActive(true);
        inDialogue = true;
        while(inDialogue)
        {
            for (int i = 0; i < dialogue.Length; i++)
            {
                //mesh.text = dialogue[i].name;
                StartCoroutine(WritingLine(dialogue[i].line));
                //Debug.Log(i + " | " + (dialogue.Length - 1));
                if (i == (dialogue.Length - 1)  && !disapearWhenFinish)
                {
                    if (callBack != null)
                    {
                        callBack();
                    }
                }
                goToNextLine = false;
                while (!goToNextLine)
                    yield return new WaitForFixedUpdate();
            }
            inDialogue = false;
        }
        if (callBack != null && disapearWhenFinish)
        {
            callBack();
        }
        StopCoroutine(Blinking());
        arrowColor.a = 0;
        arrowIndicator.color = arrowColor;

        UIObject.SetActive(!disapearWhenFinish);
    }
    IEnumerator WritingLine(string line)
    {
        isWriting = true;
        mesh.text = "";
        //mesh.GetComponent<RectTransform>().position = new Vector2(mesh.GetComponent<RectTransform>().position.x, 0f);
        //writingSound.Play();

        for (int i = 0; i < line.Length; i++)
        {
            if(isWriting)
            {
                mesh.text += line[i];
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                mesh.text += line.Substring(i,line.Length-i);
                break;
            }
        }
        isWriting = false;
        StartCoroutine(Blinking());
    }
    public bool OverButton ()
    {
        Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        //Debug.Log(button.GetComponent<RectTransform>().rect.Contains(mouse));
        return (button.GetComponent<RectTransform>().rect.Contains(mouse));
    }
    public bool InDialogue
    {
        get{ return inDialogue; }
        set{ inDialogue = value;
            StopAllCoroutines();

            arrowColor.a = 0;
            arrowIndicator.color = arrowColor;

        }
    }

    IEnumerator Blinking()
    {
        float blinkSpeed = 2.5f;

        yield return new WaitForSeconds(.5f);
        while (!isWriting && inDialogue)
        {
            while (arrowColor.a > 0 && (!isWriting && inDialogue))
            {
                arrowColor.a -= Time.deltaTime * blinkSpeed;
                arrowIndicator.color = arrowColor;
                yield return new WaitForSeconds(Time.deltaTime);

            }
            while (arrowColor.a < .5 && (!isWriting && inDialogue))
            {
                arrowColor.a += Time.deltaTime * blinkSpeed;
                arrowIndicator.color = arrowColor;
                yield return new WaitForSeconds(Time.deltaTime);
                
            }
        }
        arrowColor.a = 0;
        arrowIndicator.color = arrowColor;
    }
}
