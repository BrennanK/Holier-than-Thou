using UnityEngine;
using UnityEditor;

public class InspectorEditor : MonoBehaviour
{
    [CustomEditor(typeof(Renamer))]
    public class RenamerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Renamer renamer = (Renamer)target;

            if (GUILayout.Button("Rename Children"))
            {
                renamer.RenameChildren();
            }
        }
    }
}
