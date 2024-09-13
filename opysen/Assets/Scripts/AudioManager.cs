using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] audioSource;
    public AudioSource soundSource;
    public AudioSource musicSource;

    private void Awake()
    {   
         if(instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            instance = this;
        } else if(instance != this) // If there is already an instance and it's not this instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }

        soundSource = transform.GetChild(1).GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        ChangeMusicVolume(0f);
        ChangeSoundVolume(0f);
    }

    public AudioClip PlaySFX(int soundToPlay)
    {
        return audioSource[soundToPlay].clip;
    }

    public void Playa(int soundToPlay)
    {
        audioSource[soundToPlay].Play();
    }

    public void ChangeSoundVolume(float change)
    {
        ChangeSourceVolume(0.5f, "soundVolume", change, soundSource);
    }
    public void ChangeMusicVolume(float change)
    {
        ChangeSourceVolume(0.1f, "musicVolume", change, musicSource);
    }
    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source){
        //get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        //check if we reached the maximum or minimum value
        if(currentVolume > 1.1)
            currentVolume = 0;
        else if(currentVolume < 0)
            currentVolume = 1;    

        float finalVolume = currentVolume * baseVolume;

        if(volumeName == "soundVolume"){
            GameObject child = source.transform.Find("Hurt").gameObject;
            child.GetComponent<AudioSource>().volume = finalVolume;
            child = source.transform.Find("Jump").gameObject;
            child.GetComponent<AudioSource>().volume = finalVolume;
            child = source.transform.Find("Game Over").gameObject;
            child.GetComponent<AudioSource>().volume = finalVolume;
            child = source.transform.Find("Level Passed").gameObject;
            child.GetComponent<AudioSource>().volume = finalVolume;
        }
        else
            source.volume = finalVolume;

        //save final value to player prefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    } 
}