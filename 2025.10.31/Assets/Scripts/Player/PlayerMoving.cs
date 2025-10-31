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
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_moveDirection), _rotationSpeed * Time.deltaTime);
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

    public void SetDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
