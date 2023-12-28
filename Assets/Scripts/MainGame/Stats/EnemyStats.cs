using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private GameObject pickUpGO;

    public int score = 25;
    public string enemyName = "Enemy";

    protected override void Die()
    {
        base.Die();

        PickUpManager.Instance.DropPickup(gameObject, score);
        SceneProgressManager.instance.HandleEnemyDies(gameObject);

        Destroy(gameObject);
    }

}
