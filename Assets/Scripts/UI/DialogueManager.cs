using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {

    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Text characterText;
    [SerializeField]
    private GameObject UIObject;
    [SerializeField]
    private AudioSource writingSound;

    private bool isWriting = false;
    private bool inDialogue = false;
    private bool goToNextLine = false;

    public delegate void CallbackDelegate();

    void Update()
    {
        if (inDialogue)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                if (isWriting)
                    isWriting = false;
                else
                    goToNextLine = true;
            }
        }
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
                    dialogueText.alignment = characterText.alignment = TextAnchor.UpperRight;
                }
                else
                {
                    dialogueText.alignment = characterText.alignment = TextAnchor.UpperLeft;
                }

                characterText.text = dialogue[i].name;
                //Debug.Log("next line!");
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
        dialogueText.text = "";
        //writingSound.Play();
        for (int i = 0; i < line.Length; i++)
        {
            if(isWriting)
            {
                dialogueText.text += line[i];
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                dialogueText.text += line.Substring(i,line.Length-i);
                break;
            }
        }
        isWriting = false;
    }
    public bool InDialogue
    {
        get{ return inDialogue; }
    }
}
