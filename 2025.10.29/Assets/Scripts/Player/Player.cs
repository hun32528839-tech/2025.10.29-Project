using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerState : int
{
    Idle,
    Walk,
    Run,
    Attack,
    Die,
}

public delegate void PlayerHpUpdateDelegate();
public delegate void PlayerDieDelegate();

public class Player : MonoBehaviour
{   
    [SerializeField] private PlayerState _playerState = PlayerState.Idle;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMoving _playerMoving;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    [SerializeField] private int _hp;
  
    private Vector3 _moveDirection;
    private Camera _mainCam;

    private float _horizontal;
    private float _vertical;

    public PlayerHpUpdateDelegate PlayerHpUpdateDelegate;
    public PlayerDieDelegate PlayerDieDelegate;

    public int Hp => _hp;
   
    
    private void Awake()
    {
        _mainCam = Camera.main;
    }
    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Vector3 camForward = _mainCam.transform.forward;
        Vector3 camRight = _mainCam.transform.right;

        camForward.y = 0f;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        _moveDirection = (camForward * _vertical + camRight * _horizontal).normalized;

        _playerMoving.SetDirection(_moveDirection);

        if (Input.GetMouseButtonDown(0))
        {            
            ChangePlayerState(PlayerState.Attack);
        }

        if (_playerState != PlayerState.Attack)
        {
            OnAttackAnimEnd();
        }
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
            case PlayerState.Attack:
                {
                    _animator.SetTrigger("PlayerAttack");
                    StartCoroutine(CoroutineAttack());
                }
                break;
            case PlayerState.Die:
                {

                }
                break;
        }
    }   
    IEnumerator CoroutineAttack()
    {
        yield return new WaitForSeconds(0.5f);

        OnAttackAnimEnd();
    }

    public void OnAttackAnimEnd()
    {
        if (_horizontal != 0 || _vertical != 0)
        {
            ChangePlayerState(Input.GetKey(KeyCode.LeftShift) ? PlayerState.Run : PlayerState.Walk);
        }
        else
        {
            ChangePlayerState(PlayerState.Idle);
        }
    }

    public void ApplyDamage(int damage)
    {
        if (_hp <= 0)
        {
            Time.timeScale = 0;
            PlayerDieDelegate?.Invoke();
        }
        _hp -= damage;

        PlayerHpUpdateDelegate?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ApplyDamage(10);
        }
    }
}



// 적 구현하기 
// 게임 모드 (타임어택) 구현하기