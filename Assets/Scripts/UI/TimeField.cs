using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeField : MonoBehaviour
{
    [SerializeField] private GameObject minText;
    private InputField field;
    private int numVal = 1;
    // Start is called before the first frame update
    void Start()
    {
        field = GetComponent<InputField>();
        field.onValueChanged.AddListener(delegate
        {
            UpdateField();
        });
    }
    private void UpdateField ()
    {
        string val = field.text;
        //if its a number
        
        if (int.TryParse(val, out numVal))
        {
            numVal = Mathf.Clamp(numVal, 1, 60);
            field.text = numVal.ToString();
            Debug.Log(numVal);
        } else if (val != "")
        {
            numVal = 1;
            field.text = numVal.ToString();
        }
        minText.SetActive(field.text != "");

    }
    public int NumVal
    {
        get { return numVal; }
    }
}
