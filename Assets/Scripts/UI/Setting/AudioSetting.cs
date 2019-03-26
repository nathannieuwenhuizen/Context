using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    private Slider sl;
    void Start()
    {
        sl = GetComponent<Slider>();
        sl.value = AudioListener.volume;

    }
    public void OnValueChange()
    {
        AudioListener.volume = sl.value;
    }
}
