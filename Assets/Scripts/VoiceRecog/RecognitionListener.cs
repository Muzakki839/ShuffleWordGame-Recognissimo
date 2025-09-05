using Recognissimo.Components;
using TMPro;
using UnityEngine;
public class RecognitionListener : MonoBehaviour
{
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
    }
    public void OnResult(Result result)
    {
        Debug.Log($"<color=green>{result.text}</color>");
        if (resultText != null)
        {
            resultText.text = result.text;
        }
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
