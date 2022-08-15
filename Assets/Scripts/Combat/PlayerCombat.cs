using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRadius = 3f;
    public LayerMask attackMask;

    private CharacterCombat playerCombat;

    private EnemyStats enemyStats;

    private int? enemyInstanceId = null;

    [SerializeField]
    private GameObject focusAttackPrefab;
    private GameObject instantiatedFocusAttackPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, attackMask);

            if (hitColliders.Length == 0)
            {
                RemoveFocus();
                return;
            };

            if (enemyInstanceId != null)
            {
                foreach (var hitCollider in hitColliders)
                {
                    int colliderInstanceId = hitCollider.GetInstanceID();

                    if (colliderInstanceId == enemyInstanceId)
                    {
                        HitEnemy(hitCollider);

                        return;
                    }
                }
            }

            Collider firstCollider = hitColliders[0];

            int instanceId = firstCollider.GetInstanceID();

            enemyInstanceId = instanceId;

            HitEnemy(firstCollider);

            FocusOnEnemy(firstCollider);
        }
    }

    private void HitEnemy(Collider collider)
    {
        enemyStats = collider.GetComponent<EnemyStats>();

        playerCombat.Attack(enemyStats);

        FaceTarget(collider);
    }

    private void FocusOnEnemy(Collider collider)
    {
        RemoveFocus();

        instantiatedFocusAttackPrefab = Instantiate(focusAttackPrefab);

        instantiatedFocusAttackPrefab.transform.parent = collider.transform;

        instantiatedFocusAttackPrefab.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    private void FaceTarget(Collider collider)
    {
        Vector3 direction = (collider.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 150f);
    }

    private void RemoveFocus()
    {
        if (instantiatedFocusAttackPrefab != null)
        {
            Destroy(instantiatedFocusAttackPrefab);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
