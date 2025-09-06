using System.Collections.Generic;
using Recognissimo.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizManager : Singleton<QuizManager>
{
    [SerializeField] private List<string> wordOptions = new();
    [Header("Quiz Data")]
    [SerializeField] private Questions questionsData;

    [Header("Voice Recognition")]
    [SerializeField] private RecognitionListener recognitionListener;
    [SerializeField] private SpeechRecognizer speechRecognizer;

    [Header("UI References")]
    [SerializeField] private Transform wordOptionParent;
    [SerializeField] private GameObject wordCardPrefab;
    [SerializeField] private GameObject rowPrefab;

    [Header("Layout Settings")]
    [SerializeField] private int maxColumns = 4;

    [Header("Result Event")]
    public UnityEvent startEvent;
    public UnityEvent correctEvent;
    public UnityEvent incorrectEvent;

    private int currentColumn = 0;
    private Transform currentRow;
    private int currentQuestionIndex = 0;

    private void Start()
    {
        StartCurrentQuiz();

        // // Test: spawn some words
        // foreach (var word in wordOptions) { SpawnWord(word); }
        // // Test: set speech recognizer vocabulary
        // speechRecognizer.Vocabulary = wordOptions;
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

    private void StartQuiz(List<string> newWords)
    {
        startEvent?.Invoke();
        ClearWords();

        // spawn new words
        wordOptions = ListManipulator.ShuffleList(newWords);
        foreach (var word in wordOptions) { SpawnWord(word); }

        // set speech recognizer vocabulary
        speechRecognizer.Vocabulary = wordOptions;
    }

    public void StartCurrentQuiz()
    {
        StartQuiz(questionsData.questions[currentQuestionIndex].options);
    }

    public bool CheckAnswer(string answer)
    {
        string correctAnswer = questionsData.questions[currentQuestionIndex].correctAnswer;
        if (answer == correctAnswer)
        {
            Debug.Log("Correct!");

            // set to next question or loop back to first question
            currentQuestionIndex = (currentQuestionIndex + 1) % questionsData.questions.Count;

            // make sure correct answer event invoked as soon as possible it's correct
            InvokeResultEvent(true);
            return true;
        }
        else
        {
            Debug.Log($"Incorrect! You said: {answer}, but the correct answer was: {correctAnswer}");
            return false;
        }
    }

    public void InvokeResultEvent(bool isCorrect)
    {
        if (isCorrect)
        {
            correctEvent?.Invoke();
        }
        else
        {
            incorrectEvent?.Invoke();
        }
        speechRecognizer.StopProcessing();
    }

}
