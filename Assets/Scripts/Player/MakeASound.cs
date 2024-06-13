using UnityEngine;

/// <summary>明示的に音を立たせる立たせていないを操作するクラス</summary>
public class MakeASound : MonoBehaviour
{
    bool _isSound = false;

    /// <summary>Trueだったら音を立たせている</summary>
    public bool IsSound => _isSound;

    /// <summary>音を立たせるかどうか</summary>
    /// <param name="isSound">立たせるかどうか</param>
    public void IsSoundChange(bool isSound)
    {
        _isSound = isSound;
    }
}
