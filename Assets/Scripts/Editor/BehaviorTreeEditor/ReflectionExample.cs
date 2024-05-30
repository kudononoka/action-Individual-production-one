using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ReflectionExample
{
    public BehaviorTreeGraphView _graphView;
    private Assembly[] _assemblyArray = null;

    public object CreateInstance(string className)
    {
        // アセンブリからクラスの型を取得
        Type type = GetTypeByName(className);
        if (type == null)
        {
            Debug.LogError($"Class {className} not found.");
            return null;
        }

        // クラスのインスタンスを生成
        return Activator.CreateInstance(type);
    }

    private Type GetTypeByName(string className)
    {
        if(_assemblyArray == null)
        {
            _assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
        }

        // Unity エディタで実行されるアセンブリのみを対象とする
        foreach (Assembly assembly in _assemblyArray)
        {
            // Unity エディタで実行されるアセンブリのみを対象とする
            if (assembly.FullName.StartsWith("Assembly-CSharp"))
            {
                foreach (Type type in assembly.GetTypes())
                {
                    // 自作クラスの名前空間やクラス名のパターンに一致する場合のみ返す
                    if (type.Name == className)
                    {
                        return type;
                    }
                }
            }
        }
        return null;
    }
}
