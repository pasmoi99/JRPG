using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSet : MonoBehaviour
{
    public AudioSource musicSource; 
    public AudioMixer mixer; 

    public void OnMusicValueChanged(float newValue)
    {
        if (musicSource != null) 
        {
            musicSource.volume = newValue;
        }
    }

    public void OnSFXValueChanged(float newValue)
    {
        if (newValue < 0.01f)
            newValue = 0.01f;

        float volume = Mathf.Log10(newValue) * 20;
        if (mixer != null) 
        {
            mixer.SetFloat("Sfx_Volume", volume);
        }
    }
}
