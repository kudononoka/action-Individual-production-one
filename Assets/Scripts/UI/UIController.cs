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

    [SerializeField, Tooltip("EnemyHPバー")]
    Slider _enemyHpVer;

    [SerializeField, Tooltip("PlayerHPText")] 
    Text _playerHpValueText;

    [SerializeField, Tooltip("Enemyの数Text")]
    Text _enemyCountText;

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

    /// <summary>Enemyの現在のHp</summary>
    int _enemyHpNow;
    /// <summary>EnemyのHP最大値</summary>
    int _enemyHpMax;

    int _enemyMaxCount;
    int _enemyKillCount;

    /// <summary>HPをMaxに設定する</summary>
    /// <param name="value">HP最大値</param>
    public void PlayerSetUpMaxHP(int value)
    {
        _playerHpMax = value;
        _playerHpNow = _playerHpMax;
        _playerHpVer.maxValue = _playerHpMax;
        _playerHpVer.value = _playerHpMax;
        _playerHpValueText.text = $"{_playerHpNow} / {_playerHpMax}";
    }

    /// <summary>HPをMaxに設定する</summary>
    /// <param name="value">HP最大値</param>
    public void EnemySetUpMaxHP(int value)
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

    public void SetUpEnemyCount(int count)
    {
        _enemyKillCount = 0;
        _enemyMaxCount = count;
        if (_enemyCountText == null)
        {
            _enemyCountText = GameObject.Find("EnemyCount").GetComponent<Text>();
        }
        _enemyCountText.text = $"{_enemyKillCount} / {_enemyMaxCount}";
    }

    /// <summary>現在のHP値をVerの値に設定</summary>
    /// <param name="currentValue">現在のHP値</param>
    public void PlayerSetCurrentHP(int currentValue)
    {
        PlayerChangingVerValueHP(currentValue);
    }

    /// <summary>現在のHP値をVerの値に設定</summary>
    /// <param name="currentValue">現在のHP値</param>
    public void EnemySetCurrentHP(int currentValue)
    {
        EnemyChangingVerValueHP(currentValue);
    }

    /// <summary>現在のST値をVerの値に設定</summary>
    /// <param name="newHpValue">現在のST値</param>
    public void SetCurrentST(float currentValue)
    {
        _playerStNow = currentValue;
        _playerStVer.value = _playerStNow;
        //ChangingVerValueSP(currentValue);
    }

    public void SetCurrentEnemyKillCount(int count)
    {
        _enemyKillCount = count;
        if(_enemyCountText != null)
        _enemyCountText.text = $"{_enemyKillCount} / {_enemyMaxCount}";
    }

    /// <summary>DoTweenでVerの値を滑らかに変化させる</summary>
    /// <param name="value">設定したいHP値</param>
    public void PlayerChangingVerValueHP(int value)
    {
        DOTween.To(() => _playerHpNow,
                    x =>
                    {
                        _playerHpNow = x;
                        _playerHpVer.value = _playerHpNow;
                        _playerHpValueText.text = $"{$"{_playerHpNow} / {_playerHpMax}"}";
                    }, value, _changeTime);
    }

    /// <summary>DoTweenでVerの値を滑らかに変化させる</summary>
    /// <param name="value">設定したいHP値</param>
    public void EnemyChangingVerValueHP(int value)
    {
        DOTween.To(() => _enemyHpNow,
                    x =>
                    {
                        _enemyHpNow = x;
                        _enemyHpVer.value = _enemyHpNow;
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
