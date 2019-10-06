using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip healSound;
    public AudioClip hitSound;
    public AudioClip levelUpSound;
    public AudioClip blipSound;
    public AudioClip gameOverSound;

    private AudioSource audioSource;

    public static SoundManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHealSound()
    {
        audioSource.clip = healSound;
        audioSource.Play();
    }

    public void PlayBlipSound()
    {
        audioSource.clip = blipSound;        
        audioSource.Play();
    }
    public void PlayLevelUpSound()
    {
        audioSource.Stop();
        audioSource.clip = levelUpSound;
        audioSource.Play();
    }
    public void PlayHitSound()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
    }

    public void PlayGameOverSound()
    {
        audioSource.Stop();
        audioSource.clip = gameOverSound;
        audioSource.Play();
    }

    public void OnVolumeKnobChanged(Slider slider)
    {
        audioSource.volume = slider.value;
        PlayBlipSound();
    }

}
