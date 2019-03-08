using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player menu objects")]
    [SerializeField] private GameObject fieldNameParent;
    [SerializeField] private Dropdown amountDropDown;
    private InputField[] nameFields;

    [Header("Ober menu objects")]
    [SerializeField] private Transform oberPos;
    [SerializeField] private float OberPadding = 3f;
    [SerializeField] private float OberSpeed = 3f;
    [SerializeField] private GameObject oberSelection;
    [SerializeField] private GameObject chosenOber;

    [Header("Time menu objects")]
    [SerializeField] private TimeField timeField;

    [Header("other")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject[] screens;

    private Transform[] obers;
    private int currentPos;


    private int progressionState = 0;
    private Session cSession;
    void Start()
    {
        cSession = new Session();

        nameFields = fieldNameParent.GetComponentsInChildren<InputField>();
        for (int i = 0; i < nameFields.Length; i++)
        {
            nameFields[i].text = "person " + (i + 1);
        }

        chosenOber.SetActive(false);

        UpdateNameFields();
        amountDropDown.onValueChanged.AddListener(delegate { UpdateNameFields(); } );

        ShowScreen(0);
        SetupOber();

        Debug.Log(WholeLineToSepereateLines("sdfdfghnderftghgfdsdfgg. sdfghj. dfghj.")[0]);
    }
    private void UpdateNameFields()
    {
        int amount = amountDropDown.value + 2;
        for(int i = 0; i < nameFields.Length; i++)
        {
            nameFields[i].gameObject.SetActive(i < amount);
        }
    }
    private void ApplyNames()
    {
        cSession.players = new List<Player> { };
        for (int i = 0; i < nameFields.Length; i++)
        {
            if (i < amountDropDown.value + 2)
            {
                cSession.players.Add(new Player(nameFields[i].text, 0));
            } else {
                break;
            }
        }
        Debug.Log(cSession.players[1]);
    }
    private void SetupOber()
    {
        obers = oberPos.GetComponentsInChildren<Transform>();
        
        for (int i = 1; i < obers.Length; i++)
        {
            obers[i].position = new Vector3( OberPadding * (i - 1), obers[i].position.y, 0);
        }
    }
    public void MoveOber(bool right)
    {
        Debug.Log(currentPos);
        currentPos += (right ? 1 : -1);
        if (currentPos < 0)
        {
            currentPos = obers.Length - 2;
        } else if (currentPos > obers.Length - 2)
        {
            currentPos = 0;
        }

        StopAllCoroutines();

        StartCoroutine(MovingOber(right));
    }
    private IEnumerator MovingOber( bool right)
    {
        Vector3 newPos = oberPos.position;
        newPos.x = -OberPadding * currentPos;

        while (Mathf.Abs(oberPos.position.x - newPos.x) > 0.1f)
        {
            oberPos.position = Vector3.Lerp(oberPos.position, newPos, Time.deltaTime * OberSpeed);
            yield return new WaitForFixedUpdate();
        }

    }

    public void Progress()
    {
        if (dialogueManager.InDialogue)
        {
            return;
        }

        progressionState++;
        switch (progressionState)
        {
            case 1:
                ApplyNames();
                dialogueManager.StartDialogue(DialogueData.ChooseHost, false, null);
                ShowScreen(1);
                break;
            case 2:
                ShowScreen(-1);
                cSession.character = currentPos;

                chosenOber.SetActive(true);
                chosenOber.GetComponent<Ober>().UpdateSprite(cSession.character);

                dialogueManager.StartDialogue(DialogueData.IntroductionHost, false, () => ShowScreen(2));
                break;
            case 3:
                ShowScreen(-1);
                cSession.timePerRound = timeField.NumVal;
                dialogueManager.StartDialogue(DialogueData.BeginTheRound, false, null);
                break;
            case 4:
                EndOfScene();
                break;
            default:
                break;
        }
    }
    public void ShowScreen(int index)
    {
        foreach(GameObject screen in screens)
        {
            screen.SetActive(false);
        }
        if (index != -1)
        {
            screens[index].SetActive(true);
        }

        oberSelection.SetActive(index == 1);
        
    }
    public void EndOfScene()
    {
        Debug.Log(cSession);
        SessionData.CSESSION = cSession;
        SceneManager.LoadScene(1);
    }

    public string[] WholeLineToSepereateLines(string val)
    {
        return val.Split(new string[] { ". " }, System.StringSplitOptions.None);
    }

}
