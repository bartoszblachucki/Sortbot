using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Stand : MonoBehaviour
{
    [SerializeField] private Transform proximitySensor;
    [SerializeField] private GameObject sortableGameObject;
    [SerializeField] private Transform childPositionTransform;
    [SerializeField] private LayerMask pickableLayers;

    public void SetSortable(GameObject sortable)
    {
        sortableGameObject = sortable;
        MoveSortableIntoPlace(sortable);
    }

    private void MoveSortableIntoPlace(GameObject colorSortable)
    {
        colorSortable.transform.SetParent(childPositionTransform);
        colorSortable.transform.rotation = childPositionTransform.rotation;
    }

    private void OnValidate()
    {
        if(sortableGameObject != null)
            MoveSortableIntoPlace(sortableGameObject);
    }

    public bool IsEmpty()
    {
        return true;
    }

    public IComparable GetSortable()
    {
        return sortableGameObject.GetComponent<IComparable>();
    }
    
    private void Update()
    {
        /*if (Physics.Raycast(proximitySensor.transform.position, proximitySensor.transform.forward, out RaycastHit hit, 0.1f, pickableLayers))
        {
            sortableGameObject = hit.collider.gameObject;
        }
        else
        {
            sortableGameObject = null;
        }*/
    }
}
