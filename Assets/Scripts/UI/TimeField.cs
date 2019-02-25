using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeField : MonoBehaviour
{
    [SerializeField] private GameObject minText;
    private InputField field;
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
        int num;
        if (int.TryParse(val, out num))
        {
            num = Mathf.Clamp(num, 1, 60);
            field.text = num.ToString();
            Debug.Log(num);
        } else if (val != "")
        {
            field.text = "1";
        }
        minText.SetActive(field.text != "");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
