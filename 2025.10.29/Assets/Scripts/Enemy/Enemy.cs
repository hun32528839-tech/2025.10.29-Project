using UnityEngine;

public enum EnemyState : int
{
    Idle,
    Run,
    Attack,
    Die,
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyState _enemyState = EnemyState.Idle;

    private PlayerMoving _playerMoving;
    private EnemySpawn _enemySpawn;

    private void Start()
    {
        _playerMoving = FindFirstObjectByType<PlayerMoving>();
        _enemySpawn = FindFirstObjectByType<EnemySpawn>();
    }
    private void Update()
    {
        Vector3 direction = (_playerMoving.transform.position - transform.position).normalized;

        transform.position = transform.position + direction * _speed * Time.deltaTime;

        transform.forward = direction;
    }

    public void ChangeEnemyState(EnemyState newState)
    {
        if (_enemyState == newState)
        {
            return;
        }
        _enemyState = newState;

        _animator.SetInteger("EnemyState", (int)newState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
// 적이랑 부딪혀도 체력이 깎이고 , 공격당해도 깎이고, 

// 1.적 UI (체력 , ) 구현 (적 머리위에 따라다니게끔 world 좌표사용)
// 2.적과 플레이어 충돌 감지 구현 (플레이어와 충돌해도 체력 깎이고 , 공격당해도 깎이고,)
// 3.적이 플레이어와 어느정도 거리가 되면, 공격 애니메이션 (어차피 계속 플레이어한테 다가올테니,)
// 처음 스테이지1(타임어택 30초 안에 클리어해야 함) 시작할때 wave1 UI 띄어줘보기, 끝나면 5초 쉬었다가, wave2  UI 띄우기 ..... wave2 끝나면 스테이지 클리어. 스테이지2로.
// 웨이브1에 5마리 2에 7마리 3에 10마리 이런식으로 하고싶은데...