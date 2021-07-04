using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;

public class PixelDisplay : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    [SerializeField] private CharData firstCharData;
    [SerializeField] private CharData secondCharData;

    [SerializeField] private int xFirstOffset;
    [SerializeField] private int yFirstOffset;

    [SerializeField] private int xSecondOffset;
    [SerializeField] private int ySecondOffset;

    [SerializeField] private Color foregroundColor = Color.white;
    [SerializeField] private Color backgroundColor = Color.black;
    
    private Pixel[,] _pixels;

    private void Awake()
    {
        InitializeScreen();
    }

    private void InitializeScreen()
    {
        _pixels = new Pixel[rows, columns];
        Pixel[] foundPixels = GetComponentsInChildren<Pixel>();

        if (rows * columns != foundPixels.Length)
        {
            Debug.LogError("Rows * Columns not equal found pixels");
            return;
        }

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                _pixels[r, c] = foundPixels[r * columns + c];
                _pixels[r, c].SetState(true);
            }
        }
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Clear();
            if (firstCharData != null)
            {
                foreach (PixelData pixelData in firstCharData.pixelsLit)
                {
                    Pixel pixel = _pixels[(int) pixelData.Coordinates.x + xFirstOffset, (int) pixelData.Coordinates.y + yFirstOffset];
                    pixel.SetEmissionColor(Color.red);
                }
            }
                
            if (secondCharData != null)
            {
                foreach (PixelData pixelData in secondCharData.pixelsLit)
                {
                    Pixel pixel = _pixels[(int) pixelData.Coordinates.x + xSecondOffset, (int) pixelData.Coordinates.y + ySecondOffset];
                    pixel.SetEmissionColor(Color.yellow);
                }
            }
        }
    }

    private void Clear()
    {
        if (_pixels == null) return;
        
        foreach (Pixel pixel in _pixels)
        {
            pixel.SetEmissionColor(backgroundColor);
        }
    }
    
    public void Print(String value)
    {
        CharData first = CharDatabase.GetCharDataFor(value[0]);
        PrintCharAtCoordinates(first, xFirstOffset, yFirstOffset, Color.yellow);

        if (value.Length == 2)
        {
            CharData second = CharDatabase.GetCharDataFor(value[1]);
            PrintCharAtCoordinates(second, xSecondOffset, ySecondOffset, Color.red);
        }
        
    }

    private void PrintCharAtCoordinates(CharData charData, int xOffset, int yOffset, Color color)
    {
        foreach (PixelData pixelData in charData.pixelsLit)
        {
            Pixel pixel = _pixels[(int) pixelData.Coordinates.x + xOffset, (int) pixelData.Coordinates.y + yOffset];
            pixel.SetEmissionColor(color);
        }
    }
}
