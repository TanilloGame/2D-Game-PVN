using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthUIManager : MonoBehaviour
{
    public List<Image> heartImages;  // Lista de imágenes de corazones en el Canvas
    private int maxHealth;           // Salud máxima basada en el número de corazones

    private void Start()
    {
        maxHealth = heartImages.Count;
        UpdateHearts(maxHealth);  // Inicializa la visualización de corazones
    }

    // Método para actualizar la visualización de los corazones
    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].enabled = i < currentHealth;
        }
    }
}