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

    private Collider focusedItem;
    private GameObject instantiatedSprite;


    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRadius, interactMask);

        if (hitColliders.Length == 0)
        {
            RemoveFocus();
            return;
        };

        Collider firstCollider = hitColliders[0];

        HandleFocusSprite(firstCollider);
    }

    private void HandleFocusSprite(Collider collider)
    {
        int itemId = collider.GetInstanceID();

        if (focusedItem == null || itemId != focusedItem.GetInstanceID())
        {
            RemoveFocus();

            focusedItem = collider;

            instantiatedSprite = Instantiate(sprite);

            instantiatedSprite.transform.position = new Vector3(
                collider.transform.position.x,
                collider.transform.position.y + 0.3f,
                collider.transform.position.z
            );
        }
    }

    private void RemoveFocus()
    {
        if (instantiatedSprite != null)
        {
            Destroy(instantiatedSprite);
        }
    }

    public void HandleInteract()
    {
        if (focusedItem != null)
        {
            interactable = focusedItem.GetComponent<Interactable>();

            interactable.Interact();

            focusedItem = null;

            Destroy(instantiatedSprite);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
