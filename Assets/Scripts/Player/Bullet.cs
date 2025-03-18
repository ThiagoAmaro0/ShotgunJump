using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _bulletRigidbody;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _bounceSpeed = 0.5f;
    [SerializeField] private float _colissionRotation = 1f;
    [SerializeField] private float _upOffset = .1f;
    [SerializeField] private float _destroyDelay = 1f;

    void Awake()
    {
        _bulletRigidbody.linearVelocity = transform.right * _speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(_damage);
        }

        _bulletRigidbody.linearVelocity = Vector2.zero;
        _trail.emitting = false;
        _bulletRigidbody.constraints = RigidbodyConstraints2D.None;
        _bulletRigidbody.gravityScale = 1;
        _bulletRigidbody.AddForce((new Vector2(0, _upOffset) + (Vector2)transform.position - collision.contacts[0].point).normalized * _bounceSpeed);
        _bulletRigidbody.angularVelocity = _colissionRotation;
        _collider.enabled = false;
        Destroy(gameObject, _destroyDelay);
    }
}

public interface IHitable
{
    public void Hit(float damage);
}