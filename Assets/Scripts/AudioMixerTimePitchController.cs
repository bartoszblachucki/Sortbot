using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerTimePitchController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    void Update()
    {
        mixer.SetFloat("Pitch", Time.timeScale);
    }
}
