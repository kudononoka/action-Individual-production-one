using UnityEngine;

/// <summary>�Ώۂ����𗧂ĂĂ��Ă��ꂪ�������Ă��邩�ǂ����𔻒肷��</summary>
public class AudibilityController : MonoBehaviour
{
    [Header("��������͈�")]
    [SerializeField, Range(0, 50)]
    float _earshot;

    bool _isAudible;
    
    /// <summary>�Ώۂ������鉹�ɕ������Ă��邩�ǂ���</summary>
    /// <param name="target">�ΏۂƂȂ����</param>
    /// <returns>����������True��Ԃ�</returns>
    public bool IsAudible(GameObject target)
    {
        //�Ώۂ���������͈͂ɂ���@���@MakeASound�@�������Ă��邩
        if (Vector3.Distance(target.transform.position, this.transform.position) <= _earshot 
            && target.TryGetComponent<MakeASound>(out var makeASound))
        {
            if (makeASound.IsSound)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, _earshot);
    }
}
