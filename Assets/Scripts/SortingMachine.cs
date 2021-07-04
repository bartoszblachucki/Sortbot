using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable InconsistentNaming
#pragma warning disable 4014

public class SortingMachine : MonoBehaviour
{
    [SerializeField] private RoboticArm armA;
    [SerializeField] private RoboticArm armB;

    [SerializeField] private StandBeltManager belt;

    public UnityEvent OnSortingStarted;
    public UnityEvent OnSortingFinished;
    public UnityEvent OnComparision;

    private void Start()
    {
        GoToPositionAlongRail(armA, new Vector3(0, 0, -2));
        GoToPositionAlongRail(armB, new Vector3(0, 0, -2));
    }

    public async Task Sort(Func<Task> sortingAlgorithm)
    {
        WakeArmsUp();
        
        await WaitUntilMachineReady();

        OnSortingStarted.Invoke();
        await sortingAlgorithm();

        await WaitUntilMachineReady();
        
        OnSortingFinished.Invoke();
        
        DisableArms();
    }
    
    public Dictionary<string, Func<Task>> GetAlgorithms()
    {
        return new Dictionary<string, Func<Task>>()
        {
            {"Quick Sort", QuickSort},
            {"Insertion Sort", InsertionSort},
            {"Bubble Sort", BubbleSort},
        };
    }

    public void SetAmountOfObjectsToSort(int amountOfObjectsToSort)
    {
        belt.SetAmountOfObjectToSpawn(amountOfObjectsToSort);
    }

    public void SpawnObjectsToSort()
    {
        belt.SpawnObjectsToSort();
    }

    #region Asynchronous helper methods
    
    private async Task<int> CheckValueAtIndex(RoboticArm arm, int index, bool stayUp = false)
    {
        await PickUpAtIndex(arm, index);
        int value = GetSortableFromArm(arm).GetValue();
        
        if(!stayUp)
            await PutDownAtIndex(arm, index);

        return value;
    }

    private SortableBoxWithPixelDisplay GetSortableFromArm(RoboticArm arm)
    {
        return arm.HeldGameObject.GetComponent<SortableBoxWithPixelDisplay>();
    }

    private static async Task WaitUntilAsync(Func<bool> condition, int pollDelay = 25)
    {
        while (!condition())
        {
            await Task.Delay(pollDelay).ConfigureAwait(true);
        }
    }
    
    private async Task PickUpAtIndex(RoboticArm arm, int index)
    {
        await GoToIndex(arm, index);
        await PickUp(arm);
    }

    private async Task PutDownAtIndex(RoboticArm arm, int index)
    {
        await GoToIndex(arm, index);
        await PutDown(arm);
    }

    private Vector3 WorldToRailPosition(RoboticArm arm, Vector3 position)
    {
        var armPos = arm.transform.position;
        return new Vector3(armPos.x, armPos.y, position.z);
    }

    private Vector3 GetArmOffsetVector(RoboticArm arm)
    {
        return new Vector3(0, 0, arm.ArmOffset);
    }

    private async Task GoToIndex(RoboticArm arm, int index)
    {
        Vector3 beltPiecePosition = belt.GetBeltPiecePositionAtIndex(index).transform.position;
        Vector3 targetPosition = WorldToRailPosition(arm, beltPiecePosition) + GetArmOffsetVector(arm);
        
        arm.StartMovingTowards(targetPosition);
        await WaitUntilArmReady(arm);
    }

    private async Task WaitUntilMachineReady()
    {
        await WaitUntilArmReady(armA);

        await WaitUntilArmReady(armB);

        await WaitUntilBeltReady();
        
    }

    private async Task WaitUntilBeltReady()
    {
        await WaitUntilAsync(belt.IsIdle);
    }

    private async Task WaitUntilArmReady(RoboticArm arm)
    {
        await WaitUntilAsync(arm.IsIdle);
    }

    private async Task WaitUntilBothArmsReady()
    {
        await WaitUntilAsync(armA.IsIdle);
        await WaitUntilAsync(armB.IsIdle);
    }

    private async Task PickUpBoth()
    {
        await Task.WhenAll(PickUp(armA), PickUp(armB));
    }

