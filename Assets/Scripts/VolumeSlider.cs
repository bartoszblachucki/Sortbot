using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string mixerParameter;
    
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        SetLevel(_slider.value);       
    }

    private void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerParameter, (float) (Math.Log10(sliderValue) * 20f));
    }
}
