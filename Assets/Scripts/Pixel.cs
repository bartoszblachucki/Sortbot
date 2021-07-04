using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Pixel : MonoBehaviour
{
    [SerializeField] private Color emissionColor;
    private MeshRenderer _renderer;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private static readonly int EmissionScaleUI = Shader.PropertyToID("_EmissionScaleUI");

    private MaterialPropertyBlock _propBlock;

    private bool _initialized = false;
    
    private void Initialize()
    {
        _renderer = GetComponent<MeshRenderer>();
        _propBlock = new MaterialPropertyBlock();
        _initialized = true;
    }

    public void SetEmissionColor(Color color)
    {
        emissionColor = color;
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor(EmissionColor, emissionColor);
        _propBlock.SetFloat(EmissionScaleUI, 1);
        _renderer.SetPropertyBlock(_propBlock);
    }
    
    public void SetState(bool on)
    {
        if (!_initialized)
            Initialize();
        
        if (on)
        {
            SetEmissionColor(emissionColor);
        }
        else
        {
            SetEmissionColor(Color.black);
        }
    }
}