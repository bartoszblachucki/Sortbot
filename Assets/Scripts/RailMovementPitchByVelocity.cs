using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RailMovement))]
public class RailMovementPitchByVelocity : MonoBehaviour
{
    [SerializeField] private AudioClip movingSound;
    [SerializeField] private float velocityToPitchFactor = 40;
    private AudioSource _audioSource;
    private RailMovement _railMovement;
    
    void Awake()
    {
        _railMovement = GetComponent<RailMovement>();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1;
        _audioSource.loop = true;
        _audioSource.clip = movingSound;
        _audioSource.Play();
    }
    
    void Update()
    {
        _audioSource.pitch = _railMovement.Velocity.magnitude / velocityToPitchFactor;
    }
}
