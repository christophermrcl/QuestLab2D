using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject clickSoundPrefab;
    public GameObject settingCanvas;
    public Slider BGM;
    public Slider SFX;

    private float BGMVolume;
    private float SFXVolume;
    // Start is called before the first frame update
    void Start()
    {
        BGMVolume = PlayerPrefs.GetFloat("bgmvolume", 1);
        SFXVolume = PlayerPrefs.GetFloat("sfxvolume", 1);
        BGM.value = BGMVolume;
        SFX.value = SFXVolume;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("bgmvolume", BGM.value);
        PlayerPrefs.SetFloat("sfxvolume", SFX.value);
        PlayerPrefs.Save();
    }

    public void ExitSetting()
    {
        Instantiate(clickSoundPrefab);
        settingCanvas.SetActive(false);
    }
}
