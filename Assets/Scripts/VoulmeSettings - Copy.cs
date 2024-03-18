using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Ui;

public class VoulmeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    public void SeMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);

    }
}
 