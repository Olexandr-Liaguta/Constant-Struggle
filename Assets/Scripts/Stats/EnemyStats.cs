using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField]
    GameObject pickUpGO;

    public override void Die()
    {
        base.Die();

        GameObject createdPickUpGO = Instantiate(pickUpGO);

        PickUpObject createdPickUpObject = createdPickUpGO.GetComponent<PickUpObject>();
        createdPickUpObject.transform.position = transform.position;

        var item = ItemManager.instance.GetRandomItem();

        createdPickUpObject.items = new List<Item>() { item };

        Destroy(gameObject);
    }

}
