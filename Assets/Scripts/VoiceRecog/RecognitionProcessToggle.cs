using Recognissimo.Components;
using UnityEngine;

/// <summary>
/// Toggle speech recognition processing on and off based on the recognizer’s current state.
/// </summary>
public class RecognitionProcessToggle : MonoBehaviour
{
    public void ToggleProcessing(SpeechRecognizer recognizer)
    {
        if (recognizer.State == Recognissimo.SpeechProcessorState.Processing)
        {
            recognizer.StopProcessing();
        }
        else
        {
            recognizer.StartProcessing();
        }
    }
}
