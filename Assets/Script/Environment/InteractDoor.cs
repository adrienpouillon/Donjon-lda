using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class InteractDoor : Interact
{
    [Header("Component Required")]
    [SerializeField] coinInventory _inventory;

    [Header("Event Feedback")]
    [SerializeField] UnityEvent _onOpenEvent;

    override protected void Start()
    {
        base.Start();
    }

    void Reset()
    {
        _inventory = GetComponent<coinInventory>();
    }

    override protected void Interaction(Animator animator)
    {
        OpenDoorScript openDoorScript = animator.gameObject.GetComponent<OpenDoorScript>();
        if (openDoorScript != null && _inventory.RankQuantity >= openDoorScript.RankCoinRequired)
        {
            animator.SetBool(_nameActiveAnimator, true);
            _onOpenEvent?.Invoke();
            openDoorScript.CanvasDoorSetActive(false);
        }
    }
}
