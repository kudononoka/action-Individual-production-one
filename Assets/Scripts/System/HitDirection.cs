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
    [SerializeField] ParticleSystem _hitSlashEffect;

    [Header("�q�b�g�G�t�F�N�g")]
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
        //�X���[���o
        _timeManager.SlowSystem.OnOffSlow(true);

        //�q�b�g�G�t�F�N�g�̈ʒu�ݒ�
        var vec = _mcTra.position - hitObject.transform.position;
        Vector3 pos = (vec * _length) + hitObject.transform.position;
        _hitSlashEffect.transform.position = pos;
        _hitEffect.transform.position = pos;

        //�q�b�g�G�t�F�N�g�̊p�x�ݒ�
        float angle = UnityEngine.Random.Range(30, 45);
        _hitSlashEffect.startRotation = angle;

        //�q�b�g�G�t�F�N�g�Đ�
        _hitSlashEffect.Play();
        _hitEffect.Play();

        Debug.Log("Hit");
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
