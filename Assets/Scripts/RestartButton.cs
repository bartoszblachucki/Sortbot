using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private SortingMachine _sortingMachine;
    private SortPanelUI _sortPanelUI;
    private Button _button;

    private void Awake()
    {
        _sortPanelUI = FindObjectOfType<SortPanelUI>();
        _sortingMachine = FindObjectOfType<SortingMachine>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _sortingMachine.OnSortingFinished.AddListener(EnableButton);
        _sortPanelUI.OnSortButtonPressed.AddListener(DisableButton);
    }

    private void OnDisable()
    {
        _sortingMachine.OnSortingFinished.RemoveListener(EnableButton);
        _sortPanelUI.OnSortButtonPressed.RemoveListener(DisableButton);
    }

    private void DisableButton()
    {
        _button.interactable = false;
    }

    private void EnableButton()
    {
        _button.interactable = true;
    }
    
}
