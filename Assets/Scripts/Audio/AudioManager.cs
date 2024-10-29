using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Instancia singleton

    public AudioSource audioSource; // Fuente de audio
    public List<AudioClip> audioClips; // Lista de clips de audio

    private Dictionary<string, AudioClip> audioDictionary; // Diccionario para acceder a los clips por nombre

    private void Awake()
    {
        // Asegura que solo haya una instancia de AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cargar nuevas escenas
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializar el diccionario
        audioDictionary = new Dictionary<string, AudioClip>();
        foreach (var clip in audioClips)
        {
            audioDictionary[clip.name] = clip; // Asocia el nombre del clip con el clip mismo
        }
    }

    // Método para reproducir un sonido
    public void PlaySound(string soundName)
    {
        if (audioDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip); // Reproduce el clip
        }
        else
        {
            Debug.LogWarning($"Sonido '{soundName}' no encontrado!");
        }
    }
}