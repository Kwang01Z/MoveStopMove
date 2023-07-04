using System.Collections;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeSmooth = 1f;
    float currentVolume = 1f;
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
        audioSource.loop = true;
        audioSource.playOnAwake = true;
    }
    // Mở âm thanh mới và làm mượt
    public void OnPlayAudioClip(AudioClip clip)
    {
        SmoothSound(audioSource, timeSmooth);
        audioSource.PlayDelayed(1);
        audioSource.loop = true;
        audioSource.clip = clip;
        audioSource.Play();
    }
    // Làm âm thanh mượt hơn khi chuyển đổi 
    public void SmoothSound(AudioSource audioSource, float fadeTime)
    {
        currentVolume = audioSource.volume;
        StartCoroutine(FadeSoundOn(audioSource, fadeTime));
    }
    IEnumerator FadeSoundOn(AudioSource audioSource, float fadeTime)
    {
        if (audioSource == null) yield return null;
        yield return FadeSoundOff(audioSource, fadeTime);
        var t = 0f;
        while (t < fadeTime)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            audioSource.volume = Mathf.Clamp(t, 0, currentVolume);
        }
    }
    IEnumerator FadeSoundOff(AudioSource audioSource, float fadeTime)
    {
        var t = fadeTime;
        while (t > 0)
        {
            yield return new WaitForEndOfFrame();
            t -= Time.deltaTime;
            if (audioSource != null) audioSource.volume = Mathf.Clamp(t, 0, currentVolume);
        }
    }
    public void SetVolume(float vol)
    {
        audioSource.volume = vol;
    }
    public float GetCurrentVolumn()
    {
        return audioSource.volume;
    }
}