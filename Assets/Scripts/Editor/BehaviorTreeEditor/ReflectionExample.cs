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
        // �A�Z���u������N���X�̌^���擾
        Type type = GetTypeByName(className);
        if (type == null)
        {
            Debug.LogError($"Class {className} not found.");
            return null;
        }

        // �N���X�̃C���X�^���X�𐶐�
        return Activator.CreateInstance(type);
    }

    private Type GetTypeByName(string className)
    {
        if(_assemblyArray == null)
        {
            _assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
        }

        // Unity �G�f�B�^�Ŏ��s�����A�Z���u���݂̂�ΏۂƂ���
        foreach (Assembly assembly in _assemblyArray)
        {
            // Unity �G�f�B�^�Ŏ��s�����A�Z���u���݂̂�ΏۂƂ���
            if (assembly.FullName.StartsWith("Assembly-CSharp"))
            {
                foreach (Type type in assembly.GetTypes())
                {
                    // ����N���X�̖��O��Ԃ�N���X���̃p�^�[���Ɉ�v����ꍇ�̂ݕԂ�
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
