using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyManager : MonoBehaviour
{
    public static NotifyManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField]
    private GameObject attackNotifyPrefab;

    public void ShowAttackNotify(int damage, bool isMiss, bool isCritical)
    {
        var attackNotifyInst = Instantiate(attackNotifyPrefab);

        var attackNotify = attackNotifyInst.GetComponent<AttackNotifyUI>();

        if (isMiss)
        {
            attackNotify.SetText("miss");
            attackNotify.SetMissColor();
        }
        else
        {
            attackNotify.SetText(damage.ToString());

            if (isCritical)
            {
                attackNotify.SetCriticalTextColor();
            }
        }
    }
}
