using UnityEngine;

/// <summary>対象が音を立てていてそれが聞こえているかどうかを判定する</summary>
public class AudibilityController : MonoBehaviour
{
    [Header("聞こえる範囲")]
    [SerializeField, Range(0, 50)]
    float _earshot;

    bool _isAudible;
    
    /// <summary>対象が発する音に聞こえているかどうか</summary>
    /// <param name="target">対象となるもの</param>
    /// <returns>聞こえたらTrueを返す</returns>
    public bool IsAudible(GameObject target)
    {
        //対象が聞こえる範囲にいる　かつ　MakeASound　を持っているか
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
