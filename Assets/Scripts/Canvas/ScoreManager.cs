using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Referencia al texto de la puntuación en el Canvas
    private int score = 0;  // Puntuación inicial del jugador

    // Método para actualizar el puntaje en pantalla
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}