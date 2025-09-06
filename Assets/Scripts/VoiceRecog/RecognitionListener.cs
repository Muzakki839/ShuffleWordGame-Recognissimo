using Recognissimo.Components;
using TMPro;
using UnityEngine;

/// <summary>
/// Get speech recognition result into string
/// </summary>
public class RecognitionListener : MonoBehaviour
{
    [Tooltip("Optional UI text to display recognition results")]
    [SerializeField] private TextMeshProUGUI resultText;

    private string PartialResult { get; }
    private string Result { get; }

    public void OnPartialResult(PartialResult partialResult)
    {
        Debug.Log($"<color=yellow>{partialResult.partial}</color>");
        if (resultText != null)
        {
            resultText.text = partialResult.partial;
        }
        // check answer as soon as partial result is available
        QuizManager.Instance.CheckAnswer(partialResult.partial);
    }
    public void OnResult(Result result)
    {
        // if fail to answer correctly before final result, treat as incorrect
        QuizManager.Instance.InvokeResultEvent(false);
    }

    public string GetResult()
    {
        return Result;
    }

    public string GetPartialResult()
    {
        return PartialResult;
    }
}
