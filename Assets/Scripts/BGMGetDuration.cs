using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMGetDuration : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
            return;
        }

        // Retrieve the playback time from PlayerPrefs
        float playbackTime = PlayerPrefs.GetFloat("bgmduration", 0f);

        // Set the playback position of the AudioSource
        if (playbackTime >= 0f && playbackTime <= audioSource.clip.length)
        {
            audioSource.time = playbackTime;
        }
        else
        {
            Debug.LogWarning("Saved playback time is out of the clip's length range. Starting from the beginning.");
            audioSource.time = 0f;
        }

        // Start playing the audio
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
