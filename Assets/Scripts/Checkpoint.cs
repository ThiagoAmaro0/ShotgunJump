using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public int Priority => (int)transform.position.x;

    void OnTriggerEnter2D(Collider2D other)
    {
        RespawnSystem.instance?.SetRespawn(this);
    }
}