using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Door), true)]
    public class DoorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open"))
            {
                Door door = (Door) target;
                door.Open();
            }

            if (GUILayout.Button("Close"))
            {
                Door door = (Door) target;
                door.Close();
            }
        }
    }
}