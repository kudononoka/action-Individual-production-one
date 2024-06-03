using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI")]

    [SerializeField, Tooltip("PlayerHPバー")]
    Slider _playerHpVer;

    [SerializeField, Tooltip("PlayerSTバー")] 
    Slider _playerStVer;

    [SerializeField, Tooltip("PlayerHPText")] 
    Text _playerHpValueText;

    [SerializeField, Tooltip("EnemyHPバー")]
    Slider _enemyHpVer;

    [Header("パラメーター設定")]
    [SerializeField, Tooltip("バー変化時間")] 
    float _changeTime = 0.3f;

    /// <summary>HP最大値</summary>
    int _playerHpMax;
    /// <summary>現在のHP</summary>
    int _playerHpNow;
    /// <summary>ST最大値</summary>
    float _playerStMax;
    /// <summary>現在のST</summary>
    float _playerStNow;

    /// <summary>HP最大値</summary>
    int _enemyHpMax;
    /// <summary>現在のHP</summary>
    int _enemyHpNow;

    /// <summary>HPをMaxに設定する</summary>
    /// <param name="value">HP最大値</param>
    public void SetUpMaxHP(int value)
    {
        _playerHpMax = value;
        _playerHpNow = _playerHpMax;
        _playerHpVer.maxValue = _playerHpMax;
        _playerHpVer.value = _playerHpMax;
        _playerHpValueText.text = $"{_playerHpMax} / {_playerHpNow}";
    }

    public void SetUpMaxEnemyHP(int value)
    {
        _enemyHpMax = value;
        _enemyHpNow = _enemyHpMax;
        _enemyHpVer.maxValue = _enemyHpMax;
        _enemyHpVer.value = _enemyHpMax;
    }

    /// <summary>STをMaxに設定する</summary>
    /// <param name="value">ST最大値</param>
    public void SetUpMaxST(float value)
    {
        _playerStMax = value;
        _playerStNow = _playerStMax;
        _playerStVer.maxValue = _playerStMax;
        _playerStVer.value = _playerStMax;
    }

    /// <summary>現在のHP値をVerの値に設定</summary>
    /// <param name="currentValue">現在のHP値</param>
    public void SetCurrentHP(int currentValue)
    {
        ChangingVerValueHP(currentValue);
    }

    /// <summary>現在のST値をVerの値に設定</summary>
    /// <param name="newHpValue">現在のST値</param>
    public void SetCurrentST(float currentValue)
    {
        _playerStNow = currentValue;
        _playerStVer.value = _playerStNow;
        //ChangingVerValueSP(currentValue);
    }

    public void SetCurrentEnemyHP(int currentValue)
    {
        _enemyHpNow = currentValue;
        _enemyHpVer.value = _enemyHpNow;
    }

    /// <summary>DoTweenでVerの値を滑らかに変化させる</summary>
    /// <param name="value">設定したいHP値</param>
    public void ChangingVerValueHP(int value)
    {
        DOTween.To(() => _playerHpNow,
                    x =>
                    {
                        _playerHpNow = x;
                        _playerHpVer.value = _playerHpNow;
                        _playerHpValueText.text = $"{$"{_playerHpMax} / {_playerHpNow}"}";
                    }, value, _changeTime);
    }

    /// <summary>DoTweenでVerの値を滑らかに変化させる</summary>
    /// <param name="value">設定したいST値</param>
    public void ChangingVerValueSP(float value)
    {
        DOTween.To(() => _playerStNow,
                    x =>
                    {
                        _playerStNow = x;
                        _playerStVer.value = _playerStNow;
                    }, value, _changeTime);
    }
}
