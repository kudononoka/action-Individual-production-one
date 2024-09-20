using UnityEngine;

public class JustAvoidanceJudgment : MonoBehaviour
{
    public bool OnJudge(GameObject target)
    {
        if (target.GetComponent<StartAttack>().IsStartAttack)
        {
            return true;
        }

        return false;
    }
}
