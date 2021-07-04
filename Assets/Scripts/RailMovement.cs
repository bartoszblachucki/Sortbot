using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeedFactor = 0.01f;

    public bool Moving { get; private set; } = false;
    public Vector3 Velocity => _velocity;
    
    private Vector3 _velocity = Vector3.zero;

    public void MoveTo(Vector3 position)
    {
        StartCoroutine(MovingTowards(position));
    }
    
    private IEnumerator MovingTowards(Vector3 targetPosition)
    {
        Moving = true;
        
        float distance;
        do
        {
            distance = Vector3.Distance(transform.position, targetPosition);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, Time.deltaTime / movementSpeedFactor / Time.timeScale);
            yield return null;
        } while (distance > 0.025f);

        transform.position = targetPosition;
        Moving = false;

    }
}
