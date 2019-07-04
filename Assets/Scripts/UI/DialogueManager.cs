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

    public delegate void CallbackDelegate();

    void Start()
    {
        UIObject.SetActive(false);
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
                if (dialogue[i].allignmentRight)
                {
                    //mesh.alignment = mesh.alignment =  TextAnchor.UpperRight;
                }
                else
                {
                    //dialogueText.alignment = characterText.alignment = TextAnchor.UpperLeft;
                }

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
                }
    }
}
