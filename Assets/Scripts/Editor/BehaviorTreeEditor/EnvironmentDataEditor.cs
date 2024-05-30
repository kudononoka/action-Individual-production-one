using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
[CustomEditor(typeof(EnvironmentData))]
public class EnvironmentDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnvironmentData myTarget = (EnvironmentData)target;

        if (GUILayout.Button("Open Editor"))
        {
            EnvironmentDataEditorWindow.Open(myTarget);
        }

        DrawDefaultInspector();
    }

    [OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID)
    {
        EnvironmentData obj = EditorUtility.InstanceIDToObject(instanceID) as EnvironmentData;
        if (obj != null)
        {
            EnvironmentDataEditorWindow.Open(obj);
            return true;
        }
        return false;
    }
}
