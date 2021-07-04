using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Scanner : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip scanSound;
    
    private Animator _animator;
    private static readonly int ScanTrigger = Animator.StringToHash("Scan");

    public UnityEvent OnScanFinished;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Scan()
    {
        _animator.SetTrigger(ScanTrigger);
        _audioSource.PlayOneShot(scanSound);
    }

    private void ScanFinished()
    {
        OnScanFinished.Invoke();
        _audioSource.Stop();
    }
}
