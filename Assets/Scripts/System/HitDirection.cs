using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDirection : MonoBehaviour
{
    [SerializeField] TimeManager _timeManager;

    [Header("スロー時間")]
    [SerializeField, Range(0, 1)] float _slowTime;

    [Header("ヒットエフェクト")]
    [SerializeField] ParticleSystem _hitSlashEffect;

    [Header("ヒットエフェクト")]
    [SerializeField] ParticleSystem _hitEffect;

    [SerializeField, Range(0, 1)] float _length;

    Transform _mcTra;

    Dictionary<int, float> _hitEffectRotationData = new();

    float _timer = 0;

    public void Start()
    {
        _mcTra = Camera.main.transform;
    }

    public void HitAction(GameObject hitObject)
    {
        //スロー演出
        _timeManager.SlowSystem.OnOffSlow(true);

        //ヒットエフェクトの位置設定
        var vec = _mcTra.position - hitObject.transform.position;
        Vector3 pos = (vec * _length) + hitObject.transform.position;
        _hitSlashEffect.transform.position = pos;
        _hitEffect.transform.position = pos;

        //ヒットエフェクトの角度設定
        float angle = UnityEngine.Random.Range(30, 45);
        _hitSlashEffect.startRotation = angle;

        //ヒットエフェクト再生
        _hitSlashEffect.Play();
        _hitEffect.Play();

        Debug.Log("Hit");
    }

    public void Update()
    {
        //スロー中
        if (_timeManager.SlowSystem.IsSlowing)
        {
            _timer += Time.deltaTime;
            //スロー時間を経過したら
            if (_timer >= _slowTime)
            {
                //スロー停止
                _timer = 0;
                _timeManager.SlowSystem.OnOffSlow(false);
            }
        }
    }
}
