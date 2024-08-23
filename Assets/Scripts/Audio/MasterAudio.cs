using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.Utilities;
using GameFoundation.Scripts.Utilities.Extension;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class MasterAudio : MonoBehaviour
{
    public static MasterAudio Instance;
    private const string WINSOUND = "win_sound";

    public AudioSource musicAudioSource;
    public AudioSource soundAudioSource;
    

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);

        Instance         = this;
        musicAudioSource = GetComponent<AudioSource>();
    }

    private void Start() { this.GetCurrentContainer().Inject(this); }

    public void PlayWinSound()
    {
        AudioManager.Instance.PlaySound(WINSOUND, soundAudioSource);
    }
}