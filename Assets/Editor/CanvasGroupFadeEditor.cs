using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CanvasGroupFade))]
    public class CanvasGroupFadeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Fade In"))
            {
                CanvasGroupFade canvasGroupFade = (CanvasGroupFade) target;
                canvasGroupFade.FadeIn();
            }
            
            if (GUILayout.Button("Fade Out"))
            {
                CanvasGroupFade canvasGroupFade = (CanvasGroupFade) target;
                canvasGroupFade.FadeOut();
            }
        }
    }
}