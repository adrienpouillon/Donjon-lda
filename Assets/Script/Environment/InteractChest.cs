using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractChest : Interact
{
    Func<bool> _externalyLockCondition;

    [Header("Event Feedback")]
    [SerializeField] UnityEvent _onUnlockEvent;

    

    public Func<bool> ExternalyLockCondition
    {
        get { return _externalyLockCondition; }
        set { _externalyLockCondition = value; }
    }

    void Reset()
    {
        _externalyLockCondition = VoidFunc;
    }

    void Awake()
    {
        _externalyLockCondition = VoidFunc;
    }

    override protected void Interaction(Animator animator)
    {
        OpenChest openChest = animator.gameObject.GetComponentInChildren<OpenChest>();
        Debug.Log(_externalyLockCondition());
        if (openChest != null && _externalyLockCondition())
        {
            ActiveAnimation(true, animator, openChest);
            _onUnlockEvent?.Invoke();
        }
    }

    void ActiveAnimation(bool active, Animator animator, OpenChest openChest)
    {
        animator.SetBool(_nameActiveAnimator, active);
        openChest.CoinType.Lock = !active;
    }

    public bool VoidFunc()
    {
        return true;
    }
}
