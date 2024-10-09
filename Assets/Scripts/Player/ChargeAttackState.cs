using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class ChargeAttackState : PlayerStateBase
{
    [Header("�U���ł���܂ł̗��ߎ���")]
    [SerializeField] float _chargeTime = 3;

    [Header("�U������")]
    [SerializeField] float _coolTime;

    [Header("���߃p�[�e�B�N�����Đ����鎞��")]
    [SerializeField] float _chargeParticleStartTime;

    [Header("���ߒ���Effect")]
    [SerializeField] ParticleSystem _chargeParticle;

    [Header("���ߏI���������Effect")]
    [SerializeField] ParticleSystem _chargeEndParticle;

    float _timer = 0f;

    Animator _playerAnim;

    PlayerInputAction _inputAction;

    PlayerParameter _playerParameter;

    Weapon _weapon;

    bool _isAttack = false;

    bool _isMadeSound = false;

    bool _isParticlePlay = false;
    public override void Init()
    {
        //Update�ȂǂŎg�p����R���|�[�l���g�Ȃǂ������ŕێ����Ă���
        PlayerController playerController = _playerStateMachine.PlayerController;
        _playerAnim = playerController.PlayerAnim;
        _inputAction = playerController.InputAction;
        _playerParameter = playerController.Parameter;
        _weapon = playerController.PlayerWeapon;
    }
    public override void OnEnter()
    {
        //�A�j���[�V�����ݒ�
        _playerAnim.SetInteger("AttackType", 1);
        _playerAnim.SetTrigger("Attack");

        //������
        _timer = 0f;

        //�_���[�W�ݒ�
        _weapon.Damage = _playerParameter.AttackChargePower;
    }

    public override void OnUpdate()
    {
        //�U��������
        if (_isAttack)
        {
            _timer -= Time.deltaTime;

            //�f�U�艹�����炷
            if (!_isMadeSound)
            {
                AudioManager.Instance.SEPlayOneShot(SE.PlayerAttackStrongSwish);
                _isMadeSound = true;
            }

            //�U�����I�������
            if (_timer <= 0.1f)
            {
                //�ړ���Idle�ɑJ��
                if (_inputAction.InputMove.magnitude <= 0)
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Idle);

                else
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.Walk);
            }

        }
        //�U���O�̗���
        else
        {

            //�{�^���������Ă����
            if (_inputAction.IsAttack)
            {

                _timer += Time.deltaTime;

                //�`���[�W�ł�����
                if (_timer >= _chargeTime)
                {
                    //�`���[�W�����̃p�[�e�B�N���Đ�
                    ParticleStop(_chargeParticle);
                    ParticlePlay(_chargeEndParticle);
                    //���Đ�
                    AudioManager.Instance.SEPlay(SE.PlayerChargeEnd);
                }

                //�`���[�W�J�n
                else if (_timer >= _chargeParticleStartTime)
                {
                    //�p�[�e�B�N���Đ�
                    ParticlePlay(_chargeParticle);
                    //���Đ�
                    AudioManager.Instance.SEPlay(SE.PlayerCharge);
                }

            }

            //�{�^���𗣂�����
            else
            {

                //�`���[�W���������Ă�����
                if (_timer >= _chargeTime)
                {
                    //�p�[�e�B�N��
                    ParticleStop(_chargeEndParticle);
                    //��
                    AudioManager.Instance.SEStop();
                    //�A�j���[�V����
                    _playerAnim.SetTrigger("Attack");
                    //�U���J�n
                    _isAttack = true;
                    //�U�����Ԃɐݒ�
                    _timer = _coolTime;
                }

                //�`���[�W���������Ă��Ȃ�������
                else
                {
                    //�p�[�e�B�N����~
                    ParticleStop(_chargeParticle);
                    //�ʏ�U���֑J��
                    _playerStateMachine.OnChangeState((int)PlayerStateMachine.StateType.AttackComboOne);
                }
            }

        }

    }

    public void ParticlePlay(ParticleSystem particles)
    {
        if(particles.isPlaying)
            return;

        particles.Play();
    }

    public void ParticleStop(ParticleSystem particles)
    {
        particles.Stop();
    }
    public override void OnEnd()
    {
        //������
        _timer = 0;
        _isAttack = false;
        _isMadeSound = false;
        _playerAnim.SetInteger("AttackType", 0);
    }
}