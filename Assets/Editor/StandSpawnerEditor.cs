using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(StandSpawner))]
    public class StandSpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            
            base.OnInspectorGUI();

            if (GUILayout.Button("Update"))
            {
                StandSpawner spawner = (StandSpawner) target;
                spawner.Clear();
                spawner.Spawn();
            }
        }
    }
}