using UnityEngine;

/// <summary>�v���C���[�̃p�����[�^�[</summary>
[System.Serializable]
public class PlayerParameter
{
    [Header("���s���x")]
    [SerializeField] float _walkSpeed;

    [Header("�K�[�h���̕��s���x")]
    [SerializeField] float _guardWalkSpeed;

    [Header("�����]�����x")]
    [SerializeField] float _rotateSpeed;

    public float WalkSpeed => _walkSpeed;

    public float RotateSpeed => _rotateSpeed;

    public float GuardWalkSpeed => _guardWalkSpeed; 
}