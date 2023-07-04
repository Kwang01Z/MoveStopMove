using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource audioSource;
    private void Reset()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnValidate()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }
    public void OnPlayAudioClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void SetVolume(float vol)
    {
        audioSource.volume = vol;
    }
    public void OnPlayButtonClickSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/btn_click");
        OnPlayAudioClip(clip);
    }
    public void OnPlayDeathSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/stickman_dead2");
        OnPlayAudioClip(clip);
    }
    public void OnPlayWinSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Victory");
        OnPlayAudioClip(clip);
    }
    public void OnPlayLoseSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Lose");
        OnPlayAudioClip(clip);
    }
    public float GetCurrentVolumn()
    {
        return audioSource.volume;
    }
}
