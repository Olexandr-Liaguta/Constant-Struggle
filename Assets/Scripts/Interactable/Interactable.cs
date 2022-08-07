using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 5f;
    public Transform interctionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        // Debug.Log("Interact with" + transform.name);
    }

    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interctionTransform.position);

            if(distance < radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interctionTransform == null) interctionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interctionTransform.position, radius);
    }
}
