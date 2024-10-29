using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthUIManager : MonoBehaviour
{
    public List<Image> heartImages;  // Lista de im�genes de corazones en el Canvas
    private int maxHealth;           // Salud m�xima basada en el n�mero de corazones

    private void Start()
    {
        maxHealth = heartImages.Count;
        UpdateHearts(maxHealth);  // Inicializa la visualizaci�n de corazones
    }

    // M�todo para actualizar la visualizaci�n de los corazones
    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].enabled = i < currentHealth;
        }
    }
}