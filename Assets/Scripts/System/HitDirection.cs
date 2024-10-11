using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDirection : MonoBehaviour
{
    [SerializeField] TimeManager _timeManager;

    [Header("�X���[����")]
    [SerializeField, Range(0, 1)] float _slowTime;

    [Header("�q�b�g�G�t�F�N�g")]
    [SerializeField] ParticleSystem[] _hitSlashEffect;

    [Header("�q�b�g�G�t�F�N�g")]
    [SerializeField] ParticleSystem[] _hitEffect;

    [Header("�q�b�g�G�t�F�N�gPlayInterval")]
    [SerializeField] float _playIntervalTime = 0.2f;

    [Header("���ꂼ��̃q�b�g�G�t�F�N�g�̕�")]
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
        //�X���[���o
        _timeManager.SlowSystem.OnOffSlow(true);

        //�q�b�g�G�t�F�N�g�̈ʒu�ݒ�
        var vec = _mcTra.position - hitObject.transform.position;
        Vector3 centerPos = (vec * _length) + hitObject.transform.position;
        _hitSlashEffect[1].transform.position = centerPos;
        _hitEffect[1].transform.position = centerPos;

        //���ߍU���̏ꍇ2�G�t�F�N�g�𑝂₷
        if (attackType == PlayerStateMachine.StateType.ChargeAttack)
        {
            Debug.Log("attack");
            //1�ڂ̃q�b�g�G�t�F�N�g�𒆐S��
            //2�ڂ͍��΂ߏ�
            _hitSlashEffect[0].transform.position = (_hitEffect[1].transform.up - _hitEffect[1].transform.right) * _distance + centerPos;
            _hitEffect[0].transform.position = (_hitEffect[1].transform.up - _hitEffect[1].transform.right) * _distance + centerPos;

            //2�ڂ͉E�΂߉�
            _hitSlashEffect[2].transform.position = (-_hitEffect[1].transform.up + _hitEffect[1].transform.right) * _distance + centerPos;
            _hitEffect[2].transform.position = (-_hitEffect[1].transform.up + _hitEffect[1].transform.right) * _distance + centerPos;

            StartCoroutine(HitEffectPlay(attackType));
        }
        else
        {
            //�q�b�g�G�t�F�N�g�̊p�x�ݒ�
            float angle = UnityEngine.Random.Range(30, 45);
            _hitSlashEffect[1].startRotation = angle;

            //�q�b�g�G�t�F�N�g�Đ�
            _hitSlashEffect[1].Play();
            _hitEffect[1].Play();

            //���Đ�
            AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackHit);
        }
    }

    IEnumerator HitEffectPlay(PlayerStateMachine.StateType attackType)
    {
        for (int i = 0; i < _hitEffect.Length; i++)
        {
            //�q�b�g�G�t�F�N�g�̊p�x�ݒ�
            float angle = UnityEngine.Random.Range(30, 45);
            _hitSlashEffect[i].startRotation = angle;

            //�q�b�g�G�t�F�N�g�Đ�
            _hitSlashEffect[i].Play();
            _hitEffect[i].Play();

            //���Đ�
            AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackHit);

            //���ߍU���ȊO
            if (attackType != PlayerStateMachine.StateType.ChargeAttack)
            {
                if (i == 0) break;
            }

            yield return new WaitForSeconds(_playIntervalTime);
        }
    }

    public void Update()
    {
        //�X���[��
        if (_timeManager.SlowSystem.IsSlowing)
        {
            _timer += Time.deltaTime;
            //�X���[���Ԃ��o�߂�����
            if (_timer >= _slowTime)
            {
                //�X���[��~
                _timer = 0;
                _timeManager.SlowSystem.OnOffSlow(false);
            }
        }
    }
}
