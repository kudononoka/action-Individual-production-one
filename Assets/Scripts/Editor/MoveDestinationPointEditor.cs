using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MoveDestinationPoint))]
public class MoveDestinationPointEditor : Editor
{
    MoveDestinationPoint _target;
    private void OnEnable()
    {
        _target = target as MoveDestinationPoint;
    }
    public override void OnInspectorGUI()
    {
        //ase.OnInspectorGUI();

        if (GUILayout.Button("MovePointAdd"))
        {
            DestinationPoint pos = new DestinationPoint();

            if (_target.Point.Length > 0)
            {
                pos._point = _target.Point[_target.Point.Length - 1]._point + Vector3.right;
            }
            else
            {
                pos._point = Vector3.right;
            }

            ArrayUtility.Add(ref _target.Point, pos);
            //加わった変更の差分のみ保存
            Undo.RecordObject(_target, "Add Node");
            EditorUtility.SetDirty(_target);
        }
        int delete = -1;
        for (int i = 0; i < _target.Point.Length && _target.Point.Length >= 1; i++)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();
            int size = 50;
            EditorGUILayout.BeginVertical(GUILayout.Width(size));
            if (GUILayout.Button("削除", GUILayout.Width(size)) && i != 0)
            {
                delete = i;
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            DestinationPoint pos = new DestinationPoint();
            pos._point = EditorGUILayout.Vector3Field($"目的地ID : {i}", _target.Point[i]._point, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_target, "Changed Position");
                EditorUtility.SetDirty(_target);
                _target.Point[i] = pos;
            }
        }

        if (delete != -1)
        {
            Undo.RecordObject(target, "Deleted Position");
            EditorUtility.SetDirty(_target);
            ArrayUtility.RemoveAt(ref _target.Point, delete);
        }
    }

    private void OnSceneGUI()
    {
        if (!EditorApplication.isPlaying)
        {
            for (int i = 0; i < _target.Point.Length; i++)
            {
                Vector3 pos;
                pos = _target.transform.TransformPoint(_target.Point[i]._point);
                Vector3 newPos;
                newPos = Handles.PositionHandle(pos, Quaternion.identity);
                if (i > 0)
                {
                    Vector3 fromPos;
                    fromPos = _target.transform.TransformPoint(_target.Point[i - 1]._point);
                    Handles.color = UnityEngine.Color.red;
                    Handles.DrawLine(pos, fromPos);
                }

                if (newPos != pos)
                {
                    Undo.RecordObject(_target, "Moved");
                    EditorUtility.SetDirty(_target);
                    _target.Point[i]._point = _target.transform.InverseTransformPoint(newPos);
                }

                Handles.Label(pos, $"目的地{i}");
            }
        }

    }
}
