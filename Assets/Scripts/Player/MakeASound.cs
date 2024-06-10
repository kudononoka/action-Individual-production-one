using UnityEngine;

/// <summary>–¾¦“I‚É‰¹‚ğ—§‚½‚¹‚é—§‚½‚¹‚Ä‚¢‚È‚¢‚ğ‘€ì‚·‚éƒNƒ‰ƒX</summary>
public class MakeASound : MonoBehaviour
{
    bool _isSound = false;

    public bool IsSound => _isSound;

    /// <summary>‰¹‚ğ—§‚½‚¹‚é‚©‚Ç‚¤‚©</summary>
    /// <param name="isSound">—§‚½‚¹‚é‚©‚Ç‚¤‚©</param>
    public void IsSoundChange(bool isSound)
    {
        _isSound = isSound;
    }
}
