using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSet : MonoBehaviour
{
    public void OnMusicValueChanged(float newValue)
    {
        Music.volume = newValue;
    }

    public void OnSFXValueChanged(float newValue)
    {
        if (newValue < 0.01f)
            newValue = 0.01f;

        float volume = Mathf.Log10(newValue) * 20;
        Mixer.SetFloat("Sfx_Volume", volume);
    }
}
