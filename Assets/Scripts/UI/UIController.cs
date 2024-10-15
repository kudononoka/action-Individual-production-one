using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI")]

    [SerializeField, Tooltip("PlayerHPバー")]
    Slider _playerHpVer;

    [SerializeField, Tooltip("EnemyHPバー")]
    Slider _enemyHpVer;

    [SerializeField, Tooltip("PlayerHPText")] 
    Text _playerHpValueText;

    [Header("パラメーター設定")]
    [SerializeField, Tooltip("バー変化時間")] 
    float _changeTime = 0.3f;

    /// <summary>HP最大値</summary>
    int _playerHpMax;

    /// <summary>HPをMaxに設定する</summary>
    /// <param name="value">HP最大値</param>
    public void PlayerSetUpMaxHP(int value)
    {
        _playerHpVer.maxValue = value;
        _playerHpVer.value = value;
        _playerHpValueText.text = $"{_playerHpVer.value} / {_playerHpVer.maxValue}";
    }

    /// <summary>HPをMaxに設定する</summary>
    /// <param name="value">HP最大値</param>
    public void EnemySetUpMaxHP(int value)
    {
        _enemyHpVer.maxValue = value;
        _enemyHpVer.value = value;
    }

    /// <summary>現在のHP値をVerの値に設定</summary>
    /// <param name="currentValue">現在のHP値</param>
    public void PlayerSetCurrentHP(int currentValue)
    {
        _playerHpValueText.text = $"{currentValue} / {_playerHpVer.maxValue}";
        _playerHpVer.DOValue(currentValue, _changeTime);
    }

    /// <summary>現在のHP値をVerの値に設定</summary>
    /// <param name="currentValue">現在のHP値</param>
    public void EnemySetCurrentHP(int currentValue)
    {
        _enemyHpVer.DOValue(currentValue, _changeTime);
    }
}