    private async Task PutDownBoth()
    {
        await Task.WhenAll(PutDown(armA), PutDown(armB));
    }
    
    private async Task PickUp(RoboticArm arm)
    {
        await WaitUntilBeltReady();
        await WaitUntilArmReady(arm);
        arm.StartPickUpSequence();
        await WaitUntilArmReady(arm);
    }

    private async Task PutDown(RoboticArm arm)
    {
        await WaitUntilBeltReady();
        await WaitUntilArmReady(arm);
        arm.StartPutDownSequence();
        await WaitUntilArmReady(arm);
    }

    private async Task GoToPositionAlongRail(RoboticArm arm, Vector3 position)
    {
        Vector3 railPosition = WorldToRailPosition(arm, position);
        arm.StartMovingTowards(railPosition);
        await WaitUntilArmReady(arm);
    }
    
    #endregion
    
    #region Sorting algorithms
    
    public async Task BubbleSort()
    {
        Debug.Log("Rozpoczynam BubbleSort");
        for (int i = 0; i < belt.Length; i++)
        {
            bool noSwaps = true;

            for (int j = 0; j < belt.Length - 1 - i; j++)
            {
                await Task.WhenAll(PickUpAtIndex(armA, j), PickUpAtIndex(armB, j+1));
                
                if (GetSortableFromArm(armA).GetValue() > GetSortableFromArm(armB).GetValue())
                {
                    noSwaps = false;
                    await Task.WhenAll(PutDownAtIndex(armA, j+1), PutDownAtIndex(armB, j));
                }
                else
                {
                    await PutDownBoth();
                }
                OnComparision.Invoke();

                await WaitUntilMachineReady();
            }
            
            await WaitUntilMachineReady();

            if (noSwaps)
                return;
        }
    }
    
    public async Task QuickSort()
    {
        Debug.Log("Rozpoczynam QuickSort");
        await QuickSortSub(0, belt.Length - 1);
    }

    private async Task QuickSortSub(int start, int end)
    {
        if (start >= end)
            return;
        
        int pivot = await QuickSortPartition(start, end);
        await QuickSortSub(start, pivot - 1);
        await QuickSortSub(pivot + 1, end);
    }
    
    private async Task<int> QuickSortPartition(int start, int end)
    {
        int pivotValue = await CheckValueAtIndex(armA, start);

        int i = start;
        int j = end;
        
        while (true)
        {
            while (await CheckValueAtIndex(armA, i) < pivotValue) { i++; OnComparision.Invoke();}
            while (await CheckValueAtIndex(armB, j) > pivotValue) { j--; OnComparision.Invoke();}

            if (i < j)
            {
                await Task.WhenAll(PickUpAtIndex(armA, i), PickUpAtIndex(armB, j));

                if (GetSortableFromArm(armA).GetValue() == GetSortableFromArm(armB).GetValue())
                {
                    await Task.WhenAll(PutDownAtIndex(armA, j), PutDownAtIndex(armB, i));
                    return j;
                }
                OnComparision.Invoke();
                
                await Task.WhenAll(PutDownAtIndex(armA, j), PutDownAtIndex(armB, i));
                
            }
            else
            {
                return j;
            }
        }
    }

    public async Task InsertionSort()
    {
        Debug.Log("Rozpoczynam InsertionSort");
        for (int i = 1; i < belt.Length; i++)
        {
            int key = await CheckValueAtIndex(armA, i, true);
            int j = i - 1;

            while (j >= 0 && await CheckValueAtIndex(armB, j, true) > key)
            {
                OnComparision.Invoke();
                await PutDownAtIndex(armB, j + 1);
                j -= 1;
            }

            if (j >= 0 && GetSortableFromArm(armB) != null)
            {
                OnComparision.Invoke();
                await PutDownAtIndex(armB, j);
            }

            await PutDownAtIndex(armA, j + 1);
        }
    }
    
    #endregion

    public void DisableArms()
    {
        armA.GoToSleep();
        armB.GoToSleep();
    }
    
    
    private void WakeArmsUp()
    {
        if (armA.IsSleeping)
            armA.WakeUp();
        if (armB.IsSleeping)
            armB.WakeUp();
    }
}
