using UnityEngine;

/// <summary>�����I�ɉ��𗧂����闧�����Ă��Ȃ��𑀍삷��N���X</summary>
public class MakeASound : MonoBehaviour
{
    bool _isSound = false;

    public bool IsSound => _isSound;

    /// <summary>���𗧂����邩�ǂ���</summary>
    /// <param name="isSound">�������邩�ǂ���</param>
    public void IsSoundChange(bool isSound)
    {
        _isSound = isSound;
    }
}
