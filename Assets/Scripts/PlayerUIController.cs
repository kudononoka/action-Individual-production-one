using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("UI")]

    [SerializeField, Tooltip("HPバー")]
    Slider _hpVer;

    [SerializeField, Tooltip("STバー")] 
    Slider _stVer;

    [SerializeField, Tooltip("HPText")] 
    Text _hpValueText;

    [Header("パラメーター設定")]
    [SerializeField, Tooltip("バー変化時間")] 
    float _changeTime = 0.3f;

    /// <summary>HP最大値</summary>
    int _hpMax;
    /// <summary>現在のHP</summary>
    int _hpNow;
    /// <summary>ST最大値</summary>
    float _stMax;
    /// <summary>現在のST</summary>
    float _stNow;

    /// <summary>HPをMaxに設定する</summary>
    /// <param name="value">HP最大値</param>
    public void SetUpMaxHP(int value)
    {
        _hpMax = value;
        _hpNow = _hpMax;
        _hpVer.maxValue = _hpMax;
        _hpVer.value = _hpMax;
        _hpValueText.text = $"{_hpMax} / {_hpNow}";
    }

    /// <summary>STをMaxに設定する</summary>
    /// <param name="value">ST最大値</param>
    public void SetUpMaxST(float value)
    {
        _stMax = value;
        _stNow = _stMax;
        _stVer.maxValue = _stMax;
        _stVer.value = _stMax;
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
        _stNow = currentValue;
        _stVer.value = _stNow;
        //ChangingVerValueSP(currentValue);
    }

    /// <summary>DoTweenでVerの値を滑らかに変化させる</summary>
    /// <param name="value">設定したいHP値</param>
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

    /// <summary>DoTweenでVerの値を滑らかに変化させる</summary>
    /// <param name="value">設定したいST値</param>
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
