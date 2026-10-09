using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderTag : ColliderTag
{
    [Header("Component Required")]
    [SerializeField] Clock _clock;
    [SerializeField] protected LifeComponent _life;

    [Header("Event Feedback")]
    [SerializeField] UnityEvent _onTrapEvent; 

    void Reset()
    {
        _life = GetComponent<LifeComponent>();
    }

    void OnTriggerStay(Collider other)
    {
        if (_clock.IsFinish())
        {
            if(CollideWith(other, _life.DefaultDamage, Tag.Trap, _onTrapEvent))
            {
                _clock.ResetClock();
            }
        }
    }
}
