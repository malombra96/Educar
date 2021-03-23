using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPuntaje : MonoBehaviour
{
    public Text _percent;

    [Tooltip("Contador de respuestas correctas")]public double _rightAnswers;
    [Tooltip("Total de preguntas")] public double questions;
    
    public void IncreaseScore(int score) 
    {
        _rightAnswers += score;
        AsignScore(); 
    }

    public void IncreaseScore() 
    {
        _rightAnswers += 1;
        AsignScore();
        
    }
    
    void AsignScore()
    {
        _percent.text = "" + Math.Round( (_rightAnswers / questions) * 100 )+ "%";
    }
    
    public void resetScore()
    {
        _rightAnswers = 0;
        AsignScore();
    }
}
