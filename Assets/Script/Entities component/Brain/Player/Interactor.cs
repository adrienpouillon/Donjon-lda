using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] bool _physicsProxyUseTrigger;
    [SerializeField] bool _physicsProxyUseCollider;

    [Header("Component Required")]
    [SerializeField] PhysicsProxy _physicsProxy;

    bool _wantToInteract;

    public bool WantToInteract
    {
        get => _wantToInteract;
        set => _wantToInteract = value;
    }

    public bool PhysicsProxyUseTrigger
    {
        get => _physicsProxyUseTrigger;
    }

    public bool PhysicsProxyUseCollider
    {
        get => _physicsProxyUseCollider;
    }

    public PhysicsProxy PhysicsProxy
    {
        get => _physicsProxy;
    }

    void Awake()
    {
        _wantToInteract = false;
    }
}
