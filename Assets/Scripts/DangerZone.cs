using UnityEngine;

public class DangerZone : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerAnimation>(out PlayerAnimation player))
        {
            player.DieAnimation();
        }
    }
}
