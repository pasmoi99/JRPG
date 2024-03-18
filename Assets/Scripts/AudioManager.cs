using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("          Audio Source      ")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource; 

    [Header("         Audio Clip         ")]
    public AudioClip Death;
    public AudioClip Attack;
    public AudioClip Hit;

   

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}