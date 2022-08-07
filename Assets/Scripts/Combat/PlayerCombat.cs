using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRadius = 3f;
    public LayerMask attackMask;

    private CharacterCombat playerCombat;

    private EnemyStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, attackMask);

            foreach(var hitCollider in hitColliders)
            {
                enemyStats = hitCollider.GetComponent<EnemyStats>();

                playerCombat.Attack(enemyStats);
            }

        }   
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
