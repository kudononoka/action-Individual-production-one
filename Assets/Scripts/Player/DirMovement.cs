using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>動いている方向が四方向のどれに値するのか判定するためのクラス</summary>
public class DirMovement
{
    public enum MoveDir
    {
        /// <summary>前</summary>
        Forward,
        /// <summary>後ろ</summary>
        Backward,
        /// <summary>左</summary>
        Left,
        /// <summary>右</summary>
        Right,
        /// <summary>そもそも動いていない</summary>
        NotMove,
    }

    /// <summary>四方向のどれに値するのか判定する</summary>
    /// <param name="moveDir">動いている方向のベクトル</param>
    /// <returns>前後右左どれかを返す</returns>
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
