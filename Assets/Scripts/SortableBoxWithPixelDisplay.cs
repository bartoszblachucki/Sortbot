using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SortableBoxWithPixelDisplay : MonoBehaviour, IComparable<SortableBoxWithPixelDisplay>
{
    [SerializeField] private bool randomizeValueOnAwake = true;
    private PixelDisplay _display;
    private int _value;
    
    // Start is called before the first frame update
    void Start()
    {
        _display = GetComponent<PixelDisplay>();

        if (randomizeValueOnAwake)
        {
            SetValue(Random.Range(0, 99));
        }
    }
    
    public int GetValue()
    {
        return _value;
    }

    public void SetValue(int value)
    {
        _value = value;
        _display.Print(value.ToString());
    }

    public int CompareTo(SortableBoxWithPixelDisplay other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return _value.CompareTo(other._value);
    }
}
