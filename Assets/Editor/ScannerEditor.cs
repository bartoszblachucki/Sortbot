using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Scanner))]
    public class ScannerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Scan"))
            {
                Scanner scanner = (Scanner) target;
                scanner.Scan();
            }
            
        }
    }
}