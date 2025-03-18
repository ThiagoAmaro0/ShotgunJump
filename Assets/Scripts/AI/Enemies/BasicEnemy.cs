using UnityEngine;

public class BasicEnemy : BaseEnemy
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Vector2 _walkSpeed;
    [SerializeField] private Vector2 _chaseSpeed;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _deadParticle;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private AudioSource _deadSound;
    private PatrolStrategy _patrol;
    private ChaseStrategy _chase;

    protected override void Awake()
    {
        base.Awake();
        _patrol = new PatrolStrategy(_waypoints, _walkSpeed, _rigidbody, _spriteRenderer);
        _chase = new ChaseStrategy(_chaseSpeed, _rigidbody, _spriteRenderer, _animator);
        SetStrategy(_patrol);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_playerLayer & (1 << collision.gameObject.layer)) != 0)
        {
            _chase.SetTarget(collision.transform);
            SetStrategy(_chase);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if ((_playerLayer & (1 << collision.gameObject.layer)) != 0)
        {
            _chase.SetTarget(null);
            SetStrategy(_patrol);
        }
    }

    protected override void Die()
    {
        _deadSound.Play();
        _spriteRenderer.enabled = false;
        _deadParticle.Play();
        base.Die();
    }
}