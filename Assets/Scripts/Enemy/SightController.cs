using UnityEngine;

/// <summary>相手が見えているかどうかの判定をおこなうクラス</summary>
public class SightController : MonoBehaviour
{
    [Header("設定")]

    [Tooltip("視野角(度数法)")]
    [SerializeField, Range(0, 180)] 
    float _sightAngle;

    [Tooltip("物が見える最大の距離")]
    [SerializeField] 
    float _maxDistance = 5f;

    [Tooltip("自分自身の頭")]
    [SerializeField]
    Transform _selfHead;

    [Tooltip("相手の位置")]
    [SerializeField]
    Transform _targetTra;

    public void Init(Transform _taget)
    {
       _targetTra = _taget;
    }

    public bool isVisible(Vector3 targetPos)
    {
        var targetVec = targetPos - _selfHead.position;   //ターゲットへのベクトル
        var targetDistance = targetVec.magnitude;

        var cosHalf = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);

        var innerProduct = Vector3.Dot(_selfHead.forward, targetVec.normalized);

        return innerProduct > cosHalf && targetDistance < _maxDistance;
    }

    private void OnDrawGizmos()
    {
        Vector3 rightBorder = Quaternion.Euler(0, _sightAngle / 2, 0) * _selfHead.forward;//右端
        Vector3 leftBorder = Quaternion.Euler(0, -1 * _sightAngle / 2, 0) * _selfHead.forward;// 左端
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_selfHead.position, rightBorder * _maxDistance);
        Gizmos.DrawRay(_selfHead.position, leftBorder * _maxDistance);
    }
}
