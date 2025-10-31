using System.Collections;
using UnityEngine;

public enum PlayerState : int
{
    Idle,
    Walk,
    Run,
    Shoot,
}
public class Player : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerState _playerState = PlayerState.Idle;
    [SerializeField] private PlayerMoving _playerMoving;

    private float _horizontal;
    private float _vertical;
    private Vector3 _moveDirection;

    private Camera _mainCam;
    private void Awake()
    {
        _mainCam = Camera.main;
    }
    private void Update()
    {
        HandleMoving();
     
        if (Input.GetMouseButtonDown(0))
        {
            ChangePlayerState(PlayerState.Shoot);
        }       
        if (_playerState != PlayerState.Shoot)
        {
            OnShootAnimEnd();
        }
    }

    public void HandleMoving()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Vector3 camForward = _mainCam.transform.forward;
        Vector3 camRight = _mainCam.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        _moveDirection = (camForward * _vertical + camRight * _horizontal).normalized;

        _playerMoving.SetDirection(_moveDirection);
    }

    public void ChangePlayerState(PlayerState newState)
    {
        if (_playerState == newState)
        {
            return;
        }
        _playerState = newState;

        _animator.SetInteger("PlayerState", (int)newState);

        switch (_playerState)
        {
            case PlayerState.Idle:
                {
                    _playerMoving.SetSpeed(0f);
                }
                break;
            case PlayerState.Walk:
                {
                    _playerMoving.SetSpeed(_walkSpeed);
                }
                break;
            case PlayerState.Run:
                {
                    _playerMoving.SetSpeed(_runSpeed);
                }
                break;
            case PlayerState.Shoot:
                {
                    _animator.SetTrigger("Shoot");
                    StartCoroutine(CoroutineShoot());
                }
                break;
        }        
    }

    IEnumerator CoroutineShoot()
    {
        yield return new WaitForSeconds(0.5f);

        OnShootAnimEnd();
    }
    
    public void OnShootAnimEnd()
    {
        if (_moveDirection.magnitude == 0f)
        {
            ChangePlayerState(PlayerState.Idle);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            ChangePlayerState(PlayerState.Run);
        }
        else
        {
            ChangePlayerState(PlayerState.Walk);
        }
    }
}
