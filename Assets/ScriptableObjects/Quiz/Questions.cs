using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questions", menuName = "ScriptableObjects/Quiz/Questions", order = 1)]
public class Questions : ScriptableObject
{
    public List<Question> questions;
}

[Serializable]
public class Question
{
    public string correctAnswer;
    public List<string> options;
}
