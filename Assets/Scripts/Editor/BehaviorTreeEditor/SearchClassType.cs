using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SearchClassType
{
    private Assembly[] _assemblyArray = null;

    /// <summary>�N���X������N���X�̃C���X�^���X����</summary>
    /// <param name="className">�N���X��</param>
    /// <returns></returns>
    public object CreateInstance(string className)
    {
        Type type = GetTypeByName(className);       // �A�Z���u������N���X�̌^���擾
        if (type == null)                           // �ړI�̃N���X�^���݂���Ȃ����炽��
        {
            Debug.LogError($"Class {className} not found.");�@//�G���[�\��
            return null;
        }

        return Activator.CreateInstance(type);
    }

    /// <summary>�A�Z���u������N���X�̌^���擾����p</summary>
    /// <param name="className">�N���X��</param>
    /// <returns>�N���X��Type��Ԃ�/�Ȃ�������null��Ԃ�</returns>
    private Type GetTypeByName(string className)
    {
        if(_assemblyArray == null)
        {
            _assemblyArray = AppDomain.CurrentDomain.GetAssemblies();�@//Unity�G�f�B�^�[��̃A�Z���u�����擾����
        }

        foreach (Assembly assembly in _assemblyArray)�@
        {
            if (assembly.FullName.StartsWith("Assembly-CSharp"))�@//�A�Z���u������CSharp��Ώۂɂ���
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == className)             // ����N���X���Ɉ�v����ꍇ
                    {
                        return type;
                    }
                }
            }
        }
        return null;
    }
}
