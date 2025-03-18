using UnityEngine;

public class ChaseStrategy : Strategy
{
    private Animator _animator;
    private Rigidbody2D _enemyRigidbody;
    private SpriteRenderer _spriteRenderer;
    private Transform _target;
    private Vector2 _speed;

    public ChaseStrategy(Vector2 speed, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer, Animator animator)
    {
        _speed = speed;
        _enemyRigidbody = rigidbody;
        _spriteRenderer = spriteRenderer;
        _animator = animator;
    }

    public override void Start(BaseEnemy baseAI)
    {
        _animator.SetFloat("Chasing", _target ? 1f : 0f);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _animator.SetFloat("Chasing", target ? 1f : 0f);
    }

    public override void Update(BaseEnemy baseAI)
    {
        if (_target)
        {
            Vector2 velocity = _speed * (_target.position - baseAI.transform.position).normalized * Time.deltaTime;
            if (_enemyRigidbody.gravityScale == 0)
                _enemyRigidbody.linearVelocity = velocity;
            else
                _enemyRigidbody.linearVelocity = new Vector2(velocity.x, _enemyRigidbody.linearVelocityY);
            if (_enemyRigidbody.linearVelocityX < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (_enemyRigidbody.linearVelocityX > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
    }

    public override void Exit(BaseEnemy baseAI)
    {
        _enemyRigidbody.linearVelocity = Vector2.zero;
        _spriteRenderer.flipX = false;
        _animator.SetFloat("Chasing", 0f);
    }
}