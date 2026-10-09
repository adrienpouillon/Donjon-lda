using UnityEngine;
using UnityEngine.AI;

public class Interact : MonoBehaviour
{
    [Header("Config Parent")]
    [SerializeField] protected string _nameActiveAnimator;

    [Header("Component Required Parent")]
    [SerializeField] Interactor _interactor;

    virtual protected void Start()
    {
        if (_interactor.PhysicsProxyUseTrigger)
        {
            _interactor.PhysicsProxy.onTriggerStay += InteractObjectTrigger;
        }

        if (_interactor.PhysicsProxyUseCollider)
        {
            _interactor.PhysicsProxy.onCollisionStay += InteractObjectCollider;
        }
    }

    virtual protected void OnDestroy()
    {
        if (_interactor.PhysicsProxyUseTrigger)
        {
            _interactor.PhysicsProxy.onTriggerEnter -= InteractObjectTrigger;
        }

        if (_interactor.PhysicsProxyUseCollider)
        {
            _interactor.PhysicsProxy.onCollisionEnter -= InteractObjectCollider;
        }
    }
    protected void InteractObjectCollider(Collision other)
    {
        InteractObject(other.collider.gameObject);
    }

    protected void InteractObjectTrigger(Collider other)
    {
        InteractObject(other.gameObject);
    }

    protected void InteractObject(GameObject other)
    {
        Animator animator = other.GetComponentInParent<Animator>();
        if (animator == null || _interactor.WantToInteract == false)
        {
            return;
        }
        Interaction(animator);
    }

    virtual protected void Interaction(Animator animator)
    {
        Debug.Log("In");
    }
}
