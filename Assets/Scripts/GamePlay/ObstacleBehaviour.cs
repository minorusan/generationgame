using UnityEngine;
using GamePlay.Player;

namespace GamePlay
{
    public class ObstacleBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            FindObjectOfType<PlayerDeathBehaviour>().Kill(other.transform.position);
        }
    }
}