using System.Collections;
using System.Collections.Generic;
using Recognissimo.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<string> wordOptions = new();

    [Header("Voice Recognition")]
    [SerializeField] private RecognitionListener recognitionListener;
    [SerializeField] private SpeechRecognizer speechRecognizer;

    [Header("UI References")]
    [SerializeField] private Transform wordOptionParent;
    [SerializeField] private GameObject wordCardPrefab;
    [SerializeField] private GameObject rowPrefab;

    [Header("Layout Settings")]
    [SerializeField] private int maxColumns = 4;

    private int currentColumn = 0;
    private Transform currentRow;

    private void Start()
    {
        // Test: spawn some words
        foreach (var word in wordOptions) { SpawnWord(word); }
        // Test: set speech recognizer vocabulary
        speechRecognizer.Vocabulary = wordOptions;
    }

    private void SpawnWord(string word)
    {
        if (currentColumn == 0 || currentColumn >= maxColumns)
        {
            // create new row each time maxColumns is reached
            GameObject newRow = Instantiate(rowPrefab, wordOptionParent);
            currentRow = newRow.transform;
            currentColumn = 0;
        }

        // spawn word card
        GameObject card = Instantiate(wordCardPrefab, currentRow);
        card.GetComponentInChildren<TextMeshProUGUI>().text = word;

        // Force the layout to rebuild immediately so new card spacing is applied
        LayoutRebuilder.ForceRebuildLayoutImmediate(currentRow.GetComponent<RectTransform>());

        currentColumn++;
    }

    private void ClearWords()
    {
        foreach (Transform child in wordOptionParent)
        {
            Destroy(child.gameObject);
        }
        currentColumn = 0;
    }

    public void StartQuiz(List<string> newWords)
    {
        ClearWords();

        // spawn new words
        wordOptions = newWords;
        foreach (var word in wordOptions) { SpawnWord(word); }

        // set speech recognizer vocabulary
        speechRecognizer.Vocabulary = wordOptions;
    }

    public void CheckAnswer(string answer)
    {
        string recognizedText = recognitionListener.GetResult();
        if (recognizedText.Equals(answer, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log($"Incorrect! You said: {recognizedText}, but the correct answer was: {answer}");
        }
    }

}
