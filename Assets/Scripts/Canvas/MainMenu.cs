using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Button surveyButton; // Asigna aqu� el bot�n de la encuesta desde el Inspector

    void Start()
    {
        surveyButton.onClick.AddListener(FillSurvey); // Asocia el m�todo FillSurvey al bot�n de encuesta
    }

   

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options");

    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu"); // Aseg�rate de usar el nombre exacto de tu escena del men� principal
    }

    // M�todo para iniciar una nueva partida
    public void StartNewGame()
    {
        SceneManager.LoadScene("Game"); // Cambia "Game" por el nombre de tu escena de juego
    }

    // M�todo para abrir la encuesta
    public void FillSurvey()
    {
        Application.OpenURL("https://docs.google.com/forms/d/1UzKgsyfRn-9Ii6OsDXol_WmUaokb51YGB2o40idRXY4/edit?hl=es-419"); // URL de tu encuesta

        // Deshabilitar el bot�n para evitar m�ltiples aperturas
        surveyButton.interactable = false;
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}