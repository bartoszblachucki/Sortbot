using UnityEngine;

[System.Serializable]
public struct PixelData
{
    public Vector2 Coordinates;
}
    
[CreateAssetMenu(fileName = "Char", menuName = "Char", order = 0)]
public class CharData : ScriptableObject
{
    public char encodedChar;
    public PixelData[] pixelsLit;
}