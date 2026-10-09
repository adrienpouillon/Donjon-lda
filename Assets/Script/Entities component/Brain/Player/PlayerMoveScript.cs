using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveScript : MonoBehaviour
{
    [Header("Script dependences")]
    [SerializeField] Movement _movement;
    [SerializeField] Jump _jump;
    [SerializeField] AttackMeleeComponent _attack;
    [SerializeField] Interactor _interactor;

    [SerializeField] Camera _cam;

    [Header("Input References")]
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _jumpInput;
    [SerializeField] InputActionReference _attackInput;
    [SerializeField] InputActionReference _interactInput;

    [Header("temp")]
    [SerializeField] Animator _animator;

    bool _isGrounded;
    Vector3 _joystickDirection;

    void Start()
    {
        _moveInput.action.started += MoveUpdate;
        _moveInput.action.performed += MoveUpdate;
        _moveInput.action.canceled += MoveStop;

        _attackInput.action.started += AttackUpdate;
        _attackInput.action.performed += AttackUpdate;
        _attackInput.action.canceled += AttackStop;

        _jumpInput.action.started += JumpUpdate;
        _jumpInput.action.canceled += JumpStop;

        _interactInput.action.started += InteractUpdate;
        _interactInput.action.canceled += InteractStop;
    }

    void OnDestroy()
    {
        _moveInput.action.started -= MoveUpdate;
        _moveInput.action.performed -= MoveUpdate;
        _moveInput.action.canceled -= MoveStop;

        _attackInput.action.started -= AttackUpdate;
        _attackInput.action.performed -= AttackUpdate;
        _attackInput.action.canceled -= AttackStop;

        _jumpInput.action.started -= JumpUpdate;
        _jumpInput.action.canceled -= JumpStop;

        _interactInput.action.started -= InteractUpdate;
        _interactInput.action.canceled -= InteractStop;
    }


    // Move
    void MoveUpdate(InputAction.CallbackContext obj)
    {
        _joystickDirection = obj.ReadValue<Vector2>();
        _joystickDirection = new Vector3(_joystickDirection.x, 0f, _joystickDirection.y);
    }
    void MoveStop(InputAction.CallbackContext obj)
    {
        _joystickDirection = Vector3.zero;
    }


    // Jump
    void JumpUpdate(InputAction.CallbackContext obj)
    {
        _jump.wantToJump = true;
    }
    void JumpStop(InputAction.CallbackContext obj)
    {
        _jump.wantToJump = false;
    }


    // Attack
    void AttackUpdate(InputAction.CallbackContext obj)
    {
        _attack.wantToAttack = true;
        _animator?.SetBool("Is Attacking", true);
    }
    void AttackStop(InputAction.CallbackContext obj)
    {
        _attack.wantToAttack = false;
        _animator?.SetBool("Is Attacking", false);
    }

    void InteractUpdate(InputAction.CallbackContext obj)
    {
        _interactor.WantToInteract = true;
    }

    void InteractStop(InputAction.CallbackContext obj)
    {
        _interactor.WantToInteract = false;
    }

    void Update()
    {

        Vector3 directionFinale = _joystickDirection;
        Vector3 f = _cam.transform.forward;
        f.y = 0f;
        Vector3 r = _cam.transform.right;
        // Calcul de la direction finale
        Vector3 dir = (f * directionFinale.z) + (r * directionFinale.x);

        _movement.Direction = dir;
    }
}
