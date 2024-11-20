using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSetting : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("bgmvolume", 1);
    }
}
