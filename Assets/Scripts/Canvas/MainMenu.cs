using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Button surveyButton; // Asigna aquí el botón de la encuesta desde el Inspector

    void Start()
    {
        surveyButton.onClick.AddListener(FillSurvey); // Asocia el método FillSurvey al botón de encuesta
    }

   

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options");

    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu"); // Asegúrate de usar el nombre exacto de tu escena del menú principal
    }

    // Método para iniciar una nueva partida
    public void StartNewGame()
    {
        SceneManager.LoadScene("Game"); // Cambia "Game" por el nombre de tu escena de juego
    }

    // Método para abrir la encuesta
    public void FillSurvey()
    {
        Application.OpenURL("https://docs.google.com/forms/d/1UzKgsyfRn-9Ii6OsDXol_WmUaokb51YGB2o40idRXY4/edit?hl=es-419"); // URL de tu encuesta

        // Deshabilitar el botón para evitar múltiples aperturas
        surveyButton.interactable = false;
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}