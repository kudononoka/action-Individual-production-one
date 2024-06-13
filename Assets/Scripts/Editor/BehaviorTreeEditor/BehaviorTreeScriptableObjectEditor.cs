using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[CustomEditor(typeof(BehaviorTreeScriptableObject))]
public class BehaviorTreeScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BehaviorTreeScriptableObject myTarget = (BehaviorTreeScriptableObject)target;

        if (GUILayout.Button("Open Editor"))
        {
            BehaviorTreeEditorWindow.Open(myTarget);
        }

        DrawDefaultInspector();
    }

    [OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID)
    {
        BehaviorTreeScriptableObject obj = EditorUtility.InstanceIDToObject(instanceID) as BehaviorTreeScriptableObject;
        if (obj != null)
        {
            BehaviorTreeEditorWindow.Open(obj);
            return true;
        }
        return false;
    }
}
