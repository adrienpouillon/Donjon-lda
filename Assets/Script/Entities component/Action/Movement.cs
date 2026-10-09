using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Dependances")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] Transform _transform;

    [Header("Conf")]
    [SerializeField] float _speedMovement;

    [Header("Component Optionnal")]
    [SerializeField] Animator _animator;


    string _directionNameAnimator = "Is walking"; 


    Vector3 _direction;
    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    void Awake()
    {
        if (_animator == null) _animator = null;
    }

    void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // don't move
        if (_direction.x == 0 && _direction.z == 0)
        {
            _animator?.SetBool(_directionNameAnimator, false);
        }
        //move
        else
        {
            _transform.Translate(_direction * (_speedMovement * Time.deltaTime), Space.World);
            Vector3 ff = _direction;
            ff.y = 0f;
            _transform.forward = ff;

            _animator?.SetBool(_directionNameAnimator, true);
        }
    }
}
