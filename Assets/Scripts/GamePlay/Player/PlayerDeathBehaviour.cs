using UnityEngine;
using UnityEngine.Events;

using Core.ObjectPooling;

namespace GamePlay.Player
{
    public class PlayerDeathBehaviour : MonoBehaviour
    {
        private Rigidbody2D _body;
        private PlayerAttemptsBehaviour _attempts;

        public GameObject deathParticles;
        public float velocityToKill;
        public UnityEvent onKilled;

        private void Awake()
        {
            PoolManager.Instance.CreatePool(deathParticles, 10);
            _body = GetComponent<Rigidbody2D>();
            _attempts = FindObjectOfType<PlayerAttemptsBehaviour>();
        }

        private void Update()
        {
            if (_attempts.currentAttempts == 0 && _body.velocity.magnitude <= velocityToKill)
            {
                Kill();
            }
        }

        public void Kill()
        {
            Kill(transform.position);
        }

        public void Kill(Vector3 position)
        {
            PoolManager.Instance.ReuseObject(deathParticles, position, Quaternion.identity);
            onKilled.Invoke();
        }
    }
}