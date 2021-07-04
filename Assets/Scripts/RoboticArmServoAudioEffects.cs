using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class RoboticArmServoAudioEffects : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip servoSound;
    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 1.2f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = servoSound;
    }

    public void OnMove()
    {
        _audioSource.pitch = Random.Range(minPitch, maxPitch);
        _audioSource.Play();
    }

    public void OnStop()
    {
        _audioSource.Stop();
    }
}

