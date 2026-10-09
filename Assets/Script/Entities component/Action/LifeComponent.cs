using System;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

public class LifeComponent : MonoBehaviour
{
    [Header("Config PV")]
    [SerializeField] int _startPv;
    [SerializeField] int _maxPv;
    [SerializeField] int _minPv;
    [SerializeField, ReadOnly] int _pv;

    [Header("Config Action")]
    [SerializeField] int _health;
    [SerializeField] int _damage;
    [SerializeField] bool _deadCanDestroy;

    [Header("Component Required")]
    [SerializeField] GameObject _ParentObject;

    [Header("Component Optionnal")]
    [SerializeField] Animator _animator;
    [SerializeField] Clock _clockDeath;
    [SerializeField] Clock _clockHit;

    [SerializeField] string _nameDeadAnimator;
    [SerializeField] string _nameDamageAnimator;

    public event Action<int> OnUpdateLife;
    public event Action<int> OnHealthAll;
    public event Action<int> OnHealthLittle;
    public event Action<int> OnDamageAll;
    public event Action<int> OnDamageLittle;
    public event Action OnDead;


    public int CurrentPV
    {
        get { return _pv; }
        set
        {
            if (IsDead())
            {
                Debug.LogError("PV ne peut pas être en dessous de 0");
            }
            _pv = value;
        }
    }
    public int MaxPV
    {
        get { return _maxPv; }
    }
    public int MinPV
    {
        get { return _minPv; }
    }

    void Reset()
    {
        _startPv = 100;
        _maxPv = 100;
        _minPv = 0;

        _damage = 1;
        _health = 1;
        _deadCanDestroy = true;


        _clockDeath = GetComponent<Clock>();
        _clockHit = GetComponent<Clock>();

        _ParentObject = GetComponentInParent<Rigidbody>().GameObject();
        _animator = GetComponent<Animator>();
        _nameDeadAnimator = "Is Dead";
        _nameDamageAnimator = "Is Hiting";
    }

    void Awake()
    {
        AllHealth();

        if (_animator == null) _animator = null;
    }

    void Start()
    {
        OnDead += ResetClock;
    }

    void Update()
    {
        //dead action
        if (_deadCanDestroy && IsDead())
        {
            //entity has animation
            if (_animator != null && _nameDeadAnimator != "")
            {
                //call to launch a clock to keep time for player to play animation
                if (_animator.GetBool(_nameDeadAnimator) == false)
                {
                    OnDead?.Invoke();
                }
                //clock finished, entity can be destroyed
                if (_clockDeath.IsFinish())
                {
                    Destroy(_ParentObject);
                }
                else
                {
                    _animator.SetBool(_nameDeadAnimator, true);
                }
            }
            //no animation, instant destroy
            else
            {
                Destroy(_ParentObject);
            }
        }
        //Stop hit animation
        if (_clockHit.IsFinish())
        {
            SetAnimation(_nameDamageAnimator, false);
        }

    }

    //Health function
    public void AllHealth()
    {
        _pv += _startPv;
        OnUpdateLife?.Invoke(_pv);
        OnHealthAll?.Invoke(_pv);
    }

    public void DefaultHealth()
    {
        _pv += _health;
        if (_pv > _maxPv)
        {
            _pv = _maxPv;
        }
        OnUpdateLife?.Invoke(_pv);
        OnHealthLittle?.Invoke(_pv);
    }

    public void CustomHealth(int health)
    {
        _pv += health;
        if (_pv > _maxPv)
        {
            _pv = _maxPv;
        }
        OnUpdateLife?.Invoke(_pv);
        OnHealthLittle?.Invoke(_pv);
    }

    //dammage function
    public void DefaultDamageAll()
    {
        _pv -= _minPv;
        OnUpdateLife?.Invoke(_pv);
        OnDamageAll?.Invoke(_pv);
        SetAnimation(_nameDamageAnimator, true);
        _clockHit.ResetClock();
    }
    public void DefaultDamage()
    {
        _pv -= _damage;
        OnUpdateLife?.Invoke(_pv);
        OnDamageLittle?.Invoke(_pv);
        SetAnimation(_nameDamageAnimator, true);
        _clockHit.ResetClock();
    }
    public void CustomDamage(int damage)
    {
        _pv -= damage;
        OnUpdateLife?.Invoke(_pv);
        OnDamageLittle?.Invoke(_pv);
        SetAnimation(_nameDamageAnimator,true);
        _clockHit.ResetClock();
    }

    //other function
    public bool IsDead()
    {
        if (_pv <= _minPv)
        { 
            return true;
        }
        return false;
    }

    void ResetClock()
    {
        _clockDeath.ResetClock();
    }

    void SetAnimation(string nameBool, bool boolean)
    {
        if (nameBool != "")
        {
            _animator?.SetBool(nameBool, boolean);
        }
    }
}

