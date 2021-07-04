using System.Xml;
using UnityEngine;

public abstract class Door : MonoBehaviour
{
    public bool IsOpen { get; protected set; }

    public abstract void Open();
    public abstract void Close();
}