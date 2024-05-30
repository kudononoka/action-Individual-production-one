using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentDataEditorWindow : EditorWindow
{
    private EnvironmentData _data;
    private static string aseetpath = "";
    List<GameObject> _gameObjects = new List<GameObject>();
    List<string> _gameObjectNames = new List<string>();
    int _targetObjectID = 0;
    int _myObjectID = 0;

    public static void Open(EnvironmentData data)
    {
        aseetpath = AssetDatabase.GetAssetPath(data);
        var window = (EnvironmentDataEditorWindow)GetWindow(typeof(EnvironmentDataEditorWindow));
        window.Show();
    }

    private void OnEnable()
    {
        _data = (EnvironmentData)AssetDatabase.LoadAssetAtPath(aseetpath, typeof(EnvironmentData));
        if (_data != null)
        {
            Load(_data);
        }
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        _targetObjectID = EditorGUILayout.Popup("Popup", _targetObjectID, _gameObjectNames.ToArray());
        _myObjectID = EditorGUILayout.Popup("Popup",_myObjectID, _gameObjectNames.ToArray());
        if (EditorGUI.EndChangeCheck()) 
        {
            _data.TargetSet(_gameObjects[_targetObjectID]);
            _data.MySet(_gameObjects[_myObjectID]);
        }
    }



    private void Load(EnvironmentData data)
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            // ヒエラルキーにあるゲームオブジェクト
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
            {
                //親のみ
                if (go.transform.parent == null)
                {
                    if (go != null)
                    {
                        _gameObjects.Add(go);
                        _gameObjectNames.Add(go.name);
                    }
                }
            }
        }

        for(int i = 0; i < _gameObjectNames.Count; i++)
        {
            if (_data.Target != null && _gameObjectNames[i] == _data.Target.name)
            {
                _targetObjectID = i;
            }
            else if(_data.My != null && _gameObjectNames[i] == _data.My.name)
            {
                _myObjectID = i;
            }
        }
    }
}
