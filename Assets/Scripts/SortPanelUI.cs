using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;
#pragma warning disable 4014

public class SortPanelUI : MonoBehaviour
{
    // UI Elements
    [SerializeField] private Dropdown algorithmDropdown;
    [SerializeField] private Slider objectNumberSlider;
    
    // NonUI Elements
    private Dictionary<string, Func<Task>> _sortingAlgorithms;
    private MachineManager _machineManager;

    public UnityEvent OnSortButtonPressed;
    
    public string SelectedAlgorithmName { get; private set; }
    public int AmountOfObjectsToSort { get; private set; }

    private void Awake()
    {
        _machineManager = FindObjectOfType<MachineManager>();
    }

    private void Start()
    {
        _sortingAlgorithms = _machineManager.GetAlgorithms();
        PopulateAlgorithmDropDown();
    }

    private void PopulateAlgorithmDropDown()
    {
        List<string> names = _sortingAlgorithms.Keys.ToList();
        algorithmDropdown.ClearOptions();
        algorithmDropdown.AddOptions(names);
    }
    
    public void SortButtonPressed()
    {
        SelectedAlgorithmName = algorithmDropdown.options[algorithmDropdown.value].text;
        AmountOfObjectsToSort = (int) objectNumberSlider.value;
        
        _machineManager.SetAmountOfObjectsToSort(AmountOfObjectsToSort);
        _machineManager.Sort(_sortingAlgorithms[SelectedAlgorithmName]);
        
        OnSortButtonPressed.Invoke();
    }

    
}
