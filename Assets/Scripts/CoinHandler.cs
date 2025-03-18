using UnityEngine;

public class CoinHandler : MonoBehaviour
{
    [SerializeField] private CoinSO _coin;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private AudioSource _collectSound;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Collider2D _collider;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_playerLayer & (1 << collision.gameObject.layer)) != 0)
        {
            _collider.enabled = false;
            _sprite.enabled = false;

            _collectSound.Play();
            _coin.Collect();
            Destroy(gameObject, 2);
        }
    }
}