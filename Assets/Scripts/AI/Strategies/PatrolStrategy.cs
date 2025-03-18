using UnityEngine;

public class PatrolStrategy : Strategy
{
    private Rigidbody2D _enemyRigidbody;
    private SpriteRenderer _spriteRenderer;
    private Transform[] _waypoints;
    private Vector2 _speed;
    private int _index;
    private int _direction = 1;
    private const float STOP_DISTANCE = 0.1f;

    public PatrolStrategy(Transform[] waypoints, Vector2 speed, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer)
    {
        _waypoints = waypoints;
        _speed = speed;
        _enemyRigidbody = rigidbody;
        _spriteRenderer = spriteRenderer;
    }

    public override void Start(BaseEnemy baseAI)
    {
        _index = 0;
    }

    public override void Update(BaseEnemy baseAI)
    {
        if (_waypoints.Length == 0)
            return;
        if (Vector2.Distance(baseAI.transform.position, _waypoints[_index].position) < STOP_DISTANCE)
        {
            if (_index + _direction < 0 || _index + _direction >= _waypoints.Length)
            {
                _direction *= -1;
            }
            _index += _direction;
        }
        else
        {
            Vector2 velocity = _speed * (_waypoints[_index].position - baseAI.transform.position).normalized * Time.deltaTime;
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
    }
}