using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RailMovement))]
[RequireComponent(typeof(StandSpawner))]
public class StandBeltManager : MonoBehaviour
{
    private int _amountOfObjectsToSpawn = 2;
    
    private StandSpawner _spawner;
    private Stand[] _stands;
    private int _currentOffset = 0;

    public int Length => _stands.Length;

    private RailMovement _railMovement;

    private void Awake()
    {
        _railMovement = GetComponent<RailMovement>();
        _spawner = GetComponent<StandSpawner>();
    }
    
    public void MoveOneLeft()
    {
        if (_railMovement.Moving) return;

        Vector3 targetPosition = transform.position - transform.forward * _spawner.Spacing;
        _railMovement.MoveTo(targetPosition);

        _currentOffset++;
    }

    public void MoveOneRight()
    {
        if (_railMovement.Moving) return;
        
        Vector3 targetPosition = transform.position + transform.forward * _spawner.Spacing;
        _railMovement.MoveTo(targetPosition);

        _currentOffset--;
    }

    public void MoveToOffset(int offset, bool instantaneous=false)
    {
        Vector3 targetPosition = transform.position + transform.forward * _spawner.Spacing * (_currentOffset - offset);

        if (instantaneous)
            transform.position = targetPosition;
        else
            _railMovement.MoveTo(targetPosition);
        
        _currentOffset = offset;
    }

    public void MoveLeft(int times)
    {
        MoveToOffset(_currentOffset + times);
    }

    public void MoveRight(int times)
    {
        MoveToOffset(_currentOffset - times);
    }

    public IComparable GetSortableAtIndex(int index)
    {
        return GetBeltPiecePositionAtIndex(index).GetSortable();
    }
    
    public Stand GetBeltPiecePositionAtIndex(int index)
    {
        try
        {
            return _stands[index];
        }
        catch (IndexOutOfRangeException)
        {
            Debug.LogError($"GetSortableAtIndex was called with index {index} while stands array consists of {_stands.Length} elements");
            throw;
        }
    }

    public IComparable[] GetSortables()
    {
        return (from stand in _stands select stand.GetSortable()).ToArray();
    }

    public bool IsIdle()
    {
        return !_railMovement.Moving;
    }


    public void SpawnObjectsToSort(int numberOfObjectsToSort)
    {
        _spawner.Spawn(numberOfObjectsToSort);
        _stands = _spawner.GetStands();
    }

    public void SpawnObjectsToSort()
    {
        SpawnObjectsToSort(_amountOfObjectsToSpawn);
    }

    public void SetAmountOfObjectToSpawn(int amountOfObjectsToSort)
    {
        _amountOfObjectsToSpawn = amountOfObjectsToSort;
    }
}
