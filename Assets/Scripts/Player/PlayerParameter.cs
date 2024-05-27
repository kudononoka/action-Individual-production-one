using UnityEngine;

/// <summary>プレイヤーのパラメーター</summary>
[System.Serializable]
public class PlayerParameter
{
    [Header("歩行速度")]
    [SerializeField] float _walkSpeed;

    [Header("ガード時の歩行速度")]
    [SerializeField] float _guardWalkSpeed;

    [Header("方向転換速度")]
    [SerializeField] float _rotateSpeed;

    public float WalkSpeed => _walkSpeed;

    public float RotateSpeed => _rotateSpeed;

    public float GuardWalkSpeed => _guardWalkSpeed; 
}