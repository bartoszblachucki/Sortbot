using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComparisonCounter : MonoBehaviour
{
    [SerializeField] private Text countText;
    [SerializeField] private SortingMachine measuredMachine;

    private int _count = 0;

    private void OnEnable()
    {
        measuredMachine.OnSortingStarted.AddListener(RestartCounter);
        measuredMachine.OnComparision.AddListener(Tick);
    }

    private void OnDisable()
    {
        measuredMachine.OnSortingStarted.RemoveListener(RestartCounter);
        measuredMachine.OnComparision.RemoveListener(Tick);
    }

    private void Tick()
    {
        _count++;
        UpdateCounterText();
    }

    private void RestartCounter()
    {
        _count = 0;
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        countText.text = _count.ToString();
    }
}
