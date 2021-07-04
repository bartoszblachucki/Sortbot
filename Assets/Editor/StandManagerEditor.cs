using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(StandBeltManager))]
    public class StandManagerEditor : UnityEditor.Editor
    {
        public int offset = 0;
        
        public override void OnInspectorGUI()
        {
            
            base.OnInspectorGUI();
            
            
            if (GUILayout.Button("Move Left"))
            {
                StandBeltManager beltManager = (StandBeltManager) target;
                beltManager.MoveOneLeft();
            }
            
            if (GUILayout.Button("Move Right"))
            {
                StandBeltManager beltManager = (StandBeltManager) target;
                beltManager.MoveOneRight();
            }

            if (GUILayout.Button("Move to offset"))
            {
                StandBeltManager beltManager = (StandBeltManager) target;
                beltManager.MoveToOffset(offset);
            }
            offset = EditorGUILayout.IntField("Offset:", offset);
        }
    }
}