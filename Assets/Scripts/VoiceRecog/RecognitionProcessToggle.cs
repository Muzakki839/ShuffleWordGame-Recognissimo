using Recognissimo.Components;
using UnityEngine;

/// <summary>
/// Toggle speech recognition processing on and off
/// </summary>
public class RecognitionProcessToggle : MonoBehaviour
{
    private bool isProcessing = false;

    public void ToggleProcessing(SpeechRecognizer recognizer)
    {
        if (isProcessing)
        {
            recognizer.StopProcessing();
            isProcessing = false;
        }
        else
        {
            recognizer.StartProcessing();
            isProcessing = true;
        }
    }
}
