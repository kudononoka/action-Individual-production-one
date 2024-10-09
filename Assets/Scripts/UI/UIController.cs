using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI")]

    [SerializeField, Tooltip("PlayerHP�o�[")]
    Slider _playerHpVer;

    [SerializeField, Tooltip("PlayerST�o�[")] 
    Slider _playerStVer;

    [SerializeField, Tooltip("EnemyHP�o�[")]
    Slider _enemyHpVer;

    [SerializeField, Tooltip("PlayerHPText")] 
    Text _playerHpValueText;

    [SerializeField, Tooltip("Enemy�̐�Text")]
    Text _enemyCountText;

    [Header("�p�����[�^�[�ݒ�")]
    [SerializeField, Tooltip("�o�[�ω�����")] 
    float _changeTime = 0.3f;

    /// <summary>HP�ő�l</summary>
    int _playerHpMax;
    /// <summary>���݂�HP</summary>
    int _playerHpNow;
    /// <summary>ST�ő�l</summary>
    float _playerStMax;
    /// <summary>���݂�ST</summary>
    float _playerStNow;

    /// <summary>Enemy�̌��݂�Hp</summary>
    int _enemyHpNow;
    /// <summary>Enemy��HP�ő�l</summary>
    int _enemyHpMax;

    int _enemyMaxCount;
    int _enemyKillCount;

    /// <summary>HP��Max�ɐݒ肷��</summary>
    /// <param name="value">HP�ő�l</param>
    public void PlayerSetUpMaxHP(int value)
    {
        _playerHpMax = value;
        _playerHpNow = _playerHpMax;
        _playerHpVer.maxValue = _playerHpMax;
        _playerHpVer.value = _playerHpMax;
        _playerHpValueText.text = $"{_playerHpNow} / {_playerHpMax}";
    }

    /// <summary>HP��Max�ɐݒ肷��</summary>
    /// <param name="value">HP�ő�l</param>
    public void EnemySetUpMaxHP(int value)
    {
        _enemyHpMax = value;
        _enemyHpNow = _enemyHpMax;
        _enemyHpVer.maxValue = _enemyHpMax;
        _enemyHpVer.value = _enemyHpMax;
    }

    /// <summary>ST��Max�ɐݒ肷��</summary>
    /// <param name="value">ST�ő�l</param>
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

    /// <summary>���݂�HP�l��Ver�̒l�ɐݒ�</summary>
    /// <param name="currentValue">���݂�HP�l</param>
    public void PlayerSetCurrentHP(int currentValue)
    {
        PlayerChangingVerValueHP(currentValue);
    }

    /// <summary>���݂�HP�l��Ver�̒l�ɐݒ�</summary>
    /// <param name="currentValue">���݂�HP�l</param>
    public void EnemySetCurrentHP(int currentValue)
    {
        EnemyChangingVerValueHP(currentValue);
    }

    /// <summary>���݂�ST�l��Ver�̒l�ɐݒ�</summary>
    /// <param name="newHpValue">���݂�ST�l</param>
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

    /// <summary>DoTween��Ver�̒l�����炩�ɕω�������</summary>
    /// <param name="value">�ݒ肵����HP�l</param>
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

    /// <summary>DoTween��Ver�̒l�����炩�ɕω�������</summary>
    /// <param name="value">�ݒ肵����HP�l</param>
    public void EnemyChangingVerValueHP(int value)
    {
        DOTween.To(() => _enemyHpNow,
                    x =>
                    {
                        _enemyHpNow = x;
                        _enemyHpVer.value = _enemyHpNow;
                    }, value, _changeTime);
    }

    /// <summary>DoTween��Ver�̒l�����炩�ɕω�������</summary>
    /// <param name="value">�ݒ肵����ST�l</param>
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
