using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoNo : MonoBehaviour
{
    [Header("----------Audio Source--------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SfxSource;

   [Header("----------Audio Clip--------")]
    public AudioClip Background;
    public AudioClip Death;
    public AudioClip Attack;
    public AudioClip Hit;
}
