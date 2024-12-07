using UnityEngine;

public class ButtonCollision : MonoBehaviour
{
    private Animator animator; // Animator component
    public string triggerName = "PlayAnimation"; // Name of the trigger in Animator
    public GameObject targetObject; // Reference to the GameObject with the Animator (the one you want to animate)
    private Animator targetAnimator; // The actual Animator on the target object

    public GameObject barrier; // The barrier to destroy

    void Start()
    {
        // Check if the targetObject is assigned
        if (targetObject != null)
        {
            // Get the Animator component from the target object
            targetAnimator = targetObject.GetComponent<Animator>();
            if (targetAnimator == null)
            {
                Debug.LogError("No Animator found on the target object!");
            }
        }
        else
        {
            Debug.LogError("Target Object is not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            Debug.Log("Player touched the button! Triggering animation and destroying barrier.");

            // Trigger the animation
            if (targetAnimator != null)
            {
                targetAnimator.SetTrigger(triggerName); // Set the trigger to play animation
                Debug.Log("Animation triggered!");
            }

            // Destroy the barrier
            if (barrier != null)
            {
                Destroy(barrier);
                Debug.Log("Barrier destroyed!");
            }
        }
    }
}
