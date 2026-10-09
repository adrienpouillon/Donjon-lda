using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Dependances")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] GameObject _footPosition;
    [SerializeField] float _jumpForce;


    [Header("Component Optionnal")]
    [SerializeField] Animator _animator;


    bool _isGrounded;
    string _jumpNameAnimator = "Is Jumping";
    string _groundNameAnimator = "Is Grounded";

    bool _wantToJump = false;

    public bool wantToJump
    {
        get => _wantToJump;
        set => _wantToJump = value;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (_animator == null) _animator = null;
    }

    void Reset()
    {
        _animator = GetComponent<Animator>();
        _jumpNameAnimator = "Is Jumping";
        _groundNameAnimator = "Is Grounded";
    }

    void Update()
    {

        //Ground detection
        _isGrounded = Physics.Raycast(_footPosition.transform.position, Vector3.down, 0.4f);
        Debug.DrawRay(_footPosition.transform.position, Vector3.down * 1, Color.red);


        //jump action
        if (_isGrounded)
        {
            _animator?.SetBool(_jumpNameAnimator, false);
            if (_wantToJump)
            {
                _rb.AddForce(Vector3.up * _jumpForce);
                _animator?.SetBool(_jumpNameAnimator, true);
            }
            _animator?.SetBool(_groundNameAnimator, true);
        }
        else
        {
            _animator?.SetBool(_groundNameAnimator, false);
        }
    }
}
