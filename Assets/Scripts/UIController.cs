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

    [SerializeField, Tooltip("PlayerHPText")] 
    Text _playerHpValueText;

    [SerializeField, Tooltip("EnemyHP�o�[")]
    Slider _enemyHpVer;

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

    /// <summary>HP�ő�l</summary>
    int _enemyHpMax;
    /// <summary>���݂�HP</summary>
    int _enemyHpNow;

    /// <summary>HP��Max�ɐݒ肷��</summary>
    /// <param name="value">HP�ő�l</param>
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

    /// <summary>ST��Max�ɐݒ肷��</summary>
    /// <param name="value">ST�ő�l</param>
    public void SetUpMaxST(float value)
    {
        _playerStMax = value;
        _playerStNow = _playerStMax;
        _playerStVer.maxValue = _playerStMax;
        _playerStVer.value = _playerStMax;
    }

    /// <summary>���݂�HP�l��Ver�̒l�ɐݒ�</summary>
    /// <param name="currentValue">���݂�HP�l</param>
    public void SetCurrentHP(int currentValue)
    {
        ChangingVerValueHP(currentValue);
    }

    /// <summary>���݂�ST�l��Ver�̒l�ɐݒ�</summary>
    /// <param name="newHpValue">���݂�ST�l</param>
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

    /// <summary>DoTween��Ver�̒l�����炩�ɕω�������</summary>
    /// <param name="value">�ݒ肵����HP�l</param>
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
