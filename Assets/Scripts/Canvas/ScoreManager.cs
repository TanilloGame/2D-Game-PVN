using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Referencia al texto de la puntuaci�n en el Canvas
    private int score = 0;  // Puntuaci�n inicial del jugador

    // M�todo para actualizar el puntaje en pantalla
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}