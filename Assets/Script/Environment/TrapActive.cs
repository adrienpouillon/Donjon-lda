using UnityEngine;
using UnityEngine.Events;

public class TrapActive : ColliderTag
{
    [Header("Config")]
    [SerializeField] string _nameActiveAnimator;

    [Header("Component required")]
    [SerializeField] Animator _animator;

    [Header("Event Feedback")]
    [SerializeField] UnityEvent _onActiveEvent;
    [SerializeField] UnityEvent _onDesactiveEvent;

    void Reset()
    {
        _animator = GetComponent<Animator>();
        _nameActiveAnimator = "IsActive";
    }

    void Awake()
    {
        ActiveAnimation(false);
    }

    void OnTriggerEnter(Collider other)
    {
        CollideWithParam(other, ActiveAnimation, Tag.Player, _onActiveEvent, true);
    }

    void OnTriggerExit(Collider other)
    {
        CollideWithParam(other, ActiveAnimation, Tag.Player, _onDesactiveEvent, false);
    }

    void ActiveAnimation(bool active)
    {
        _animator?.SetBool(_nameActiveAnimator, active);
    }
}
