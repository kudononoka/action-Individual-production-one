using UnityEngine;

/// <summary>���肪�����Ă��邩�ǂ����̔���������Ȃ��N���X</summary>
public class SightController : MonoBehaviour
{
    [Header("�ݒ�")]

    [Tooltip("����p(�x���@)")]
    [SerializeField, Range(0, 180)] 
    float _sightAngle;

    [Tooltip("����������ő�̋���")]
    [SerializeField] 
    float _maxDistance = 5f;

    [Tooltip("�������g�̓�")]
    [SerializeField]
    Transform _selfHead;

    [Tooltip("����̈ʒu")]
    [SerializeField]
    Transform _targetTra;

    public void Init(Transform _taget)
    {
       _targetTra = _taget;
    }

    public bool isVisible(Vector3 targetPos)
    {
        var targetVec = targetPos - _selfHead.position;   //�^�[�Q�b�g�ւ̃x�N�g��
        var targetDistance = targetVec.magnitude;

        var cosHalf = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);

        var innerProduct = Vector3.Dot(_selfHead.forward, targetVec.normalized);

        return innerProduct > cosHalf && targetDistance < _maxDistance;
    }

    private void OnDrawGizmos()
    {
        Vector3 rightBorder = Quaternion.Euler(0, _sightAngle / 2, 0) * _selfHead.forward;//�E�[
        Vector3 leftBorder = Quaternion.Euler(0, -1 * _sightAngle / 2, 0) * _selfHead.forward;// ���[
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_selfHead.position, rightBorder * _maxDistance);
        Gizmos.DrawRay(_selfHead.position, leftBorder * _maxDistance);
    }
}
