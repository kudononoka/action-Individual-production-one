using UnityEngine;

/// <summary>‘ÎÛ‚ª‰¹‚ğ—§‚Ä‚Ä‚¢‚Ä‚»‚ê‚ª•·‚±‚¦‚Ä‚¢‚é‚©‚Ç‚¤‚©‚ğ”»’è‚·‚é</summary>
public class AudibilityController : MonoBehaviour
{
    [Header("•·‚±‚¦‚é”ÍˆÍ")]
    [SerializeField, Range(0, 50)]
    float _earshot;

    bool _isAudible;

    /// <summary>•·‚±‚¦‚½êŠ</summary>
    Vector3 _soundLocation = Vector3.zero;

    /// <summary>•·‚±‚¦‚½êŠ</summary>
    public Vector3 SoundLocation => _soundLocation;

    /// <summary>‘ÎÛ‚ª”­‚·‚é‰¹‚É•·‚±‚¦‚Ä‚¢‚é‚©‚Ç‚¤‚©</summary>
    /// <param name="target">‘ÎÛ‚Æ‚È‚é‚à‚Ì</param>
    /// <returns>•·‚±‚¦‚½‚çTrue‚ğ•Ô‚·</returns>
    public bool IsAudible(GameObject target)
    {
        //‘ÎÛ‚ª•·‚±‚¦‚é”ÍˆÍ‚É‚¢‚é@‚©‚Â@MakeASound@‚ğ‚Á‚Ä‚¢‚é‚©
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, _earshot);
    }
}
