using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ExecutionTimeCounter : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private SortingMachine measuredMachine;

    private float _time = 0;
    private bool _counting;
    
    void Update()
    {
        if (_counting)
            _time += Time.deltaTime;

        UpdateTimeText();
    }

    private void OnEnable()
    {
        measuredMachine.OnSortingStarted.AddListener(StartCounting);
        measuredMachine.OnSortingFinished.AddListener(StopCounting);
    }

    private void OnDisable()
    {
        measuredMachine.OnSortingStarted.RemoveListener(StartCounting);
        measuredMachine.OnSortingFinished.RemoveListener(StopCounting);
    }

    private void StartCounting()
    {
        _time = 0;
        _counting = true;
    }

    private void StopCounting()
    {
        _counting = false;
    }
    
    private void UpdateTimeText()
    {
        TimeSpan span = TimeSpan.FromSeconds(_time);
        timeText.text = span.ToString(@"mm\:ss\:fff");
    }
}
