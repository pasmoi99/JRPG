using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderValue1 : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Text Value;
    public Slider Slider;

    public void OnValueChanged(float newValue)
    {

        int valueInt = (int)Mathf.Round(newValue * 100.0f);
        TextValue.text = valueInt.ToString();
    
    }
    public void OnMusicValueChanged (float newValue)
    {
        Music.volume = newValue;
    }

    public void OnSFXValueChanged(float newValue)
    {
        if (newValue < 0.01f)
            newValue = 0.01f;

        float volume = Mathf.Log10(newValue) * 20;

        Mixer.SetFloat("SFX_Volume", volume);
    }

}