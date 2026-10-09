using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NavMeshController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Vector3 _disToFollow;

    [Header("Component Required")]
    [SerializeField] GameObject _target;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] InputActionReference _navToSupp;

    [Header("Component Optionnal")]
    [SerializeField] Animator _animator;

    void Reset()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (_animator == null) _animator = null;
    }

    void Update()
    {
        if (_target != null)
        {
            Vector3 posTarget = _target.transform.position;
            Vector3 posMy = transform.position;
            if (posTarget.x - posMy.x < _disToFollow.x && posTarget.y - posMy.y < _disToFollow.y && posTarget.z - posMy.z < _disToFollow.z)
            {
                _animator?.SetBool("Is Moving", true);
                _agent.SetDestination(new Vector3(posTarget.x, posTarget.y, posTarget.z));
            }
            else
            {
                _animator?.SetBool("Is Moving", false);
            }
        }

    }
}
