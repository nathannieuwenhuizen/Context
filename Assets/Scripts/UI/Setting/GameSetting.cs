using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    [SerializeField]
    private Slider opinionDuration;
    [SerializeField]
    private Slider questionDuration;

    [SerializeField]
    private Text opinionText;
    [SerializeField]
    private Text questionText;
    void Start()
    {
        opinionDuration.value = SessionData.OpinionDuration;
        questionDuration.value = SessionData.QuestionDuration;
        UpdateOpinionDuration();
    }
    public void UpdateOpinionDuration()
    {
        SessionData.OpinionDuration = opinionDuration.value;
        opinionText.text = opinionDuration.value + "s";
    }
    public void UpdateQuestionDuration()
    {
        SessionData.QuestionDuration = questionDuration.value;
        questionText.text = questionDuration.value + "s";
    }

}
