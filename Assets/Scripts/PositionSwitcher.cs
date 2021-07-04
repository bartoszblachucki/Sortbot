using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSwitcher : MonoBehaviour
{

    [SerializeField] private Vector3[] positions;
    [SerializeField] private Vector3[] rotations;
    [SerializeField] private int positionOnAwake = 0;

    private int _currentPositionIndex;

    private void Awake()
    {
        if (positions.Length > 0)
        {
            SwitchToPosition(positionOnAwake);
        }
    }

    private void SwitchToPosition(int i)
    {
        _currentPositionIndex = i;

        Vector3 targetPosition = positions[i];
        Quaternion targetRotation = Quaternion.Euler(rotations[i]);

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    public void SwitchToNextPosition()
    {
        _currentPositionIndex += 1;
        _currentPositionIndex %= positions.Length;
        
        SwitchToPosition(_currentPositionIndex);
    }
}
