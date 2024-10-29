using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private void Start()
    {
        // Inicia la corutina para manejar el Game Over
        StartCoroutine(HandleGameOver());
    }

    private IEnumerator HandleGameOver()
    {
        // Espera 3 segundos
        yield return new WaitForSeconds(3f);

        // Vuelve a cargar la escena del juego
        SceneManager.LoadScene("Game"); // Cambia "GameScene" por el nombre de tu escena de juego
    }
}