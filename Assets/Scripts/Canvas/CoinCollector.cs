using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private ScoreManager scoreManager;  // Referencia al ScoreManager para actualizar el puntaje

    private void Start()
    {
        // Encuentra el objeto ScoreManager en la escena y accede a su componente
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto con el que colisiona tiene el Tag "Coin"
        if (other.CompareTag("Coin"))
        {
            AudioManager.Instance.PlaySound("Coin");
            scoreManager.AddScore(1);  // Aumenta el puntaje en 1
            Destroy(other.gameObject);  // Destruye la moneda después de recogerla
        }
        else if (other.CompareTag("Heart"))
        {
            AudioManager.Instance.PlaySound("HealthUp");
            playerHealth.RestoreHealth(1);  // Recupera 1 de salud
            Destroy(other.gameObject);
        }
    }
}