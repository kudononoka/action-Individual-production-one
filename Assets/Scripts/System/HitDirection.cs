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
    [SerializeField] ParticleSystem[] _hitSlashEffect;

    [Header("ヒットエフェクト")]
    [SerializeField] ParticleSystem[] _hitEffect;

    [Header("ヒットエフェクトPlayInterval")]
    [SerializeField] float _playIntervalTime = 0.2f;

    [Header("それぞれのヒットエフェクトの幅")]
    [SerializeField] float _distance = 0.5f;

    [SerializeField, Range(0, 1)] float _length;

    Transform _mcTra;

    float _timer = 0;

    public void Start()
    {
        _mcTra = Camera.main.transform;
    }

    public void HitAction(GameObject hitObject, PlayerStateMachine.StateType attackType)
    {
        //スロー演出
        _timeManager.SlowSystem.OnOffSlow(true);

        //ヒットエフェクトの位置設定
        var vec = _mcTra.position - hitObject.transform.position;
        Vector3 centerPos = (vec * _length) + hitObject.transform.position;
        _hitSlashEffect[1].transform.position = centerPos;
        _hitEffect[1].transform.position = centerPos;

        //ため攻撃の場合2つエフェクトを増やす
        if (attackType == PlayerStateMachine.StateType.ChargeAttack)
        {
            Debug.Log("attack");
            //1個目のヒットエフェクトを中心に
            //2個目は左斜め上
            _hitSlashEffect[0].transform.position = (_hitEffect[1].transform.up - _hitEffect[1].transform.right) * _distance + centerPos;
            _hitEffect[0].transform.position = (_hitEffect[1].transform.up - _hitEffect[1].transform.right) * _distance + centerPos;

            //2個目は右斜め下
            _hitSlashEffect[2].transform.position = (-_hitEffect[1].transform.up + _hitEffect[1].transform.right) * _distance + centerPos;
            _hitEffect[2].transform.position = (-_hitEffect[1].transform.up + _hitEffect[1].transform.right) * _distance + centerPos;

            StartCoroutine(HitEffectPlay(attackType));
        }
        else
        {
            //ヒットエフェクトの角度設定
            float angle = UnityEngine.Random.Range(30, 45);
            _hitSlashEffect[1].startRotation = angle;

            //ヒットエフェクト再生
            _hitSlashEffect[1].Play();
            _hitEffect[1].Play();

            //音再生
            AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackHit);
        }
    }

    IEnumerator HitEffectPlay(PlayerStateMachine.StateType attackType)
    {
        for (int i = 0; i < _hitEffect.Length; i++)
        {
            //ヒットエフェクトの角度設定
            float angle = UnityEngine.Random.Range(30, 45);
            _hitSlashEffect[i].startRotation = angle;

            //ヒットエフェクト再生
            _hitSlashEffect[i].Play();
            _hitEffect[i].Play();

            //音再生
            AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackHit);

            //ため攻撃以外
            if (attackType != PlayerStateMachine.StateType.ChargeAttack)
            {
                if (i == 0) break;
            }

            yield return new WaitForSeconds(_playIntervalTime);
        }
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
