using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroyDuration : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on this GameObject. DestroyOnAudioEnd script requires an AudioSource component.");
            Destroy(gameObject); // Optional: destroy the GameObject if no AudioSource is present
            return;
        }
    }

    private void Update()
    {
        // Check if the audio has finished playing
        if (!audioSource.isPlaying && !audioSource.loop)
        {
            Destroy(gameObject); // Destroy the GameObject
        }
    }
}
