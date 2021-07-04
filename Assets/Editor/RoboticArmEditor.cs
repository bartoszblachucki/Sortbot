using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(RoboticArm))]
    [CanEditMultipleObjects]
    public class RoboticArmEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            
            base.OnInspectorGUI();

            if (GUILayout.Button("Pickup"))
            {
                if(!Application.isPlaying)
                    Debug.Log("Cant pick up while not playing");
                
                RoboticArm arm = (RoboticArm) target;
                arm.StartPickUpSequence();
            }
            
            if (GUILayout.Button("Drop"))
            {
                if(!Application.isPlaying)
                    Debug.Log("Cant put down while not playing");
                
                RoboticArm arm = (RoboticArm) target;
                arm.StartPutDownSequence();
            }
            
        }
    }
}