using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    void Update()
    {
        Time.timeScale = slider.value;
    }
}
