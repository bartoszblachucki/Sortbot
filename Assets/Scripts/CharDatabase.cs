using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDatabase : MonoBehaviour
{
    [SerializeField] private CharSet charSet;
    private static CharDatabase instance;

    private Dictionary<char, CharData> charToData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeCharDictionary();
    }

    private void InitializeCharDictionary()
    {
        charToData = new Dictionary<char, CharData>();
        foreach (CharData charData in charSet.chars)
        {
            instance.charToData[charData.encodedChar] = charData;
        }
    }
    
    public static CharData GetCharDataFor(char c)
    {
        return instance.charToData[c];
    }
}
