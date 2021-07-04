using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosenAlgorithmLabel : MonoBehaviour
{
    [SerializeField] private Text algorithmNameText;
    private SortPanelUI _sortPanelUI;

    private void Awake()
    {
        _sortPanelUI = FindObjectOfType<SortPanelUI>();
    }

    private void OnEnable()
    {
        _sortPanelUI.OnSortButtonPressed.AddListener(SetAlgorithmName);
    }

    private void OnDisable()
    {
        _sortPanelUI.OnSortButtonPressed.RemoveListener(SetAlgorithmName);
    }

    private void SetAlgorithmName()
    {
        algorithmNameText.text = _sortPanelUI.SelectedAlgorithmName;
    }
}
