using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private float _speed;   
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Rigidbody _rb;

    private Vector3 _moveDirection;
 
    private void Update()
    {      
        if (_moveDirection.magnitude > 0.01f)
        //.magnitude는 벡터의 길이 → 입력이 얼마나 있는지 확인
        // > 0.1f 조건:  이동 입력이 거의 없으면 회전하지 않음
        //LookRotation에 0벡터가 들어가면 에러 발생할 수 있음 → 안전 장치
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_moveDirection), _rotationSpeed * Time.deltaTime);
            // 두 회전 사이를 구면 상에서 부드럽게 보간
            // 즉, 현재 회전에서 목표 회전으로 자연스럽게 회전           
        }
    }
    private void FixedUpdate()
    {
        if (_speed <= 0f)
        {
            return;
        }
        _rb.MovePosition(_rb.position + _moveDirection * _speed * Time.fixedDeltaTime);       
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void SetDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }
}
