using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable InconsistentNaming
#pragma warning disable 4014

public class MachineManager : MonoBehaviour
{
    [SerializeField] private SortingMachine sortingMachine;
    [SerializeField] private StandBeltManager conveyorBelt;
    [SerializeField] private Scanner scanner;
    [SerializeField] private Door door;

    private static readonly int StartSequence = Animator.StringToHash("StartSequence");
    private static readonly int RestartSequence = Animator.StringToHash("RestartSequence");

    private Animator _animator;
    private bool _chamberEmpty = true;
    private Func<Task> _selectedSortingAlgorithm;

    public UnityEvent OnRestartInitiated;
    public UnityEvent OnRestartFinished;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        conveyorBelt.MoveToOffset(-50, true);
    }

    public void StartRestartSequence()
    {
        OnRestartInitiated.Invoke();
        _animator.SetTrigger(RestartSequence);
        _chamberEmpty = true;
    }

    private void OnEnable()
    {
        sortingMachine.OnSortingFinished.AddListener(Scan);
        //scanner.OnScanFinished.AddListener(DisableMachine);
    }

    private void OnDisable()
    {
        sortingMachine.OnSortingFinished.RemoveListener(Scan);
        //scanner.OnScanFinished.RemoveListener(DisableMachine);
    }

    private void OpenDoor()
    {
        door.Open();
    }
    
    private void CloseDoor()
    {
        door.Close();
    }
    
    private void MoveBeltIn()
    {
        conveyorBelt.MoveToOffset(0);
    }

    private void MoveBeltOut()
    {
        conveyorBelt.MoveToOffset(-60);
    }

    private async void StartSorting()
    {
        await sortingMachine.Sort(_selectedSortingAlgorithm);
    }

    private void Scan()
    {
        scanner.Scan();
    }

    public Dictionary<string, Func<Task>> GetAlgorithms()
    {
        return sortingMachine.GetAlgorithms();
    }

    public void Sort(Func<Task> sortingAlgorithm)
    {
        _selectedSortingAlgorithm = sortingAlgorithm;

        StartSortingSequence();
    }

    private void StartSortingSequence()
    {
        if (_chamberEmpty)
        {
            sortingMachine.SpawnObjectsToSort();
            _animator.SetTrigger(StartSequence);
            _chamberEmpty = false;
        }
            
        else
        {
            StartRestartSequence();
        }
    }

    public void SetAmountOfObjectsToSort(int amountOfObjectsToSort)
    {
        sortingMachine.SetAmountOfObjectsToSort(amountOfObjectsToSort);
    }

    public void InvokeRestartFinished()
    {
        OnRestartFinished.Invoke();
    }
}
