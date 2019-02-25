using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private int currentPos;
    private int maxPos = 1;


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

        UpdateNameFields();
        amountDropDown.onValueChanged.AddListener(delegate { UpdateNameFields(); } );
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
        cSession.players = new List<string> { };
        for (int i = 0; i < nameFields.Length; i++)
        {
            if (i < amountDropDown.value + 2)
            {
                cSession.players.Add(nameFields[i].text);
            } else {
                break;
            }
        }
        Debug.Log(cSession.players[1]);
    }
    private void SetupOber()
    {
        //GameObject
    }

    private IEnumerator MoveOber( bool right)
    {
        yield return new WaitForFixedUpdate();
    }

    public void Progress()
    {
        progressionState++;
        switch (progressionState)
        {
            case 1:
                ApplyNames();
                break;
            case 2:
                break;
            default:
                break;
        }
    }
    
    public class Session
    {
        public List<string> players; //how many players it will be
        public int timePerRound = 60; // in sec
        public int character; // the ober/waitress
    }
}
