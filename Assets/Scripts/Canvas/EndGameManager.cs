using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameManager : MonoBehaviour
{
    public float delayBeforeReturnToMenu = 3f; // Tiempo en segundos antes de volver al menú

    // Método para finalizar la partida
    public void EndGame()
    {
        StartCoroutine(EndGameCoroutine());
    }

    // Coroutine que espera antes de cambiar al menú
    private IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeReturnToMenu);
        SceneManager.LoadScene("MainMenu"); // Cambia "MainMenu" por el nombre exacto de tu escena de menú
    }
}