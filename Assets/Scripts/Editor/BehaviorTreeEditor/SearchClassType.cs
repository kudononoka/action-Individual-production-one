using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SearchClassType
{
    private Assembly[] _assemblyArray = null;

    /// <summary>クラス名からクラスのインスタンス生成</summary>
    /// <param name="className">クラス名</param>
    /// <returns></returns>
    public object CreateInstance(string className)
    {
        Type type = GetTypeByName(className);       // アセンブリからクラスの型を取得
        if (type == null)                           // 目的のクラス型がみつからなかっらたら
        {
            Debug.LogError($"Class {className} not found.");　//エラー表示
            return null;
        }

        return Activator.CreateInstance(type);
    }

    /// <summary>アセンブリからクラスの型を取得する用</summary>
    /// <param name="className">クラス名</param>
    /// <returns>クラスのTypeを返す/なかったらnullを返す</returns>
    private Type GetTypeByName(string className)
    {
        if(_assemblyArray == null)
        {
            _assemblyArray = AppDomain.CurrentDomain.GetAssemblies();　//Unityエディター上のアセンブリを取得する
        }

        foreach (Assembly assembly in _assemblyArray)　
        {
            if (assembly.FullName.StartsWith("Assembly-CSharp"))　//アセンブリ内のCSharpを対象にする
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == className)             // 自作クラス名に一致する場合
                    {
                        return type;
                    }
                }
            }
        }
        return null;
    }
}
