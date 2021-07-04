using System;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class StandSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject standPrefab;
    
    [Range(2, 20)] [SerializeField] private int standAmount;
    [SerializeField] private float spacing = 1.5f;
    public float Spacing => spacing;

    private List<GameObject> stands = new List<GameObject>();
    
    void Awake()
    {
        Spawn();

        //RandomizeSortables();
    }

    private void RandomizeSortables()
    {
        int[] randomColorValues = new int[standAmount];
        for (int i = 0; i < standAmount; i++)
        {
            randomColorValues[i] = Random.Range(0, 255);
            Debug.Log(randomColorValues[i]);   
        }

        SortableBoxWithPixelDisplay[] sortables = GetComponentsInChildren<SortableBoxWithPixelDisplay>();
        for (int i = 0; i < standAmount; i++)
        {
            sortables[i].SetValue(randomColorValues[i]);
        }
    }

    public void Clear()
    {
        for (int i = transform.childCount; i > 0; i--)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
        stands.Clear();
    }

    public void Spawn()
    {
        Clear();
        
        for (int i = 0; i < standAmount; i++)
        {
            Vector3 standPosition = transform.position + Vector3.forward * (spacing * (i + 1));
            GameObject stand = Instantiate(standPrefab, standPosition, Quaternion.identity, transform);
            stands.Add(stand);
        }
    }

    public void Spawn(int numberOfObjectsToSort)
    {
        standAmount = numberOfObjectsToSort;
        Spawn();
    }

    public Stand[] GetStands()
    {
        return GetComponentsInChildren<Stand>();
    }
}
