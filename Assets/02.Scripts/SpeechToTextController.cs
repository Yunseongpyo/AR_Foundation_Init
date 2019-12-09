using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechToTextController : MonoBehaviour
{
    public IBM.Watsson.Examples.ExampleStreaming voice;
    public bool onOffRecording;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!onOffRecording)
        {
            voice.Active = false; 
        }
        else if(onOffRecording)
        {
            voice.Active = true;
        }
    }
}
