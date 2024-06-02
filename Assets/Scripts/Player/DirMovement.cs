using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�����Ă���������l�����̂ǂ�ɒl����̂����肷�邽�߂̃N���X</summary>
public class DirMovement
{
    public enum MoveDir
    {
        /// <summary>�O</summary>
        Forward,
        /// <summary>���</summary>
        Backward,
        /// <summary>��</summary>
        Left,
        /// <summary>�E</summary>
        Right,
        /// <summary>�������������Ă��Ȃ�</summary>
        NotMove,
    }

    /// <summary>�l�����̂ǂ�ɒl����̂����肷��</summary>
    /// <param name="moveDir">�����Ă�������̃x�N�g��</param>
    /// <returns>�O��E���ǂꂩ��Ԃ�</returns>
    public MoveDir DirMovementJudge(Vector2 moveDir)
    {
        Vector3 vec = moveDir.normalized;
        if (vec.magnitude == 0)
        {
            return MoveDir.NotMove;
        }

        if (vec.x >= -0.5 && vec.x <= 0.5)
        {
            if (vec.y >= 0)
            {
                return MoveDir.Forward;
            }
            else
            {
                return MoveDir.Backward;
            }
        }
        else
        {
            if (vec.x >= 0)
            {
                return MoveDir.Right;
            }
            else
            {
                return MoveDir.Left;
            }
        }
    }
}
