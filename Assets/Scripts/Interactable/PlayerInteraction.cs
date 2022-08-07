using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRadius = 3f;
    public LayerMask interactMask;

    private Interactable interactable;

    [SerializeField]
    private GameObject sprite;

    private int focusedItemId;
    private GameObject instantiatedSprite;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRadius, interactMask);

        if (hitColliders.Length == 0) return;

        Collider firstCollider = hitColliders[0];

        HandleFocusSprite(firstCollider);

        HandleInteract(firstCollider);
    }

    private void HandleFocusSprite(Collider collider)
    {
        int itemId = collider.GetInstanceID();

        if (itemId != focusedItemId)
        {
            if (instantiatedSprite != null)
            {
                Destroy(instantiatedSprite);
            }

            focusedItemId = itemId;

            instantiatedSprite = Instantiate(sprite);

            instantiatedSprite.transform.position = new Vector3(
                collider.transform.position.x,
                collider.transform.position.y + 0.3f,
                collider.transform.position.z
            );
        }
    }

    private void HandleInteract(Collider collider)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable = collider.GetComponent<Interactable>();

            interactable.Interact();

            focusedItemId = -1;

            Destroy(instantiatedSprite);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
