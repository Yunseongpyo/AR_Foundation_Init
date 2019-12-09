using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechToTextController : MonoBehaviour
{
    public IBM.Watsson.Examples.ExampleStreaming voice;
    public bool onOffRecording;
    public TextMeshProUGUI voiceText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        voiceText.text = voice._text;
        if (!onOffRecording)
        {
            voice.Active = false;
        }
        else if (onOffRecording)
        {
            voice.Active = true;
        }
    }
}
