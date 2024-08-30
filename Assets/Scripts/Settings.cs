using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        VolumeChanging();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeChanging()
    {
        Debug.Log("Sound Changed");
        float volume = volumeSlider.value;
        if (volume <= 0.00000001f)
        {
            volume = 0.00000001f;
        }
        myMixer.SetFloat("sounds", Mathf.Log10(volume)*20);
        //myMixer.SetFloat("sounds", volume);
    }

    public void SettingsClose()
    {
        Time.timeScale = 1.0f;
        MenuHandler.IsGamePaused = false;
        this.gameObject.SetActive(false);
    }
}
