using UnityEngine;

/// <summary>対象が音を立てていてそれが聞こえているかどうかを判定する</summary>
public class AudibilityController : MonoBehaviour
{
    [Header("聞こえる範囲")]
    [SerializeField, Range(0, 50)]
    float _earshot;

    /// <summary>聞こえた場所</summary>
    Vector3 _soundLocation = Vector3.zero;

    /// <summary>聞こえた場所</summary>
    public Vector3 SoundLocation => _soundLocation;

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
                _soundLocation = target.transform.position;
                return true;
            }
        }

        return false;
    }

    /// <summary>聴覚範囲の可視化</summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, _earshot);
    }
}
