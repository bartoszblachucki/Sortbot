using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroupFade _sortPanelFader;
    [SerializeField] private CanvasGroupFade _statisticsPanelFader;
    [SerializeField] private CanvasGroupFade _timePanelFader;
    [SerializeField] private CanvasGroupFade _menuPanelFader;

    private MachineManager _machineManager;
    private SortPanelUI _sortPanelUI;

    private void Awake()
    {
        _machineManager = FindObjectOfType<MachineManager>();
        _sortPanelUI = _sortPanelFader.GetComponentInChildren<SortPanelUI>();
        
        _sortPanelFader.FadeIn();
        _menuPanelFader.FadeIn();
    }

    private void OnEnable()
    {
        _sortPanelUI.OnSortButtonPressed.AddListener(SortingStart);
        _machineManager.OnRestartInitiated.AddListener(RestartStart);
        _machineManager.OnRestartFinished.AddListener(RestartFinished);
    }
    
    private void OnDisable()
    {
        _sortPanelUI.OnSortButtonPressed.RemoveListener(SortingStart);
    }

    private void SortingStart()
    {
        _sortPanelFader.FadeOut();
        _timePanelFader.FadeIn();
        _statisticsPanelFader.FadeIn();
    }

    private void RestartStart()
    {
        _statisticsPanelFader.FadeOut();
    }

    private void RestartFinished()
    {
        _sortPanelFader.FadeIn();
    }
}
