using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class AttackMeleeComponent : MonoBehaviour
{
    TagAll _tagAll;
    bool _wantToAttack;

    [Header("Config")]
    [SerializeField] int _damage;
    [SerializeField] bool _physicsProxyUseTrigger;
    [SerializeField] bool _physicsProxyUseCollider;

    [Header("Player config")]
    [SerializeField] bool _useInputController;

    [Header("Component Required")]
    [SerializeField] Clock _clock;
    [SerializeField] PhysicsProxy _physicsProxy;

    [Header("Component Optionnal")]
    [SerializeField] Animator _animator;
    [SerializeField] string _nameAttackAnimator;
    [SerializeField] UnityEvent _onAttackEvent;
    

    [Header("Show Component")]
    [ShowNativeProperty] TagAll TagAll => _tagAll ?? (_tagAll = SearchTag(gameObject));

    public bool wantToAttack
    {
        get => _wantToAttack; 
        set => _wantToAttack = value;
    }
    bool IsAttacking = false;

    void Reset()
    {
        _damage = 1;
        _clock = GetComponent<Clock>();
        _animator = GetComponent<Animator>();

        _physicsProxyUseTrigger = true;
        _physicsProxyUseCollider = false;
        _nameAttackAnimator = "Is Attacking";
    }

    void Awake()
    {
        _wantToAttack = false;
        if (_animator == null) _animator = null;
    }

    void Start()
    {
        if (_physicsProxyUseTrigger)
        {
            _physicsProxy.onTriggerStay += AttackTrigger;
        }

        if (_physicsProxyUseCollider)
        {
            _physicsProxy.onCollisionStay += AttackCollider;
        }
    }

    /// <summary>
    /// Search a component tag to know what type of entity is 
    /// </summary>
    /// <param name="my"></param>
    /// <returns></returns>
    TagAll SearchTag(GameObject my)
    {
        Rigidbody rb = my.GetComponentInParent<Rigidbody>();
        if(rb != null)
        {
            return rb.gameObject.GetComponentInChildren<TagAll>();
        }
        return null;
    }

    void AttackCollider(Collision other)
    {
        if (other.collider.attachedRigidbody == null)
        {
            return;
        }
        TryAttack(other.collider.attachedRigidbody.gameObject);
    }

    void AttackTrigger(Collider other)
    {
        if (other.attachedRigidbody == null)
        {
            return;
        }
        TryAttack(other.attachedRigidbody.gameObject);
    }

    /// <summary>
    /// For each tag, he looks if the other tag is a tag who he can attack
    /// </summary>
    /// <param name="other"></param>
    void TryAttack(GameObject other)
    {
        if ((_useInputController && _wantToAttack == false) || _clock.IsFinish() == false)
        {
            return;
        }
        TagAll tag_other;
        switch (TagAll.MyTag)
        {
            case Tag.Player:
                tag_other = SearchTag(other);
                if (tag_other == null)
                {
                    return;
                }
                switch (tag_other.MyTag)
                {
                    case Tag.Player:
                        //None
                        break;
                    case Tag.Allie:
                        //None
                        break;
                    case Tag.Enemy:
                        Attack(other);
                        break;
                    case Tag.Boss:
                        Attack(other);
                        break;
                    case Tag.None:
                    case Tag.Count:
                        //None
                        break;
                }
                break;
            case Tag.Allie:
                tag_other = SearchTag(other);
                if (tag_other == null)
                {
                    return;
                }
                switch (tag_other.MyTag)
                {
                    case Tag.Player:
                        //None
                        break;
                    case Tag.Allie:
                        //None
                        break;
                    case Tag.Enemy:
                        Attack(other);
                        break;
                    case Tag.Boss:
                        Attack(other);
                        break;
                    case Tag.None:
                    case Tag.Count:
                        //None
                        break;
                }
                break;
            case Tag.Enemy:
                tag_other = SearchTag(other);
                if (tag_other == null)
                {
                    return;
                }
                switch (tag_other.MyTag)
                {
                    case Tag.Player:
                        Attack(other);
                        break;
                    case Tag.Allie:
                        Attack(other);
                        break;
                    case Tag.Enemy:
                        //None
                        break;
                    case Tag.Boss:
                        //None
                        break;
                    case Tag.None:
                    case Tag.Count:
                        //None
                        break;
                }
                break;
            case Tag.Boss:
                tag_other = SearchTag(other);
                if (tag_other == null)
                {
                    return;
                }
                switch (tag_other.MyTag)
                {
                    case Tag.Player:
                        Attack(other);
                        break;
                    case Tag.Allie:
                        Attack(other);
                        break;
                    case Tag.Enemy:
                        //None
                        break;
                    case Tag.Boss:
                        //None
                        break;
                    case Tag.None:
                    case Tag.Count:
                        //None
                        break;
                }
                break;
            case Tag.None:
            case Tag.Count:
                //None
                break;
        }
    }

    void Attack(GameObject other)
    {
        LifeComponent component = other.GetComponentInChildren<LifeComponent>();
        if (component != null)
        {
            component.CustomDamage(_damage);
            IsAttacking = true;
            _onAttackEvent?.Invoke();

        }
        _clock.ResetClock();
    }

    void Update()
    {
        if (IsAttacking)
        {
            _animator?.SetBool(_nameAttackAnimator, true);
            IsAttacking = false;
        }
        else
        {
            _animator?.SetBool(_nameAttackAnimator, false);
        }

    }

    void OnDestroy()
    {
        if (_physicsProxyUseTrigger)
        {
            _physicsProxy.onTriggerEnter -= AttackTrigger;
        }

        if (_physicsProxyUseCollider)
        {
            _physicsProxy.onCollisionEnter -= AttackCollider;
        }
    }
}
