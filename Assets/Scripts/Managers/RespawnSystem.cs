using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private AudioSource _deadSound;
    private Checkpoint _currentSpawn;
    public static RespawnSystem instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void Respawn()
    {
        if (_currentSpawn)
        {
            _deadSound.Play();
            _playerRigidbody.linearVelocity = Vector2.zero;
            _playerRigidbody.transform.position = _currentSpawn.transform.position;
        }
    }
    public void SetRespawn(Checkpoint checkPoint)
    {
        if (!_currentSpawn)
        {
            _currentSpawn = checkPoint;
        }
        else if (checkPoint.Priority > _currentSpawn.Priority)
        {
            _currentSpawn = checkPoint;
        }
    }
}
