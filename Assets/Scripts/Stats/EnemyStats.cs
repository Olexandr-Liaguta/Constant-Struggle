using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField]
    GameObject pickUpGO;

    public int score = 25;

    public override void Die()
    {
        base.Die();

        PickUpManager.instance.DropPickup(gameObject, score);

        Destroy(gameObject);
    }

}
