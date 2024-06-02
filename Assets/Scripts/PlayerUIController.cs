using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("UI")]

    [SerializeField, Tooltip("HP�o�[")]
    Slider _hpVer;

    [SerializeField, Tooltip("ST�o�[")] 
    Slider _stVer;

    [SerializeField, Tooltip("HPText")] 
    Text _hpValueText;

    [Header("�p�����[�^�[�ݒ�")]
    [SerializeField, Tooltip("�o�[�ω�����")] 
    float _changeTime = 0.3f;

    /// <summary>HP�ő�l</summary>
    int _hpMax;
    /// <summary>���݂�HP</summary>
    int _hpNow;
    /// <summary>ST�ő�l</summary>
    float _stMax;
    /// <summary>���݂�ST</summary>
    float _stNow;

    /// <summary>HP��Max�ɐݒ肷��</summary>
    /// <param name="value">HP�ő�l</param>
    public void SetUpMaxHP(int value)
    {
        _hpMax = value;
        _hpNow = _hpMax;
        _hpVer.maxValue = _hpMax;
        _hpVer.value = _hpMax;
        _hpValueText.text = $"{_hpMax} / {_hpNow}";
    }

    /// <summary>ST��Max�ɐݒ肷��</summary>
    /// <param name="value">ST�ő�l</param>
    public void SetUpMaxST(float value)
    {
        _stMax = value;
        _stNow = _stMax;
        _stVer.maxValue = _stMax;
        _stVer.value = _stMax;
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
        _stNow = currentValue;
        _stVer.value = _stNow;
        //ChangingVerValueSP(currentValue);
    }

    /// <summary>DoTween��Ver�̒l�����炩�ɕω�������</summary>
    /// <param name="value">�ݒ肵����HP�l</param>
    public void ChangingVerValueHP(int value)
    {
        DOTween.To(() => _hpNow,
                    x =>
                    {
                        _hpNow = x;
                        _hpVer.value = _hpNow;
                        _hpValueText.text = $"{$"{_hpMax} / {_hpNow}"}";
                    }, value, _changeTime);
    }

    /// <summary>DoTween��Ver�̒l�����炩�ɕω�������</summary>
    /// <param name="value">�ݒ肵����ST�l</param>
    public void ChangingVerValueSP(float value)
    {
        DOTween.To(() => _stNow,
                    x =>
                    {
                        _stNow = x;
                        _stVer.value = _stNow;
                    }, value, _changeTime);
    }
}
