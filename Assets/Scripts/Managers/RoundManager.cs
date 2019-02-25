using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class Round
    {
        public int roundTime;
        public CardSet cardSet;
        public string finalAnswer;
        public string[] answers;
    }
    public class CardSet
    {
        
    }
    public class Card
    {
        public int type;
        public string content;
    }
}
